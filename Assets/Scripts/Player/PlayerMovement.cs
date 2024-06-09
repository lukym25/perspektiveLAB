using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private InputEvents inputEvents;
    [SerializeField]
    private Rigidbody rigidbodyComponent;
    [SerializeField] 
    private Transform groundHitbox;
    
    
    public float movementSpeed;
    public float jumpForce;
    
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        //camera is slightly tilted (30 degrees in y-axis), so the move direction must be recalculated
        var rotationInRad = 30 * Mathf.Deg2Rad;
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
        //map layer = 6
        var checkLayer = 1 << 6;
        
        var collider =  Physics.OverlapBox(groundHitbox.position, groundHitbox.localScale, Quaternion.identity, checkLayer);
        Debug.Log(groundHitbox.localScale);
        Debug.Log(collider.Length);
        return collider.Length != 0;
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