using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private InputEvents inputEvents;
    [SerializeField] 
    private GameInfo gameInfo;
    [SerializeField]
    private Rigidbody rigidbodyComponent;
    
    [SerializeField] 
    private Transform groundHitbox;
    [SerializeField] 
    private LayerMask groundLayer;
    
    public float movementSpeed;
    public float jumpForce;
    
    private void FixedUpdate()
    {
        if(gameInfo.gameState != GameStateEnum.InGame) {return;}
        
        MovePlayer();
    }

    private void MovePlayer()
    {
        //camera is slightly tilted (30 degrees in y-axis), so the move direction must be recalculated
        var rotationInRad = gameInfo.cameraRotation * Mathf.Deg2Rad;
        var moveDirectionX = inputEvents.verticalInput * Mathf.Sin(rotationInRad) + inputEvents.horizontalInput * Mathf.Cos(rotationInRad);
        var moveDirectionZ = inputEvents.verticalInput * Mathf.Cos(rotationInRad) - inputEvents.horizontalInput * Mathf.Sin(rotationInRad);
        //direction is automatically normalized with math
        var moveVelocity = new Vector3(moveDirectionX, 0, moveDirectionZ) * movementSpeed;
        
        rigidbodyComponent.velocity = new Vector3(moveVelocity.x, rigidbodyComponent.velocity.y, moveVelocity.z);
    }

    private void PlayerJump()
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

    private void OnEnable()
    {
        inputEvents.SpacePressed += PlayerJump;
    }

    private void OnDisable()
    {
        inputEvents.SpacePressed -= PlayerJump;
    }
}
