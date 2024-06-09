using System.Collections.Generic;
using UnityEngine;
using Lukas.MyClass;

public class InstancesManager : Singelton<InstancesManager>
{
    public Transform player;

    public List<Transform> enemies;
}
