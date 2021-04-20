using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;

public class GameManager : MonoBehaviour
{

    public Text score;
    private int gameScore;
    public VideoPlayer vp;
    PhotonView PV;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0){
            if(Input.anyKey)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Time.timeSinceLevelLoad >= vp.length)
            {
                SceneManager.LoadScene(2);

            }
        }

        if(Time.timeSinceLevelLoad <= 2f){
        
            if(SceneManager.GetActiveScene().buildIndex == 2){

                score = GameObject.Find("Score").GetComponent<Text>();
                
                PV = GameObject.Find("Canvas").GetComponent<PhotonView>();
                if(!PV.IsMine){

                    GameObject.Find("UIPacman").SetActive(false);
                    GameObject.Find("UIGhost").SetActive(true);

                }else{

                    GameObject.Find("UIPacman").SetActive(true);
                    GameObject.Find("UIGhost").SetActive(false);
                    
                }
                

            }
        }
    }

    public void increaseScore(int objScore){
        gameScore += objScore;
        score.text = "Score: " + gameScore.ToString();
    }
}
