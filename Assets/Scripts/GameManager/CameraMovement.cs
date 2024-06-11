using UnityEngine.Assertions;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform followedObject;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private GameInfo gameInfo;

    [SerializeField] private float rotationPeriod;
    
    private float timeForRotationSinusFunc; 
    private Vector3 currentOffset;
    private Vector3 maxOffset;

    private void Awake()
    {
        Assert.IsNotNull(cameraObject, "The cameraObject is null");
        Assert.IsNotNull(gameInfo, "The gameInfo is null");
        
        Assert.IsTrue(rotationPeriod >= 0, "The rotationPeriod is negative");
        
        gameInfo.cameraRotation = cameraObject.rotation.eulerAngles.y;
        CalculateStartingValues();
    }

    private void Start()
    {
        followedObject = InstancesManager.Instance.player;
    }

    private void Update()
    {
        if (gameInfo.gameState == GameStateEnum.InGame)
        {
            MoveCameraInGame();
        }
        else
        {
            timeForRotationSinusFunc += Time.deltaTime;
            MoveCameraInMenu();
        }
    }

    private void MoveCameraInGame()
    {
        if (followedObject == null) { return; }

        cameraObject.position = followedObject.transform.position + currentOffset;
    }

    private void MoveCameraInMenu()
    {
        /*absoluteOffset is max offset witch is multiplied by sin and cos by value between (-1, 1)
         Time.deltaTime * 2 * Mathf.PI make is rotate 360 deg per sec
         / rotationPeriod make it rotate 1 cycle per rotationPeriod
         */
        var positionX = maxOffset.x * Mathf.Cos(timeForRotationSinusFunc * 2 * Mathf.PI / rotationPeriod);
        var positionZ = maxOffset.z * Mathf.Sin(timeForRotationSinusFunc * 2 * Mathf.PI / rotationPeriod);
        cameraObject.position = new Vector3(positionX, maxOffset.y, positionZ);
        
        /*Time.deltaTime * 360 make rotation 360 deg per sec
         / rotationPeriod make it rotate 1 cycle per rotationPeriod
         */
        cameraObject.Rotate(0, -Time.deltaTime * 360 / rotationPeriod, 0,Space.World);

        gameInfo.cameraRotation = cameraObject.rotation.eulerAngles.y;
        currentOffset = cameraObject.position;
    }

    public void ChangeFollowedObject(Transform newFollowedObject)
    {
        followedObject = newFollowedObject;
    }

    private void CalculateStartingValues()
    {
        //calculate starting deg in radians, position of camera and centre of map 
        var startingAngle = Vector2.Angle(Vector2.right, new Vector2(cameraObject.position.x, cameraObject.position.z));
        //update to convex angle (if position z is negative the angle is not convex)
        startingAngle = cameraObject.position.z < 0 ? 360 - startingAngle : startingAngle;
        //update to radians
        startingAngle *= Mathf.Deg2Rad;
        
        //calculate max values for x and z
        maxOffset = new Vector3(cameraObject.position.x / Mathf.Cos(startingAngle),cameraObject.position.y, cameraObject.position.z / Mathf.Sin(startingAngle));
        
        //result is time that takes to rotate to starting angle
        timeForRotationSinusFunc = startingAngle * rotationPeriod / 2 / Mathf.PI;
    }
}
