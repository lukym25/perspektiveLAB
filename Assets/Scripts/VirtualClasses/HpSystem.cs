using UnityEngine.Assertions;
using UnityEngine;

public class HpSystem : MonoBehaviour
{
    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp;
    
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject deathParticle;

    protected virtual void Awake()
    {
        Assert.IsTrue(currentHp >= 0, "The currentHp is negative");
        Assert.IsTrue(maxHp >= 0, "The maxHp is negative");
        Assert.IsNotNull(hitParticle, "The hitParticle is null");
        Assert.IsNotNull(deathParticle, "The hitParticle is null");
        
        if (currentHp == 0)
        {
            currentHp = maxHp;
        }
    }

    public void Damage(float damageAmount)
    {
        currentHp -= damageAmount;

        PlayParticle(hitParticle);

        OnHit();

        if (currentHp <= 0)
        {
            currentHp = 0;
            
            PlayParticle(deathParticle);
            
            OnDeath();
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

    protected virtual void OnHeal() { }

    protected virtual void OnHit() { }
    
    protected virtual void OnDeath() { }
    
    private void PlayParticle(GameObject particlePrefab)
    { 
        var newObject = Instantiate(particlePrefab, transform.position, transform.rotation);
        
        var newParticleSystem = newObject.GetComponent<ParticleSystem>();

        if (newParticleSystem == null)
        {
            Destroy(newObject);
            
            return;
        }
        
        newParticleSystem.Play();
    }
}
