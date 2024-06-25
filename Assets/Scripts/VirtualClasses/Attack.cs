using UnityEngine;
using UnityEngine.Assertions;

public class Attack : MonoBehaviour
{
    [SerializeField] protected Transform targetObject;
    [SerializeField] protected GameObject projectilePrefab;
    
    [SerializeField] private float attacksPerSecond;

    private Timer attackCooldownTimer;

    protected virtual void Awake()
    {
        Assert.IsNotNull(projectilePrefab, "The projectilePrefab is null");
        Assert.IsTrue(attacksPerSecond >= 0, "The attacksPerSecond is negative");
        //targetObject can be null
        
        attackCooldownTimer = new Timer(0);
    }

    private void Update()
    {
        attackCooldownTimer.CountDown();
    }
    
    protected void Shoot()
    {
        if (targetObject == null) {return;}

        ShootOnTargetPosition(targetObject.position);
    }
    
    protected void ShootOnTargetPosition(Vector3 targetPosition)
    {
        if(attackCooldownTimer.RemainingTime > 0) {return;}
        
        var shootingDirection = (targetPosition - transform.position).normalized;
        var positionOfNewProjectile = transform.position + shootingDirection;
        
        var newProjectile = Instantiate(projectilePrefab, positionOfNewProjectile, projectilePrefab.transform.rotation);
        InstancesManager.Instance.objects.Add(newProjectile.transform);
        
        var projectileBehaviour = newProjectile.GetComponent<ProjectileBehaviour>();
        var rigidBodyOfProjectile = newProjectile.GetComponent<Rigidbody>();
        
        if(projectileBehaviour == null) {return;}
        if(rigidBodyOfProjectile == null) {return;}
        
        rigidBodyOfProjectile.velocity = shootingDirection * projectileBehaviour.projectileSpeed;

        attackCooldownTimer.RemainingTime = 1 / attacksPerSecond;
    }
}
