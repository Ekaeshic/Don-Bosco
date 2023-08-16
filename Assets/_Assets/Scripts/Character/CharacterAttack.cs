using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DonBosco.Character
{
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyLayer;
        [Header("Settings")]
        [SerializeField] private float fireDelay = 0.5f;

        private bool readyToFire = true;

        
        public void Attack(float angle, Vector3 startPosition)
        {
            if(!readyToFire) return;
            Bullet bullet = Pooling.Instance.GetBullet();
            bullet.SetOwner(gameObject);
            bullet.SetAngle(angle);
            bullet.SetStartPosition(startPosition);
            bullet.SetDamageMask(enemyLayer);

            // Start the fireTimer
            readyToFire = false;
            FireDelay();
        }

        // Countdown the fireTimer
        private async void FireDelay()
        {
            await System.Threading.Tasks.Task.Delay((int)(fireDelay * 1000));
            readyToFire = true;
        }
    }
}