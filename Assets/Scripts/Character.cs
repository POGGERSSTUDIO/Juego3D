using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Character : MonoBehaviourPunCallbacks
{
    public float walkingSpeed;
    public float runningSpeed;
    private float currentSpeed;
    public Vector3 velocity;
    public Rigidbody rb;
    public PhotonView PV;
    PlayerManager playerManager;
    

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        //playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    void Start(){
        currentSpeed = walkingSpeed;
        rb = GetComponent<Rigidbody>();
        
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    void FixedUpdate(){
        if (!PV.IsMine)
            return;
        Move();
    }

    
    public virtual void Update()
    {
        if (!PV.IsMine)
            return;
        Run();
        
    }

    public virtual void Move(){

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){

            rb.velocity = move * currentSpeed * Time.deltaTime;

        }else{
            rb.velocity = Vector3.zero;
        }
    }
    public virtual void Run(){
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runningSpeed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = walkingSpeed;
        }
    }

    

}
