using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
   private GoatStats _goatStats;
   private EnemyStats _enemyStats;

   void Awake()
   {
      _enemyStats = GetComponent<EnemyStats>();
   }

   void Hit(float damage)
   {
      _enemyStats.enemyHp -= damage;
      if (_enemyStats.enemyHp <= 0)
      {
         Die();
      }
   }

   void Die()
   {
      gameObject.SetActive(false);
   }
   void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Player"))
      {
         if (_goatStats == null)
         {
            collision.gameObject.TryGetComponent(out _goatStats);
         }

         _goatStats.Hit(_enemyStats.enemyDamage, _enemyStats.enemyName);
      }
   }
}
