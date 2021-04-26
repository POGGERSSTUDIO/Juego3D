using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Character
{

    public override void Start()
    {
        
        base.Start();

        walkingSpeed = 500f;
        runningSpeed = 600f;

    }

    public override void Update()
    {
        base.Update();

        if (Time.timeSinceLevelLoad <= 5f)
        {
            GetComponent<CharacterController>().enabled = false;

        }
        else if (Time.timeSinceLevelLoad >= 5f && Time.timeSinceLevelLoad <= 6f)
        {
            GetComponent<CharacterController>().enabled = true;
        }

    }

    
}
