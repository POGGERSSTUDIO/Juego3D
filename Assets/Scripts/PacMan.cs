using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    float changeTime;
    float startTimer;
    Text counter;
    public GameObject[] pacmanLifes;
    public AudioSource[] audioPacman;
    float deathTime;

    public override void Start(){
        base.Start();
        canKill = false;
        lifes = 5f;
        minimap = GameObject.Find("MinimapCamera").GetComponent<Minimap>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        minimap.SetTarget(gameObject.transform);
        walkingSpeed = 500f;
        runningSpeed = 600f;
        score = 0;
        killTimer = 10f;
        changeTime = 0f;
        startTimer = 5f;
        deathTime = 0f;
        counter = GameObject.Find("CountDown").GetComponent<Text>();
        
    }  
    
    public override void Update()
    {
        base.Update();

        if(Time.timeSinceLevelLoad <= 5f){
            walkingSpeed = 0f;
            runningSpeed = 0f;
        }else if(Time.timeSinceLevelLoad >= 5f && Time.timeSinceLevelLoad <= 6f){
            walkingSpeed = 500f;
            runningSpeed = 600f;
        }

        if(Time.timeSinceLevelLoad <= 2f){

            balls = GameObject.FindGameObjectsWithTag("Point").Length;
            if(GetComponent<PhotonView>().IsMine){
                portalPos[0] = GameObject.Find("TP1").transform;
                portalPos[1] = GameObject.Find("TP2").transform;
            }
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

        if(startTimer >= 0 && counter != null){
            
            counter.text = "Game starts in: " + (int) startTimer;
            startTimer -= Time.deltaTime;

        }else{
            if(counter != null){
                counter.gameObject.GetComponentInParent<Image>().gameObject.SetActive(false);
            }
        }


        if(canKill){
            
            if(Time.time > changeTime){
                GameObject.Find("PacmanBody").GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                GameObject.Find("Directional Light").GetComponent<Light>().color = Random.ColorHSV();
                GameObject.Find("Directional Light").GetComponent<Light>().intensity = 15f;
                GameObject.Find("GameManager").GetComponent<AudioSource>().pitch = 1.5f;
                changeTime = Time.time + 0.2f;
            }
            killTimer -= Time.deltaTime;
            if(killTimer <= 0f){
                canKill = false;
            }
        }else{       
            GameObject.Find("PacmanBody").GetComponent<MeshRenderer>().material = defMat;
            GameObject.Find("Directional Light").GetComponent<Light>().color = Color.white;
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = 2f;
            GameObject.Find("GameManager").GetComponent<AudioSource>().pitch = 1f;
            killTimer = 10f;            
        }
        
    }

    void OnTriggerEnter(Collider collision){

        if(collision.gameObject.layer == 11){


            if(collision.gameObject.name == "Plane"){
                
                GetComponent<CapsuleCollider>().enabled = false;
                transform.localPosition = portalPos[1].position;
                GetComponent<CapsuleCollider>().enabled = true;
                
            }else{

                GetComponent<CapsuleCollider>().enabled = false;
                transform.localPosition = portalPos[0].position;
                GetComponent<CapsuleCollider>().enabled = true;

            }

        }

        if(collision.gameObject.tag == "Point"){

            if(audioPacman[0].isPlaying){
                audioPacman[0].Stop();
            }

            audioPacman[0].Play();

            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

            PointManager pm = GameObject.Find(collision.gameObject.name).GetComponent<PointManager>();

            int scoreBalls = pm.getScore();

            Destroy(collision.gameObject);

            gm.increaseScore(scoreBalls);

            score++;

        }

        if(collision.gameObject.tag == "BigPoint"){

            if(audioPacman[0].isPlaying){
                audioPacman[0].Stop();
            }

            audioPacman[0].Play();

            PointManager pm = GameObject.Find(collision.gameObject.name).GetComponent<PointManager>();

            int scoreBalls = pm.getScore();

            Destroy(collision.gameObject);

            gm.increaseScore(scoreBalls);

            canKill = true;

        }

        if (collision.gameObject.tag == "Ghost")
        {
            if(canKill){
                
                collision.gameObject.transform.position = new Vector3(0f, 0f, 0f);

            }else{

                photonView.RPC("PacmanDied", RpcTarget.All);

            }

        }

    }

    public void OnCollisionEnter(Collision collision){

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

        audioPacman[1].Play();

        deathTime = Time.time + 1f;

        pacmanLifes[(int) lifes - 1].SetActive(false);

        lifes--;

        foreach(GameObject g in ghost){

            g.transform.position = new Vector3(0f, 0f, 0f);

        }

        transform.position = new Vector3(0f, 0f, -20f);

    }

}
