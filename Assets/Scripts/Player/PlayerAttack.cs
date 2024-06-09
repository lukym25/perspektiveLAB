using UnityEngine;

public class PlayerAttack : Attack
{
    [SerializeField]
    private InputEvents inputEvents;
    [SerializeField] 
    private Camera mainCamera;

    private bool autoAim = true;

    private void FixedUpdate()
    {
        if (autoAim)
        {
            AutomaticAttack();
        }
        else
        {
            ManualAttack();
        }
    }

    private void AutomaticAttack()
    {
        var closestEnemy = FindClosestEnemy();
        
        if (closestEnemy == null) {return;}
        
        attackingOnObject = closestEnemy;
        Shoot();
    }
    
    private Transform FindClosestEnemy()
    {
        Transform closestEnemy = null;
        float distanceToClosestEnemy = 500;
        
        //go through all enemies and find closest to this object 
        foreach (var enemy in InstancesManager.Instance.enemies )
        {
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.position);
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                closestEnemy = enemy;
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
