using UnityEngine;
using UnityEngine.Assertions;

public class EnemyMovementDodger : EnemyMovement
{
    [SerializeField] private float movementSpeedOnYAxis;
    [SerializeField] private float heightMaxOffset;
    
    private float timeForSinusFunc;

    protected override void Awake()
    {
        Assert.IsTrue(movementSpeedOnYAxis >= 0, "The movementSpeedOnYAxis is not negative");
        Assert.IsTrue(heightMaxOffset >= 0, "The heightOffset is not negative");
        
        base.Awake();

        timeForSinusFunc = 0;
    }

    private void Update()
    {
        timeForSinusFunc += Time.deltaTime;
    }

    protected override void Move()
    {
        var moveDirection = (followedObject.position - transform.position).normalized;
        var moveVelocity = moveDirection * movementSpeed;
        
        /*
         normally for set position the equation is: y = hieghtOffset * Mathf.Sin(timeForSinus * speedOnYAxis);
         but this is velocity, so we need to use derivation
         !Even that in the code is Cosine, object moves on Sine trajectory
         this is the correct way to do it, because in this form you can set the correct amplitude  
         */
        
        var velocityOnYAxis = heightMaxOffset * movementSpeedOnYAxis * Mathf.Cos(timeForSinusFunc * movementSpeedOnYAxis);
        
        rigidbodyComponent.velocity = new Vector3(moveVelocity.x, velocityOnYAxis, moveVelocity.z);
    }
}
