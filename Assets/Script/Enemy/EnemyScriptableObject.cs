using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/EnemyData")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Enemy Stats")]
    public string enemyName;
    public float enemyMaxHp;
    public float enemyDamage;
    public float enemySpeed;
}