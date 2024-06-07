using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform followedObject;
    public Vector3 offset;
    [SerializeField]
    private Transform cameraObject;

    private void Awake()
    {
        offset = cameraObject.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(followedObject == null)
        {
            followedObject = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        cameraObject.position = followedObject.transform.position + new Vector3(offset.x, offset.y, offset.z);
    }
}
