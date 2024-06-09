using Lukas.MyClass;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameInfo gameInfo;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private EnemySpawner enemySpawner;

    private void Awake()
    {
        gameInfo.gameState = GameStateEnum.InMenu;
    }

    public void StartGame()
    {
        gameInfo.gameState = GameStateEnum.InGame;
        
        mainMenu.SetActive(false);
        inGameUI.SetActive(true);
        
        enemySpawner.StartSpawning();
    }

    public void GoToMainMenu()
    {
        gameInfo.gameState = GameStateEnum.InMenu;
        
        inGameUI.SetActive(false);
        mainMenu.SetActive(true);
    }
}
