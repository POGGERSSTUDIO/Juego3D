using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public static PlayerManager localPlayer;
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();   
    }
    void Start()
    {
        if (PV.IsMine)
        {
            localPlayer = this;
            CreateController();
        }
    }

    void CreateController()
    {
        if(PhotonNetwork.IsMasterClient) PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PacmanController"), new Vector3(0f, 0f, -20f), Quaternion.identity);
        else PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "GhostController"), new Vector3(0f, 0f, -3f), Quaternion.identity);
    }

}
