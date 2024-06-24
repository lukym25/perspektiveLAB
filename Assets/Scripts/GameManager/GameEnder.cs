using UnityEngine.Assertions;
using Lukas.MyClass;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEnder : Singleton<GameEnder>
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject killCounter;
        
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    
    [SerializeField] private Slider hpSliderStored;
    [SerializeField] private TextMeshProUGUI hpTextStored;

    private void Awake()
    {
        Assert.IsNotNull(menuManager, "The menuManager is null");
        Assert.IsNotNull(enemySpawner, "The enemySpawner is null");
        Assert.IsNotNull(killCounter, "The killCounter is null");
        Assert.IsNotNull(cameraMovement, "The cameraMovement is null");
        Assert.IsNotNull(player, "The player is null");
        Assert.IsNotNull(playerSpawnPoint, "The playerSpawnPoint is null");
        Assert.IsNotNull(hpSliderStored, "The hpSliderStored is null");
        Assert.IsNotNull(hpTextStored, "The hpTextStored is null");
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
        var newPlayer = Instantiate(player, playerSpawnPoint.position, playerSpawnPoint.rotation);
        
        cameraMovement.ChangeFollowedObject(newPlayer.transform);
        InstancesManager.Instance.player = newPlayer.transform;

        var hpScript = newPlayer.GetComponent<PlayerHp>();
        hpScript.hpSlider = hpSliderStored;
        hpScript.hpText = hpTextStored;
    }

    private void ChangeToMainMenu()
    {
        menuManager.GoToMainMenu();
    }
}
