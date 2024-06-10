using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.Bullets
{
    public class BulletPool
    {
        private BulletView bulletView;
        private BulletScriptableObject bulletScriptableObject;
        private List<PooledBullet> pooledBullets = new List<PooledBullet>();


        public BulletPool(BulletView bulletView, BulletScriptableObject bulletScriptableObject)
        {
            this.bulletView = bulletView;
            this.bulletScriptableObject = bulletScriptableObject;
        }

        public BulletController GetBullet()
        {
            if (pooledBullets.Count > 0)
            {
                PooledBullet pooledBullet = pooledBullets.Find(item => !item.isUsed);
                if (pooledBullet != null)
                {
                    pooledBullet.isUsed = true;
                    return pooledBullet.bullet;
                }
            }
            return CreateNewPooledBullet();
        }

        public void ReturnToBulletPool(BulletController returnedBullet)
        {
            PooledBullet pooledBullet = pooledBullets.Find(item => item.bullet.Equals(returnedBullet));
            pooledBullet.isUsed = false;
        }

        private BulletController CreateNewPooledBullet()
        {
            PooledBullet newBullet = new PooledBullet();
            newBullet.bullet = new BulletController(bulletView, bulletScriptableObject);
            newBullet.isUsed = true;
            pooledBullets.Add(newBullet);

            return newBullet.bullet;
        }


        public class PooledBullet
        {
            public BulletController bullet;
            public bool isUsed;
        }
    }
}