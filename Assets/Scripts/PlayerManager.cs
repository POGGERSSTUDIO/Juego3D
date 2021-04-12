using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager localPlayer;
    PhotonView PV;
    public bool isPacman;
    float direction = 1;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();   
    }
    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    
    void Update()
    {
        
    }

    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PacmanController"), Vector3.zero, Quaternion.identity);
    }

    public void BecomePacman(int PacmanNumber){

        if(PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[PacmanNumber]){

            isPacman = true;

        }else{
            isPacman = false;
        }

    }

    public void OnPhotonSerializableView(PhotonStream stream, PhotonMessageInfo info){

        if(stream.IsWriting){
            stream.SendNext(direction);
            stream.SendNext(isPacman);
        }else{
            this.direction = (float)stream.ReceiveNext();
            this.isPacman = (bool)stream.ReceiveNext();
        }

    }
}
