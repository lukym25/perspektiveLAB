using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lukas.MyClass;

public class InstancesManager : Singelton<InstancesManager>
{
    public Transform Player;

    public Transform[] Enemies;

    private void Awake()
    {
        if (Player != null) {return;}
        
        /**/
    }

    /*private void OnPlayerDestroy()
    {
        Instantiate()
    }*/
}
