using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform followedObject;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private GameInfo gameInfo;

    public float rotationPeriod;
    
    private float timeForRotation; 
    private Vector3 currentOffset;
    private Vector3 absoluteOffset;

    private void Awake()
    {
        gameInfo.cameraRotation = cameraObject.rotation.eulerAngles.y;
        CalculateStartingValues();
            
        Debug.Log(absoluteOffset);
    }
    
    private void Update()
    {
        if (gameInfo.gameState == GameStateEnum.InGame)
        {
            MoveCameraInGame();
        }
        else
        {
            timeForRotation += Time.deltaTime;
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
        var positionX = absoluteOffset.x * Mathf.Cos(timeForRotation * 2 * Mathf.PI / rotationPeriod);
        var positionZ = absoluteOffset.z * Mathf.Sin(timeForRotation * 2 * Mathf.PI / rotationPeriod);
        cameraObject.position = new Vector3(positionX, absoluteOffset.y, positionZ);
        
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
        absoluteOffset = new Vector3(cameraObject.position.x / Mathf.Cos(startingAngle),cameraObject.position.y, cameraObject.position.z / Mathf.Sin(startingAngle));
        
        //result is time that takes to rotate to this angle
        timeForRotation = startingAngle * rotationPeriod / 2 / Mathf.PI;
    }
}
