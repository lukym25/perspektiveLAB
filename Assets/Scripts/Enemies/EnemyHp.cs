using UnityEngine;
using UnityEngine.Assertions;

public class EnemyHp : HpSystem
{
    [SerializeField] string nameOfEnemy;
    [SerializeField] private GameObject healItemPrefab;

    protected override void Awake()
    {
        Assert.IsNotNull(healItemPrefab, "The healItemPrefab is null");
        Assert.IsNotNull(nameOfEnemy, "The nameOfEnemy is null");
        
        base.Awake();
    }
    
    protected override void OnDeath()
    {
        InstancesManager.Instance.enemies.Remove(gameObject.transform);
        KillCounter.Instance.EnemyDied(nameOfEnemy);

        var newHealItem = Instantiate(healItemPrefab, transform.position, healItemPrefab.transform.rotation);
        InstancesManager.Instance.objects.Add(newHealItem.transform);
        
        Destroy(gameObject);
    }
}
