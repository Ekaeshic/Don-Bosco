using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace DonBosco
{
    public class Pooling : MonoBehaviour
    {
        private static Pooling instance;
        public static Pooling Instance { get { return instance; } }

        [Header("Bullet Pool")]
        [SerializeField] Bullet bulletPrefab;
        [SerializeField] int defaultSize = 10;
        [SerializeField] int maxSize = 100;
        private Action<Bullet> beforeGetBullet;

        private ObjectPool<Bullet> pool;


        void Awake()
        {
            instance = this;
        }


        void Start()
        {
            pool = new ObjectPool<Bullet>(() => {
                return Instantiate(bulletPrefab);
            }, bullet => {
                if(beforeGetBullet != null) beforeGetBullet(bullet);
                bullet.gameObject.SetActive(true);
            }, bullet => {
                bullet.gameObject.SetActive(false);
            }, bullet => {
                if(bullet)
                    Destroy(bullet.gameObject);
            }, false, defaultSize, maxSize);

            Bullet[] bullets = new Bullet[defaultSize];
            for(int i = 0; i < defaultSize; i++)
            {
                bullets[i] = Instantiate(bulletPrefab);
            }
            for(int i = 0; i < defaultSize; i++)
            {
                pool.Release(bullets[i]);
            }
        }

        void OnDestroy()
        {
            pool.Dispose();
        }



        public Bullet GetBullet(Action<Bullet> beforeGet = null)
        {
            beforeGetBullet = beforeGet;
            return pool.Get();
        }

        public void ReturnBullet(Bullet bullet)
        {
            pool.Release(bullet);
        }
    }
}
