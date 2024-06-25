using UnityEngine;
using UnityEngine.Assertions;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected Rigidbody rigidbodyComponent;
    [SerializeField] protected float movementSpeed;
    
    protected Transform FollowedObject;

    protected virtual void Awake()
    {
        Assert.IsNotNull(rigidbodyComponent, "The rigidbodyComponent is null");
        Assert.IsTrue(movementSpeed >= 0, "The movementSpeed is not negative");
    }

    private void Start()
    {
        FollowedObject = InstancesManager.Instance.player;
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        var moveDirection = (FollowedObject.position - transform.position).normalized;
        var moveVelocity = moveDirection * movementSpeed;
        
        rigidbodyComponent.velocity = new Vector3(moveVelocity.x, rigidbodyComponent.velocity.y, moveVelocity.z);
    }

    public void ChangeFollowedObject(Transform newFollowedObject)
    {
        FollowedObject = newFollowedObject;
    }
}
