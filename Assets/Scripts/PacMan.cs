using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PacMan : Character
{
    public GameObject[] portalPos;
    public Minimap minimap;
    float changeTime;
    float lifes;
    float score;
    float balls;
    GameManager gm;
    GameObject[] ghost;
    bool canKill;
    public Material defMat;
    float killTimer;
    public AudioSource[] audioPacman;
    public GameObject[] pacmanLifes;

    public override void Start(){
        base.Start();
        changeTime = 0f;
        canKill = false;
        lifes = 5f;
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
        if (Time.timeSinceLevelLoad <= 5f)
        {
            if (GetComponent<CharacterController>() != null)
            {
                GetComponent<CharacterController>().enabled = false;
            }
                

        }
        else if (Time.timeSinceLevelLoad >= 5f && Time.timeSinceLevelLoad <= 6f) 
        {
            //if(GetComponent<CharacterController>() == null)
           
            GetComponent<CharacterController>().enabled = true;
           
            
        }

        base.Update();
        if(Time.timeSinceLevelLoad <= 2f){

            portalPos = GameObject.FindGameObjectsWithTag("Portal");

            balls = GameObject.FindGameObjectsWithTag("Point").Length;
            ghost = GameObject.FindGameObjectsWithTag("Ghost");
            
            if(GameObject.Find("Panel") != null){
                gm.disableUI(GameObject.Find("Panel"));
            }

        }
        


        if (score == balls){
            gm.setVictory(true);
        }else if (lifes <= 0f){
            gm.setVictory(false);
        }


        if (canKill)
        {

            if (Time.time > changeTime)
            {
                GameObject.Find("PacmanBody").GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                GameObject.Find("Directional Light").GetComponent<Light>().color = Random.ColorHSV();
                GameObject.Find("Directional Light").GetComponent<Light>().intensity = 50f;
                GameObject.Find("MusicPlayer").GetComponent<AudioSource>().pitch = 1.5f;
                changeTime = Time.time + 0.2f;
            }

            killTimer -= Time.deltaTime;
            if (killTimer <= 0f)
            {
                canKill = false;
            }

        }
        else
        {
            GameObject.Find("PacmanBody").GetComponent<MeshRenderer>().material = defMat;
            GameObject.Find("Directional Light").GetComponent<Light>().color = Color.white;
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = 2f;
            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().pitch = 1f;
            killTimer = 10f;
        }


    }

    void OnTriggerEnter(Collider collision){

        if(collision.gameObject.layer == 11){


            if(collision.gameObject.name == "Plane"){
                GetComponent<CharacterController>().enabled = false;
                transform.localPosition = portalPos[0].transform.position;
                GetComponent<CharacterController>().enabled = true;
            }
            else{
                GetComponent<CharacterController>().enabled = false;
                transform.localPosition = portalPos[1].transform.position;
                GetComponent<CharacterController>().enabled = true;
            }

        }

        if(collision.gameObject.tag == "Point"){

            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();

            PointManager pm = GameObject.Find(collision.gameObject.name).GetComponent<PointManager>();

            int scoreBalls = pm.getScore();

            Destroy(collision.gameObject);

            gm.increaseScore(scoreBalls);

            score++;

            if (audioPacman[0].isPlaying)
            {
                audioPacman[0].Stop();
            }

            audioPacman[0].Play();

        }

        if(collision.gameObject.tag == "BigPoint"){

            PointManager pm = GameObject.Find(collision.gameObject.name).GetComponent<PointManager>();

            int scoreBalls = pm.getScore();

            Destroy(collision.gameObject);

            gm.increaseScore(scoreBalls);

            canKill = true;

            if (audioPacman[0].isPlaying)
            {
                audioPacman[0].Stop();
            }

            audioPacman[0].Play();

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

        pacmanLifes[(int)lifes - 1].SetActive(false);
        audioPacman[1].Play();

        GetComponent<CharacterController>().enabled = false;

        lifes--;

        foreach(GameObject g in ghost){

            g.transform.position = new Vector3(0f, 0f, 0f);

        }

        transform.position = new Vector3(0f, 0f, -20f);

        GetComponent<CharacterController>().enabled = true;

    }
}
