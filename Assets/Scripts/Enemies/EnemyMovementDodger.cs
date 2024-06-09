using UnityEngine;

public class EnemyMovementDodger : EnemyMovement
{
    public float speedOnYAxis;
    public float hieghtOffset;
    private float timeForSinus;

    protected override void Awake()
    {
        base.Awake();

        timeForSinus = Random.Range(0f, 2 * Mathf.PI);
    }

    private void Update()
    {
        timeForSinus += Time.deltaTime;
    }

    protected override void Move()
    {
        var moveDirection = followedObject.position - transform.position;
        var moveVelocity = moveDirection.normalized * movementSpeed;
        
        /*
         normally for set position the equation is: y = hieghtOffset * Mathf.Sin(timeForSinus * speedOnYAxis);
         but this is velocity, so we need to use derivation
         !Even that in the code is Cosine, object moves on Sine trajectory
         this is the correct way to do it, because in this form you can set the correct amplitude  
         */
        
        var moveOnYAxis = hieghtOffset * speedOnYAxis * Mathf.Cos(timeForSinus * speedOnYAxis);
        
        rigidbodyComponent.velocity = new Vector3(moveVelocity.x, moveOnYAxis, moveVelocity.z);
    }
}
