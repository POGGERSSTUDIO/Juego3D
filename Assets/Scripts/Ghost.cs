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

    }

    
}
