using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform followedObject;
    [SerializeField] private Rigidbody rigidbodyComponent;
    
    public float movementSpeed;

    private void Awake()
    {
        followedObject = InstancesManager.Instance.player;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var moveDirection = followedObject.position - transform.position;
        var moveVelocity = moveDirection.normalized * movementSpeed;
        
        rigidbodyComponent.velocity = new Vector3(moveVelocity.x, rigidbodyComponent.velocity.y, moveVelocity.z);
    }
}
