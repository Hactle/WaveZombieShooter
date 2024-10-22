using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveData", menuName = "Wave")]
public class WaveData : ScriptableObject
{
    public int EnemyCount;
    public EnemyType[] EnemyTypes;
    public float TimeBetweenSpawns;
}
