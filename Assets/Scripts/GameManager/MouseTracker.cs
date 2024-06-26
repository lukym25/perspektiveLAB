using Lukas.MyClass;
using UnityEngine;

public class MouseTracker : Singleton<MouseTracker>
{
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; 
        }
    }

    public Vector3? GetMousePosition() 
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }

        return null;
    }
}
