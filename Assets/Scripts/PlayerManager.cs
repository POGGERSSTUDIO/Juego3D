using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
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
            localPlayer = this;
            CreateController();
        }
    }

    void CreateController()
    {
       
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PacmanController"), Vector3.zero, Quaternion.identity);

    }

}
