using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    protected Transform attackingOnObject;
    [SerializeField]
    protected GameObject projectilePrefab;
    
    public float attackSpeed; //Attacks per second

    private Timer attackCooldownTimer;

    protected virtual void Awake()
    {
        attackCooldownTimer = new Timer(0);
    }

    private void Update()
    {
        attackCooldownTimer.CountDown();
    }
    
    protected void Shoot()
    {
        if (attackingOnObject == null) {return;}

        ShootOnTargetPosition(attackingOnObject.position);
    }
    
    protected void ShootOnTargetPosition(Vector3 targetPosition)
    {
        if(attackCooldownTimer.RemainingTime > 0) {return;}
        
        var shootingDirection = (targetPosition - transform.position).normalized;
        var positionOfNewProjectile = shootingDirection + transform.position;
        
        var newProjectile = Instantiate(projectilePrefab, positionOfNewProjectile, projectilePrefab.transform.rotation);
        
        var projectileBehaviour = newProjectile.GetComponent<ProjectileBehaviour>();
        var rigidBodyOfProjectile = newProjectile.GetComponent<Rigidbody>();
        
        if(projectileBehaviour == null) {return;}
        if(rigidBodyOfProjectile == null) {return;}
        
        rigidBodyOfProjectile.velocity = shootingDirection * projectileBehaviour.projectileSpeed;

        attackCooldownTimer.RemainingTime = 1 / attackSpeed;
    }
}
