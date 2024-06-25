using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAttack : Attack
{
    [SerializeField] private InputEvents inputEvents;
    
    [SerializeField] private float autoAimRange;
    [SerializeField] private LayerMask enemyLayer;

    private bool autoAim;

    protected override void Awake()
    {
        Assert.IsNotNull(inputEvents, "The inputEvents is null");
        Assert.IsTrue(autoAimRange >= 0, "The autoAimRange is negative");
        
        base.Awake();

        autoAim = true;
    }

    public void Attack()
    {
        if (autoAim)
        {
            AutoAttack();
        }
        else
        {
            ManualAttack();
        }
    }

    private void AutoAttack()
    {
        var closestEnemy = FindClosestEnemy();
        
        if (closestEnemy == null) {return;}
        
        targetObject = closestEnemy;
        Shoot();
    }
    
    //can return null
    private Transform FindClosestEnemy()
    {
        var collidersFound = Physics.OverlapSphere(transform.position, autoAimRange, enemyLayer.value);

        if (collidersFound.Length == 0)
        {
            return null;
        }
        
        Transform closestEnemy = null;
        var distanceToClosestEnemy = autoAimRange;
        
        foreach (var enemyCollider in collidersFound )
        {
            var distanceToEnemy = Vector3.Distance(transform.position, enemyCollider.gameObject.transform.position);
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                closestEnemy = enemyCollider.transform;
                distanceToClosestEnemy = distanceToEnemy;
            }
        }

        return closestEnemy;
    }
    
    private void ManualAttack()
    {
        var mousePosition = MouseTracker.Instance.GetMousePosition();
        
        if(mousePosition == null) {return;}
        
        ShootOnTargetPosition(mousePosition.Value);
    }
    
    private void MouseButtonPressed()
    {
        autoAim = false;
    }    
    
    private void MouseButtonReleased()
    {
        autoAim = true;
    }
    
    private void OnEnable()
    {
        inputEvents.MousePrimaryPressed += MouseButtonPressed;
        inputEvents.MousePrimaryReleased += MouseButtonReleased;
    }

    private void OnDisable()
    {
        inputEvents.MousePrimaryPressed -= MouseButtonPressed;
        inputEvents.MousePrimaryReleased -= MouseButtonReleased;
    }
}
