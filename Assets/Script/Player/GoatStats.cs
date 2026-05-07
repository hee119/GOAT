using UnityEngine;
using TMPro;
public class GoatStats : MonoBehaviour
{
    public float playerMaxHp;
    public float playerHp;
    public GameObject spawnPoint;
    public TextMeshProUGUI dieText;
    public GameObject reSpawnButton;
    private string _lastAttackEnemyName;

    void Awake()
    {
        playerHp = playerMaxHp;
    }
    void Update()
    {
        
    }

    public void Hit(float damage, string lastAttackEnemyName)
    {
        _lastAttackEnemyName = lastAttackEnemyName;
        playerHp -= damage;
        Debug.Log($"{_lastAttackEnemyName} is hit you");
        if (playerHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Time.timeScale = 0;
        dieText.enabled = true;
        reSpawnButton.SetActive(true);
        dieText.text = $"{_lastAttackEnemyName} is kill you";
        Debug.Log("you Die");
    }

    public void Respawn()
    {
        Time.timeScale = 1; 
        dieText.enabled = false;
        reSpawnButton.SetActive(false);
        playerHp = playerMaxHp;
        transform.position = spawnPoint.transform.position;
    }
}
