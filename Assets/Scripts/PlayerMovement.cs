using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;
    public float walkingSpeed;
    public float runningSpeed;
    private float currentSpeed;
    public float climbSpeed;

    Vector3 velocity;

    public Transform groundCheck;
    public Transform frontCheckPos;
    public float groundDistance;
    public float frontDistance;
    public LayerMask groundMask;
    public LayerMask climbMask;
    bool isGrounded, isClimbing;
    bool frontCheck;
    private Rigidbody rb;

    public Transform[] portalPos;
    
    void Awake()
    {

    }

    void Start(){
        
        currentSpeed = walkingSpeed;
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();

    }

    void FixedUpdate(){

        //MOVEMENT//

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){

            rb.velocity = move * currentSpeed * Time.deltaTime;

        }else{
            rb.velocity = Vector3.zero;
        }

    }

    
    void Update()
    {

        //GROUNDCHECK//
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0 && !isClimbing)
        {
            velocity.y = 0f;    
        }

        if(!isGrounded && !isClimbing){

            velocity.y += 1f;

        }

        //FRONTCHECK//

        frontCheck = Physics.CheckSphere(frontCheckPos.position, frontDistance, climbMask);

        if(frontCheck && Input.GetKey(KeyCode.W)){
            
            isClimbing = true;

            velocity.y = climbSpeed;

            Debug.Log("Climb");

            rb.velocity = Vector3.up * climbSpeed * Time.deltaTime;

        }else{
            isClimbing = false;
        }

        //RUN//

        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            currentSpeed = runningSpeed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkingSpeed;
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

            gm.increaseScore(pm.getScore());

            Destroy(collision.gameObject);

        }

    }

}
