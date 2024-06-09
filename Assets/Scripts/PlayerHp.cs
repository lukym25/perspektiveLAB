using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour, IHpSystem
{
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
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        //Destroy(gameObject);
    }
}
