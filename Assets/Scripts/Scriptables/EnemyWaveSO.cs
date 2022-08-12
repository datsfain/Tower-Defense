using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Wave", menuName = "Scriptable Objects/EnemyWave")]
public class EnemyWaveSO : ScriptableObject
{
    [System.Serializable]
    public class EnemyAndProportion
    {
        public EnemyTypeSO EnemyType;
        public int Portion;
    }
    public List<EnemyAndProportion> Enemies;
    public float Duration;
    public float SpawnInterval;
}
