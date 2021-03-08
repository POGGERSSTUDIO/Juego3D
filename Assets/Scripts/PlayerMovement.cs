using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;
    public GameObject player;
    public float speed = 5f;
    public float climbSpeed;
    private Vector3 crouchScale, normalScale;

    Vector3 velocity;

    public Transform groundCheck;
    public Transform frontCheckPos;
    public float groundDistance;
    public float frontDistance;
    public LayerMask groundMask;
    public LayerMask climbMask;
    bool isGrounded, isClimbing;
    bool frontCheck;
    public float jumpHeight = 3f;
    public bool canJump, canCrouch;
    private Rigidbody rb;

    public Transform[] portalPos;
    
    void Awake()
    {

    }

    void Start(){

        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();

    }

    void FixedUpdate(){

        //MOVEMENT//

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){

            //transform.localPosition += move * speed * Time.deltaTime;

            rb.AddForce(move * speed, ForceMode.Force);

        }

    }

    // Update is called once per frame
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

            transform.localPosition += new Vector3(0f, climbSpeed * Time.deltaTime, 0f);

        }else{
            isClimbing = false;
        }

        //RUN//

        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            speed = 12f;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
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
