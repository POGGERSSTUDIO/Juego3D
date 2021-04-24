using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : Character
{
    public Transform[] portalPos;
    public Minimap minimap;

    public override void Start(){
        base.Start();
        minimap = GameObject.Find("Camera").GetComponent<Minimap>();
        walkingSpeed = 500f;
        runningSpeed = 600f;
        portalPos[0] = GameObject.Find("TP1").transform;
        portalPos[1] = GameObject.Find("TP2").transform;
    }  
    
    public override void Update()
    {

        base.Update();
        minimap.SetTarget(gameObject.transform);
    }

    void OnTriggerEnter(Collider collision){

        if(collision.gameObject.layer == 11){


            if(collision.gameObject.name == "Plane"){

                transform.localPosition = portalPos[1].position;
                
            }else{

                transform.localPosition = portalPos[0].position;

            }

        }

        if(collision.gameObject.tag == "Point"){

            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

            PointManager pm = GameObject.Find(collision.gameObject.name).GetComponent<PointManager>();

            gm.increaseScore(pm.getScore());

            Destroy(collision.gameObject);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            GameObject[] ghost = GameObject.FindGameObjectsWithTag("Ghost");
            foreach(GameObject g in ghost){

                g.transform.position = new Vector3(0f, 0f, 0f);

            }
            transform.position = new Vector3(0f, 0f, -20f);
        }
    }
}
