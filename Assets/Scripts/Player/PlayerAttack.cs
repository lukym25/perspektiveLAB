using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAttack : Attack
{
    [SerializeField] private InputEvents inputEvents;
    [SerializeField] private GameInfo gameInfo;
    
    [SerializeField] private float autoAimRange;
    [SerializeField] private LayerMask enemyLayer;

    private bool autoAim;

    protected virtual void Awake()
    {
        Assert.IsNotNull(inputEvents, "The inputEvents is null");
        Assert.IsNotNull(gameInfo, "The gameInfo is null");
        Assert.IsTrue(autoAimRange >= 0, "The autoAimRange is negative");
        
        base.Awake();

        autoAim = true;
    }

    private void FixedUpdate()
    {
        if(gameInfo.gameState != GameStateEnum.InGame) {return;}
        
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
        //cast Sphere and Find all enemies in range; max found 10
        Collider[] collidersFound = new Collider[10];
        var numberOfCollisions = Physics.OverlapSphereNonAlloc(transform.position, autoAimRange, collidersFound, enemyLayer.value);
        
        if(numberOfCollisions == 0) {return null;}
        
        //go through all enemies found and decide which is closest 
        Transform closestEnemy = null;
        float distanceToClosestEnemy = autoAimRange + 1;
        
        foreach (var enemyCollider in collidersFound )
        {
            if(enemyCollider == null) {continue;}
            
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
