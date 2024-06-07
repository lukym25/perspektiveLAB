using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InputEvents", menuName = "ScriptableObjects/InputEvents", order = 1)]
public class InputEvents : ScriptableObject
{
    public float horizontalInput;
    public float verticalInput;
    public event Action SpacePressedEvent;

    public void SpaceEventInitiate()
    {
        SpacePressedEvent?.Invoke();
    }
}
