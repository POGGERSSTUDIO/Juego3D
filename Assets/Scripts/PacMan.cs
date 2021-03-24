using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : Character
{
    public Transform groundCheck;
    public Transform frontCheckPos;
    public float groundDistance;
    public float frontDistance;
    public LayerMask groundMask;
    public LayerMask climbMask;
    public Transform[] portalPos;
    public bool isGrounded;
    public bool frontCheck;


    public override void Update()
    {
        base.Update();
        isGrounded = GroundCheck();
        Gravity();


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

            //gm.increaseScore(pm.getScore());

            Destroy(collision.gameObject);

        }

    }

    public virtual bool GroundCheck(){
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public virtual void Gravity(){

       if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;    
        }

        if(!isGrounded){

            velocity.y += 1f;
        }
    }

    
}
