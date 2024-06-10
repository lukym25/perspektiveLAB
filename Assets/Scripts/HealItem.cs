using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    public float healAmount;
    [SerializeField] 
    private LayerMask targetLayer;

    private void OnTriggerEnter(Collider collider)
    {
        var colliderObject = collider.gameObject;
        
        var layerValue = Mathf.Log(targetLayer.value, 2);
        if (colliderObject.layer == (int)layerValue)
        {
            colliderObject.GetComponent<HpSystem>().Heal(healAmount);
            
            Destroy(gameObject);
        }
    }
}
