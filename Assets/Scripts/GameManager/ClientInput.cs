using UnityEngine;
using UnityEngine.Assertions;

public class ClientInput : MonoBehaviour
{
    [SerializeField]
    private InputEvents inputEvents;

    private void Awake()
    {
        Assert.IsNotNull(inputEvents, "The inputEvents is null");
    }

    private void Update()
    {
        inputEvents.horizontalInput = Input.GetAxisRaw("Horizontal");
        inputEvents.verticalInput = Input.GetAxisRaw("Vertical");
        
        ButtonPressed();
    }

    private void ButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputEvents.SpacePressedEventInvoke();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            inputEvents.MousePrimaryPressedEventInvoke();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            inputEvents.MousePrimaryReleasedEventInvoke();
        }
    }
}
