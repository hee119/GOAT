using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/EnemyData")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Enemy Stats")]
    public string enemyName;
    public float hp;
    public float damage;
    public float moveSpeed;
}