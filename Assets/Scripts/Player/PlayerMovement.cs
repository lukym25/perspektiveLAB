using UnityEngine;
using UnityEngine.Assertions;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputEvents inputEvents;
    [SerializeField] private GameInfo gameInfo;
    [SerializeField] private Rigidbody rigidbodyComponent;
    
    [SerializeField] private Transform groundHitbox;
    [SerializeField] private LayerMask groundLayer;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private Vector2 moveVectorX;
    private Vector2 moveVectorZ;

    private void Awake()
    {
        Assert.IsNotNull(inputEvents, "The inputEvents is null");
        Assert.IsNotNull(gameInfo, "The gameInfo is null");
        Assert.IsNotNull(rigidbodyComponent, "The rigidbodyComponent is null");
        Assert.IsNotNull(groundHitbox, "The groundHitbox is null");
        Assert.IsTrue(movementSpeed >= 0, "The movementSpeed is negative");
        Assert.IsTrue(jumpForce >= 0, "The jumpForce is negative");
    }

    public void MovePlayer()
    {
        var moveDirection = moveVectorX * inputEvents.verticalInput + moveVectorZ * inputEvents.horizontalInput;
        var moveVelocity = new Vector3(moveDirection.x, 0, moveDirection.y) * movementSpeed;
        
        rigidbodyComponent.velocity = new Vector3(moveVelocity.x, rigidbodyComponent.velocity.y, moveVelocity.z);
    }

    public void PlayerJump()
    {
        if(!IsOnGround()) { return;}
        
        rigidbodyComponent.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsOnGround()
    {
        Collider[] results = new Collider[1];
        var numberOfCollisions = Physics.OverlapBoxNonAlloc(groundHitbox.position, groundHitbox.localScale, results, Quaternion.identity, groundLayer.value);

        return numberOfCollisions > 0;
    }

    public void CalculateMoveVectors()
    {
        var rotationInRad = gameInfo.cameraRotation * Mathf.Deg2Rad;
        moveVectorX = new Vector2(Mathf.Sin(rotationInRad), Mathf.Cos(rotationInRad));
        moveVectorZ = new Vector2(Mathf.Cos(rotationInRad), -Mathf.Sin(rotationInRad));

        Debug.Log(moveVectorX + ";" + moveVectorZ);
    }
}
