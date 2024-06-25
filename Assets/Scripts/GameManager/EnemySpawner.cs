using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyWave[] enemyWaves;
    private int currentWave;
    private int currentSpawn;
    private int currentEnemy;

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float warmUpTime;
    [SerializeField] private float waitAfterWave;

    private Coroutine waitToSpawnEnemyCoroutine;

    private void Awake()
    {
        Assert.IsNotNull(enemyWaves, "The enemyWaves is null");
        Assert.IsNotNull(spawnPoints, "The spawnPoints is null");
        Assert.IsTrue(warmUpTime >= 0, "The warmUpTime is negative");
        Assert.IsTrue(waitAfterWave >= 0, "The waitAfterWave is negative");
    }
    
    public void StartSpawning()
    {
        StartCoroutine(WarmUp());
    }

    private IEnumerator WarmUp()
    {
        yield return new WaitForSeconds(warmUpTime);

        EndWarmUp();
    }

    private void EndWarmUp()
    {
        currentWave = 0;
        currentSpawn = 0;
        currentEnemy = 0;

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
        
        if (thisWave[currentSpawn].enemyObject != null) 
        {
            InstantiateEnemyObject();
        }

        if (CanSpawnNextEnemy(thisWave))
        {
            waitToSpawnEnemyCoroutine = StartCoroutine(WaitToSpawnEnemy(thisWave[currentSpawn].intervalTime));
        }
    }
    
    private void InstantiateEnemyObject()
    {
        var enemyObject = enemyWaves[currentWave].wave[currentSpawn].enemyObject;
        
        var randomSpawnPoint = Random.Range(0, spawnPoints.Length - 1);
        Vector3 spawnPosition = new Vector3(spawnPoints[randomSpawnPoint].position.x, 9, spawnPoints[randomSpawnPoint].position.z);
        
        var newEnemy = Instantiate(enemyObject, spawnPosition, enemyObject.transform.rotation);
        
        InstancesManager.Instance.enemies.Add(newEnemy.transform);
    }

    private bool CanSpawnNextEnemy(EnemySpawn[] thisWave)
    {
        currentEnemy++;
        if (currentEnemy >= thisWave[currentSpawn].numberOfEnemies)
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
                    return false;
                }
                
                waitToSpawnEnemyCoroutine = StartCoroutine(WaitToSpawnEnemy(waitAfterWave));
                return false;
            }
        }

        return true;
    }
    
    private IEnumerator WaitToSpawnEnemy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SpawnEnemy();
    }

    public void EndSpawning()
    {
        if (waitToSpawnEnemyCoroutine != null)
        {
            StopCoroutine(waitToSpawnEnemyCoroutine);
        }
    }
}
