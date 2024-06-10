using System.Collections;
using UnityEngine;

public class EnemyHp : HpSystem
{
    public string name;
    
    protected override void Died()
    {
        InstancesManager.Instance.enemies.Remove(gameObject.transform);
        KillCounter.Instance.Died(name);
        
        Destroy(gameObject);
    }
}
