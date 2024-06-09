using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InputEvents", menuName = "ScriptableObjects/InputEvents", order = 1)]
public class InputEvents : ScriptableObject
{
    public float horizontalInput;
    public float verticalInput;
    public event Action SpacePressed;
    public event Action MousePrimaryPressed;
    public event Action MousePrimaryReleased;

    public void SpacePressedEventInvoke()
    {
        SpacePressed?.Invoke();
    }

    public void MousePrimaryPressedEventInvoke()
    {
        MousePrimaryPressed?.Invoke();
    }

    public void MousePrimaryReleasedEventInvoke()
    {
        MousePrimaryReleased?.Invoke();
    }
}
