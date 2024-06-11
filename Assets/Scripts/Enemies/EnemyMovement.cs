using UnityEngine;
using UnityEngine.Assertions;

public class EnemyMovement : MonoBehaviour
{
    public Transform followedObject;
    [SerializeField] protected Rigidbody rigidbodyComponent;
    
    [SerializeField] protected float movementSpeed;

    protected virtual void Awake()
    {
        followedObject = InstancesManager.Instance.player;
        
        Assert.IsNotNull(rigidbodyComponent, "The rigidbodyComponent is null");
        Assert.IsTrue(movementSpeed >= 0, "The movementSpeed is not negative");
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        var moveDirection = (followedObject.position - transform.position).normalized;
        var moveVelocity = moveDirection * movementSpeed;
        
        rigidbodyComponent.velocity = new Vector3(moveVelocity.x, rigidbodyComponent.velocity.y, moveVelocity.z);
    }
}
