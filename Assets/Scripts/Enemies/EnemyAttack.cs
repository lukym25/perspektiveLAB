using UnityEngine;
using UnityEngine.Assertions;

public class EnemyAttack : Attack
{
    [SerializeField] private float attackRadius;
    
    protected override void Awake()
    {
        Assert.IsTrue(attackRadius >= 0, "The attackRadius is negative");
        
        base.Awake();
        
        targetObject = InstancesManager.Instance.player;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var distanceToTargetObject = Vector3.Distance(targetObject.position, transform.position);
        if (distanceToTargetObject <= attackRadius)
        {
            Shoot();
        }
    }
}
