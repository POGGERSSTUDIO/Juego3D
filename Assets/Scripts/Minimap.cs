using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Minimap : MonoBehaviourPunCallbacks
{

    public Transform PacMan;
    public GameObject minimapa;

    void Start(){

        minimapa = GameObject.Find("UIPacman");

    }

    private void LateUpdate()
    {
        Vector3 newPosition = PacMan.position;
        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
    }

    public void SetTarget(Transform target)
    {
        PacMan = target.transform;
        minimapa.SetActive(true);
    }
}
