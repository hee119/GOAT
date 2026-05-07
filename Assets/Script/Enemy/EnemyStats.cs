using UnityEngine;
using UnityEngine.Serialization;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyScriptableObject;
    public string enemyName;
    public float enemyMaxHp;
    public float enemyHp;
    public float enemyDamage;
    public float enemySpeed;
    void Awake()
    {
        enemyName = enemyScriptableObject.enemyName;
        enemyMaxHp = enemyScriptableObject.enemyMaxHp;
        enemyHp = enemyMaxHp;
        enemyDamage = enemyScriptableObject.enemyDamage;
        enemySpeed = enemyScriptableObject.enemySpeed;
    }
}
