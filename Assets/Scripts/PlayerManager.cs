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
       
<<<<<<< Updated upstream
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PacmanController"), Vector3.zero, Quaternion.identity);
=======
        if(PhotonNetwork.IsMasterClient) PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PacmanController"), Vector3.zero, Quaternion.identity);
        else PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "GhostController"), Vector3.zero, Quaternion.identity);
>>>>>>> Stashed changes

    }

}
