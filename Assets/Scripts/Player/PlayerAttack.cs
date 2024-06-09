using UnityEngine;

public class PlayerAttack : Attack
{
    [SerializeField]
    private InputEvents inputEvents;
    [SerializeField] 
    private Camera mainCamera;
    
    public float autoAimRange;
    [SerializeField]
    private LayerMask enemyLayer;

    private bool autoAim = true;

    private void FixedUpdate()
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
        
        attackingOnObject = closestEnemy;
        Shoot();
    }
    
    private Transform FindClosestEnemy()
    {
        //cast Sphere and Find all enemies in range; max found 10
        Collider[] results = new Collider[10];
        var numberOfCollisions = Physics.OverlapSphereNonAlloc(transform.position, autoAimRange, results, enemyLayer.value);
        
        if(numberOfCollisions == 0) {return null;}
        
        
        //go through all enemies found and decide which is closest 
        Transform closestEnemy = null;
        float distanceToClosestEnemy = 500;
        
        foreach (var enemy in results )
        {
            if(enemy == null) {continue;}
            
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.gameObject.transform.position);
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                closestEnemy = enemy.transform;
                distanceToClosestEnemy = distanceToEnemy;
            }
        }

        return closestEnemy;
    }
    
    private void ManualAttack()
    {
        var mousePosition = MousePositionInWorld();
        
        if(mousePosition == null) {return;}
        
        ShootOnTargetPosition(mousePosition.Value);
    }

    private Vector3? MousePositionInWorld()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }

        return null;
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
