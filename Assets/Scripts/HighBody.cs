using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighBody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name=="ghostPau")
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
