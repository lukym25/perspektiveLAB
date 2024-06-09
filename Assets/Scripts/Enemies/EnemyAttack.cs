using UnityEngine;

public class EnemyAttack : Attack
{
    [SerializeField] 
    private float attackRadius;
    
    protected override void Awake()
    {
        base.Awake();
        attackingOnObject = InstancesManager.Instance.player;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Vector3.Distance(attackingOnObject.position, transform.position) <= attackRadius)
        {
            Shoot();
        }
    }
}
