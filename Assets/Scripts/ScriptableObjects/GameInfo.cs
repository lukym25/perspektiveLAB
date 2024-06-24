using UnityEngine;

public enum GameStateEnum
{
    InMenu,
    InGame
}

[CreateAssetMenu(fileName = "GameInfo", menuName = "ScriptableObjects/GameInfo", order = 3)]
public class GameInfo : ScriptableObject
{
    public GameStateEnum gameState;
    public float cameraRotation;
}
