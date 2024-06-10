using UnityEngine;

public class EnemyHp : MonoBehaviour, IHpSystem
{
    //[HideInInspector]
    public float currentHp;
    public float maxHp;
    public string name;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void Damage(float damageAmount)
    {
        currentHp -= damageAmount;

        if (currentHp <= 0)
        {
            EnemyDied();
        }
    }

    private void EnemyDied()
    {
        InstancesManager.Instance.enemies.Remove(gameObject.transform);
        KillCounter.Instance.Died(name);
        
        Destroy(gameObject);
    }
}
