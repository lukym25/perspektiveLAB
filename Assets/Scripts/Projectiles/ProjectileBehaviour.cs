using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float projectileDamage;
    public float projectileSpeed;
    public LayerMask targetLayer;
    public float lifeTime;
    private float aliveTime;

    private void LateUpdate()
    {
        //destroy after lifeTime time
        aliveTime += Time.deltaTime;
        if (aliveTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        var layerValue = Mathf.Log(targetLayer.value, 2);
        
        if (collision.gameObject.layer == (int)layerValue)
        {
            var hpSystem = collision.gameObject.GetComponent<HpSystem>();
            hpSystem.Damage(projectileDamage);
        }
        
        Destroy(gameObject);
    }
}
