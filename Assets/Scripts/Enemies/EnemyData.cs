using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public GameObject EnemyPrefab;
    public int Healh;
    public int Damage;
    public float Speed;
    public float AttackColdown;
    public EnemyType enemyType;
}
