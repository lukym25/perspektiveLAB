using System.Collections;
using UnityEngine;

public class EnemyHp : HpSystem
{
    public string name;

    [SerializeField] 
    private GameObject healItem;
    
    protected override void Died()
    {
        InstancesManager.Instance.enemies.Remove(gameObject.transform);
        KillCounter.Instance.Died(name);

        Instantiate(healItem, transform.position, healItem.transform.rotation);
        
        Destroy(gameObject);
    }
}
