using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
   private GoatHP _goatHp;
   public float enemyDamage;
   void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Player"))
      {
         if (_goatHp == null)
         {
            collision.gameObject.TryGetComponent(out _goatHp);
         }

         _goatHp.Hit(enemyDamage, gameObject.name);
      }
   }
}
