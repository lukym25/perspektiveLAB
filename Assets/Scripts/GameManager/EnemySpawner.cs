using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyWave[] enemyWaves;
    private int currentWave = 0;
    private int currentSpawn;
    private int currentEnemy;

    public Vector2 spawnPoint1;
    public Vector2 spawnPoint2;

    [SerializeField] private float warmUpTime;

    [SerializeField] private float waitAfterWave;

    private Timer waitingToSpawnTimer;

    private void Awake()
    {
        waitingToSpawnTimer = new Timer(warmUpTime);
        waitingToSpawnTimer.OnCompletionEvent += StartSpawning;
    }

    private void Update()
    {
        if (waitingToSpawnTimer != null)
        {
            waitingToSpawnTimer.CountDown();
        }
    }

    private void StartSpawning()
    {
        waitingToSpawnTimer.OnCompletionEvent -= StartSpawning;
        
        currentSpawn = 0;
        currentEnemy = 0;
        waitingToSpawnTimer.OnCompletionEvent += SpawnEnemy;

        if (enemyWaves.Length == 0)
        {
            EndSpawning();
            return;
        }
        
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var thisWave = enemyWaves[currentWave].wave;
        
        //If it is something to spawn, spawn it
        if (thisWave[currentSpawn].EnemyObject != null) 
        {
            InstantiateEnemyObject();
        }
        
        //Add time after spawning
        waitingToSpawnTimer.RemainingTime = thisWave[currentSpawn].IntervalTime;
        
        //check if it is last Enemy/Spawn and go to next Enemy/Spawn
        GoToNextEnemy(thisWave);
    }
    
    private void InstantiateEnemyObject()
    {
        var enemyObject = enemyWaves[currentWave].wave[currentSpawn].EnemyObject;
        var positionX = Random.Range(spawnPoint1.x, spawnPoint2.x);
        var positionZ = Random.Range(spawnPoint1.y, spawnPoint2.y);
        Vector3 spawnPosition = new Vector3(positionX, 9, positionZ);
        
        var newEnemy = Instantiate(enemyObject, spawnPosition, enemyObject.transform.rotation);
        
        InstancesManager.Instance.enemies.Add(newEnemy.transform);
    }

    private void GoToNextEnemy(EnemySpawn[] thisWave)
    {
        currentEnemy++;
        if (currentEnemy >= thisWave[currentSpawn].NumberOfEnemies)
        {
            currentEnemy = 0;
            currentSpawn++;

            if (currentSpawn >= thisWave.Length)
            {
                currentSpawn = 0;
                currentWave++;

                if (currentWave >= enemyWaves.Length)
                {
                    EndSpawning();
                    return;
                }
                
                waitingToSpawnTimer.RemainingTime = waitAfterWave;
            }
        }
    }

    private void EndSpawning()
    {
        Debug.Log("AllSpawned");
        waitingToSpawnTimer.OnCompletionEvent -= SpawnEnemy;
        waitingToSpawnTimer = null;
    }
    
}