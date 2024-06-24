using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "ScriptableObjects/EnemyWave", order = 2)]
public class EnemyWave : ScriptableObject
{
    public EnemySpawn[] wave;
}
