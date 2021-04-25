using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PacMan : Character
{
    public Transform[] portalPos;
    public Minimap minimap;
    float lifes;
    float score;
    float balls;
    GameManager gm;
    GameObject[] ghost;
    bool canKill;
    public Material defMat;
    float killTimer;

    public override void Start(){
        base.Start();
        canKill = false;
        lifes = 3f;
        minimap = GameObject.Find("Camera").GetComponent<Minimap>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        minimap.SetTarget(gameObject.transform);
        walkingSpeed = 500f;
        runningSpeed = 600f;
        score = 0;
        killTimer = 10f;
    }  
    
    public override void Update()
    {

        base.Update();
        if(Time.timeSinceLevelLoad <= 2f){

            balls = GameObject.FindGameObjectsWithTag("Point").Length;
            portalPos[0] = GameObject.Find("TP1").transform;
            portalPos[1] = GameObject.Find("TP2").transform;
            ghost = GameObject.FindGameObjectsWithTag("Ghost");
            
            if(GameObject.Find("Panel") != null){
                gm.disableUI(GameObject.Find("Panel"));
            }

        }

        if(score == balls){
            gm.setVictory(true);
        }else if (lifes <= 0f){
            gm.setVictory(false);
        }


        if(canKill){
        
            GameObject.Find("PacmanBody").GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            killTimer -= Time.deltaTime;
            if(killTimer <= 0f){
                canKill = false;
            }

        }else{       
            GameObject.Find("PacmanBody").GetComponent<MeshRenderer>().material = defMat;
            killTimer = 10f;            
        }
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

            int scoreBalls = pm.getScore();

            Destroy(collision.gameObject);

            gm.increaseScore(scoreBalls);

            score++;

        }

        if(collision.gameObject.tag == "BigPoint"){

            PointManager pm = GameObject.Find(collision.gameObject.name).GetComponent<PointManager>();

            int scoreBalls = pm.getScore();

            Destroy(collision.gameObject);

            gm.increaseScore(scoreBalls);

            canKill = true;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            if(canKill){
                
                collision.gameObject.transform.position = new Vector3(0f, 0f, 0f);

            }else{

                photonView.RPC("PacmanDied", RpcTarget.All);

            }

        }
    }

    [PunRPC]
    void PacmanDied(){

        lifes--;

        foreach(GameObject g in ghost){

            g.transform.position = new Vector3(0f, 0f, 0f);

        }

        transform.position = new Vector3(0f, 0f, -20f);

    }
}
