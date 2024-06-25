using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float lifeTime;

    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Rigidbody rigidbodyComponent;
    
    private float aliveTime;

    private void Awake()
    {
        Assert.IsTrue(projectileDamage >= 0, "The movementSpeed is negative");
        Assert.IsTrue(projectileSpeed >= 0, "The projectileSpeed is negative");
        Assert.IsTrue(lifeTime >= 0, "The lifeTime is negative");

        aliveTime = 0;
    }

    private void LateUpdate()
    {
        //destroy after lifeTime time
        aliveTime += Time.deltaTime;
        if (aliveTime >= lifeTime)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        var collisionLayerBit = 1 << collision.gameObject.layer;
        
        if (collisionLayerBit == targetLayer.value)
        {
            var hpSystem = collision.gameObject.GetComponent<HpSystem>();
            hpSystem.Damage(projectileDamage);
        }

        DestroyProjectile();
    }

    public void AddVelocity(Vector3 direction)
    {
        rigidbodyComponent.velocity = direction * projectileSpeed;
    }

    private void DestroyProjectile()
    {
        InstancesManager.Instance.objects.Remove(transform);
        
        Destroy(gameObject);
    }
}
