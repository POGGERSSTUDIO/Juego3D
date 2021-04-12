using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    public Text score;
    private int gameScore;

    PhotonView myPV;

    int whichPLayerIsPacman;
    
    void Start()
    {
    
        myPV = GetComponent<PhotonView>();
        Debug.Log(PhotonNetwork.IsMasterClient);
        if(PhotonNetwork.IsMasterClient){
            PickPacman();
        }
    }

    
    void Update()
    {
        
    }

    public void increaseScore(int objScore){

        gameScore += objScore;

        score.text = gameScore.ToString();

    }

    void PickPacman(){

        whichPLayerIsPacman = Random.Range(0, PhotonNetwork.CurrentRoom.PlayerCount);
        myPV.RPC("RPC_SyncPacman", RpcTarget.All, whichPLayerIsPacman);
        Debug.Log("Pacman is: " + whichPLayerIsPacman);

    }

    void RPC_SyncPacman(int playerNumber){
        whichPLayerIsPacman = playerNumber;
        PlayerManager.localPlayer.BecomePacman(whichPLayerIsPacman);
    }
}
