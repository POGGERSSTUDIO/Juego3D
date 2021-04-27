using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    public Text score;
    private int gameScore;
    public VideoPlayer vp;
    PhotonView PV;
    float endTime;
    bool gameEnded;
    bool victory;
    GameObject endPanel;
    
    void Start()
    {
        gameEnded = false;
        endTime = 2f;
    }

    
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0){
            if(Input.anyKey)
            {
                SceneManager.LoadScene(1);
            }
        }

        if(Time.timeSinceLevelLoad <= 2f){
        
            if(SceneManager.GetActiveScene().buildIndex == 2){
                
                if(GameObject.Find("Score") != null){
                    score = GameObject.Find("Score").GetComponent<Text>();
                }

                if(GameObject.Find("Canvas") != null){
                    PV = GameObject.Find("Canvas").GetComponent<PhotonView>();
                }

                if(GameObject.FindObjectOfType<OVRCameraRig>() != null){
                    GameObject.FindObjectOfType<OVRCameraRig>().gameObject.SetActive(false);
                }

                if (!PV.IsMine){
                    
                    if(GameObject.Find("UIPacman") != null){
                        GameObject.Find("UIPacman").SetActive(false);
                    }

                }else{

                    GameObject.Find("UIPacman").SetActive(true);
                    
                }

            }
        }

        if(SceneManager.GetActiveScene().buildIndex == 2){

            if(gameEnded){
                EndGame(victory);
            }

            if(Time.timeSinceLevelLoad > 10f && GameObject.FindObjectOfType<PacMan>() == null){

                PhotonNetwork.LeaveRoom();
                Destroy(RoomManager.Instance.gameObject);
                SceneManager.LoadScene(1);

            }

        }
    }

    public void increaseScore(int objScore){
        gameScore += objScore;
        if(score != null){
            score.text = "Score: " + gameScore.ToString();
        }
        
    }

    public void EndGame(bool vPacman){
        
        if(endPanel != null){
            endPanel.SetActive(true);
        }

        if(GameObject.Find("EndText") != null){
            Text endText = GameObject.Find("EndText").GetComponent<Text>();

            if(vPacman){
                endText.text = "GAME ENDED: PACMAN WINS!";
            }else{
                endText.text = "GAME ENDED: GHOSTS WINS!";
            }
        }

        endTime -= Time.deltaTime;

        if(endTime <= 0f){
            PhotonNetwork.LeaveRoom();
            Destroy(RoomManager.Instance.gameObject);
            SceneManager.LoadScene(1);
        }

    }

    public void disableUI(GameObject endScreen){
        
        endPanel = endScreen;
        endPanel.SetActive(false);

    }

    public void setVictory(bool v){
        gameEnded = true;
        victory = v;
    }

    public void Quit(){
        Application.Quit();
    }
}
