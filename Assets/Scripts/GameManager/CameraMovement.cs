using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 offset;
    [SerializeField]
    private Transform followedObject;
    [SerializeField]
    private Transform cameraObject;

    private void Awake()
    {
        offset = cameraObject.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if(followedObject == null) {return;}
        
        cameraObject.position = followedObject.transform.position + new Vector3(offset.x, offset.y, offset.z);
    }

    public void ChangeFolowedObject(Transform newFollowedObject)
    {
        followedObject = newFollowedObject;
    }
}
