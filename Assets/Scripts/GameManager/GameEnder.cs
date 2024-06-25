using UnityEngine.Assertions;
using Lukas.MyClass;
using UnityEngine;

public class GameEnder : Singleton<GameEnder>
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject killCounter;
    [SerializeField] private GameStateManager gameStateManager;
        
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    protected override void Awake()
    {
        Assert.IsNotNull(menuManager, "The menuManager is null");
        Assert.IsNotNull(enemySpawner, "The enemySpawner is null");
        Assert.IsNotNull(killCounter, "The killCounter is null");
        Assert.IsNotNull(gameStateManager, "The gameStateManager is null");
        Assert.IsNotNull(cameraMovement, "The cameraMovement is null");
        Assert.IsNotNull(playerPrefab, "The playerPrefab is null");
        Assert.IsNotNull(playerSpawnPoint, "The playerSpawnPoint is null");
        
        base.Awake();
    }

    public void RestartGame()
    {
        ClearScene();
        SpawnNewPlayer();
        ChangeToMainMenu();
    }

    private void ClearScene()
    {
        //delete all enemies
        enemySpawner.EndSpawning();
        foreach (var enemy in InstancesManager.Instance.enemies)
        {
            Destroy(enemy.gameObject);
        }
        
        InstancesManager.Instance.enemies.Clear();
        
        //destroy all objects
        foreach (var objectTransform in InstancesManager.Instance.objects) 
        {
            Destroy(objectTransform.gameObject);
        }
        
        InstancesManager.Instance.objects.Clear();
        
        //delete player
        Destroy(InstancesManager.Instance.player.gameObject);
        
        KillCounter.Instance.Reset();
    }

    private void SpawnNewPlayer()
    {
        var newPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        
        gameStateManager.AddNewPlayer(newPlayer.GetComponent<PlayerMain>());
        cameraMovement.ChangeFollowedObject(newPlayer.transform);
        InstancesManager.Instance.player = newPlayer.transform;
    }

    private void ChangeToMainMenu()
    {
        menuManager.GoToMainMenu();
    }
}
