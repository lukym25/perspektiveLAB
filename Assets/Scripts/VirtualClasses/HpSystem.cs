using System;
using UnityEngine;

public class HpSystem : MonoBehaviour
{
    public float currentHp;
    public float maxHp;
    
    [SerializeField] 
    private GameObject hitParticle;
    [SerializeField] 
    private GameObject deathParticle;

    protected void Awake()
    {
        currentHp = maxHp;
    }

    public void Damage(float damageAmount)
    {
        currentHp -= damageAmount;

        PlayParticle(hitParticle);

        Hit();

        if (currentHp <= 0)
        {
            PlayParticle(deathParticle);
            
            Died();
        }
    }
    
    public void Heal(float healAmount)
    {
        currentHp += healAmount;

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        
        OnHeal();
    }

    protected virtual void OnHeal()
    {
        
    }

    protected virtual void Hit()
    {
        
    }
    
    protected virtual void Died()
    {
        
    }
    
    private void PlayParticle(GameObject particlePrefab)
    { 
        var newObject = Instantiate(particlePrefab, transform.position, transform.rotation);
        
        newObject.GetComponent<ParticleSystem>().Play();
    }
}
