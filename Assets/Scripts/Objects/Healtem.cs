using UnityEngine.Assertions;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    [SerializeField] private float healAmount;
    [SerializeField] private LayerMask targetLayer;

    private void Awake()
    {
        Assert.IsTrue(healAmount >= 0, "The healAmount is negative");
    }

    private void OnTriggerEnter(Collider collider)
    {
        var colliderObject = collider.gameObject;
        
        var layerValue = Mathf.Log(targetLayer.value, 2);
        if (colliderObject.layer == (int)layerValue)
        {
            colliderObject.GetComponent<HpSystem>().Heal(healAmount);

            InstancesManager.Instance.objects.Remove(transform);
            
            Destroy(gameObject);
        }
    }
}
