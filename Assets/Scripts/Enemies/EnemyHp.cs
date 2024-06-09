using System;
using UnityEngine;

public class EnemyHp : MonoBehaviour, IHpSystem
{
    //[HideInInspector]
    public float currentHP;
    public float maxHp;

    private void Awake()
    {
        currentHP = maxHp;
    }

    public void Damage(float damageAmount)
    {
        currentHP -= damageAmount;

        if (currentHP <= 0)
        {
            EnemyDied();
        }
    }

    private void EnemyDied()
    {
        InstancesManager.Instance.enemies.Remove(gameObject.transform);
        
        Destroy(gameObject);
    }
}
