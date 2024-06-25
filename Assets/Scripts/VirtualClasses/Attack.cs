using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class Attack : MonoBehaviour
{
    [SerializeField] protected Transform targetObject;
    [SerializeField] protected GameObject projectilePrefab;
    
    [SerializeField] private float attacksPerSecond;

    private bool canAttack;

    protected virtual void Awake()
    {
        Assert.IsNotNull(projectilePrefab, "The projectilePrefab is null");
        Assert.IsTrue(attacksPerSecond >= 0, "The attacksPerSecond is negative");

        canAttack = true;
    }
    
    protected void Shoot()
    {
        if (targetObject == null) {return;}

        ShootOnTargetPosition(targetObject.position);
    }
    
    protected void ShootOnTargetPosition(Vector3 targetPosition)
    {
        if (!canAttack)
        {
            return;
        }

        SpawnProjectile(targetPosition);

        var attackCooldown = 1 / attacksPerSecond;
        StartCoroutine(AttackCooldown(attackCooldown));
    }

    private void SpawnProjectile(Vector3 targetPosition)
    {
        var shootingDirection = (targetPosition - transform.position).normalized;
        var positionOfNewProjectile = transform.position + shootingDirection;
        
        var newProjectile = Instantiate(projectilePrefab, positionOfNewProjectile, projectilePrefab.transform.rotation);
        InstancesManager.Instance.objects.Add(newProjectile.transform);
        
        AddSpeedToProjectile(newProjectile, shootingDirection);
    }
    
    private void AddSpeedToProjectile(GameObject newProjectile, Vector3 shootingDirection)
    {
        var projectileBehaviour = newProjectile.GetComponent<ProjectileBehaviour>();
        if (projectileBehaviour == null)
        {
            return;
        }
        
        var rigidBodyOfProjectile = newProjectile.GetComponent<Rigidbody>();
        if (rigidBodyOfProjectile == null)
        {
            return;
        }
        
        rigidBodyOfProjectile.velocity = shootingDirection * projectileBehaviour.projectileSpeed;
    }

    private IEnumerator AttackCooldown(float waitTime)
    {
        canAttack = false;
        
        yield return new WaitForSeconds(waitTime);

        canAttack = true;
    }
}
