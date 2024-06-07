using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "ScriptableObjects/EnemyWave", order = 2)]
public class EnemyWave : ScriptableObject
{
    public EnemySpawn[] Wave;
}
