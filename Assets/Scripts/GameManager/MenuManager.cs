using UnityEngine;
using UnityEngine.Assertions;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameInfo gameInfo;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private EnemySpawner enemySpawner;

    private void Awake()
    {
        Assert.IsNotNull(gameInfo, "The gameInfo is null");
        Assert.IsNotNull(mainMenu, "The mainMenu is null");
        Assert.IsNotNull(inGameUI, "The inGameUI is null");
        Assert.IsNotNull(enemySpawner, "The enemySpawner is null");
        
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
