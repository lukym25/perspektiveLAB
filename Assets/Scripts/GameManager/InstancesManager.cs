using System.Collections.Generic;
using UnityEngine;
using Lukas.MyClass;
using UnityEngine.Assertions;

public class InstancesManager : Singleton<InstancesManager>
{
    public Transform player;

    public List<Transform> enemies;
    public List<Transform> objects;

    private void Awake()
    {
        Assert.IsNotNull(player, "The player is null");
    }
}
