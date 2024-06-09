using UnityEngine;

public class ClientInput : MonoBehaviour
{
    [SerializeField]
    private InputEvents inputEvents;

    // Update is called once per frame
    void Update()
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
