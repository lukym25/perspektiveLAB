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

    private void Start()
    {
        waitingToSpawnTimer = new Timer(warmUpTime);
        waitingToSpawnTimer.OnCompletionEvent += StartSpawningWave;
    }

    private void Update()
    {
        Debug.Log(waitingToSpawnTimer.RemainingTime);
        if (waitingToSpawnTimer != null)
        {
            waitingToSpawnTimer.CountDown();
        }
    }

    private void StartSpawningWave()
    {
        waitingToSpawnTimer.OnCompletionEvent -= StartSpawningWave;
        
        currentSpawn = 0;
        currentEnemy = 0;
        waitingToSpawnTimer.OnCompletionEvent += SpawnEnemy;
        
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        var thisWave = enemyWaves[currentWave].Wave;
        
        //If it is something to spawn, spawn it
        if (thisWave[currentSpawn].EnemyObject != null) 
        {
            InstantiateEnemyObject();
        }
        
        //Add time after spawning
        currentEnemy++;
        waitingToSpawnTimer.RemainingTime = thisWave[currentSpawn].IntervalTime;
        
        //check if it is last Enemy/Spawn
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
                    //stopped spawning, last wave spawned
                    Debug.Log("AllSpawned");
                    waitingToSpawnTimer.OnCompletionEvent -= SpawnEnemy;
                    waitingToSpawnTimer.RemainingTime = 0;

                    return;
                }
                
                waitingToSpawnTimer.RemainingTime = waitAfterWave;
            }
        }
    }

    private void InstantiateEnemyObject()
    {
        var enemyObject = enemyWaves[currentWave].Wave[currentSpawn].EnemyObject;
        var positionX = Random.Range(spawnPoint1.x, spawnPoint2.x);
        var positionZ = Random.Range(spawnPoint1.y, spawnPoint2.y);
        Vector3 spawnPosition = new Vector3(positionX, 9, positionZ);
        
        Instantiate(enemyObject, spawnPosition, enemyObject.transform.rotation);
    }
}
