using Lukas.MyClass;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEnder : Singelton<GameEnder>
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private EnemySpawner enemySpawner;
        
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawnPoint;
    
    [SerializeField] private Slider hpSliderStored;
    [SerializeField] private TextMeshProUGUI hpTextStored;
    
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
        
        //delete player
        Destroy(InstancesManager.Instance.player.gameObject);
    }

    private void SpawnNewPlayer()
    {
        var newPlayer = Instantiate(player, playerSpawnPoint.position, playerSpawnPoint.rotation);
        
        cameraMovement.ChangeFolowedObject(newPlayer.transform);
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
