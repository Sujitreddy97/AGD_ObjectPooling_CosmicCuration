using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

namespace CosmicCuration.Enemy
{
    public class EnemyPool
    {
        private EnemyView enemyView;
        private EnemyData enemyData;
        private List<PooledEnemy> pooledEnemies = new List<PooledEnemy>();

        public EnemyPool(EnemyView enemyView, EnemyData enemyData)
        {
            this.enemyView = enemyView;
            this.enemyData = enemyData;
        }

        public EnemyController GetEnemy()
        {
            if(pooledEnemies.Count > 0)
            {
                PooledEnemy pooledEnemy = pooledEnemies.Find(item => !item.isUsed);
                if (pooledEnemy != null)
                {
                    pooledEnemy.isUsed = true;
                    return pooledEnemy.enemy;
                } 
            }

            return CreateNewPooledEnemy();
        }

        public void ReturnEnemyToPool(EnemyController returnedEnemy)
        {
            PooledEnemy pooledEnemy = pooledEnemies.Find(item => item.enemy.Equals(returnedEnemy));
            pooledEnemy.isUsed = false;
        }

        private EnemyController CreateNewPooledEnemy()
        {
            PooledEnemy newEnemy = new PooledEnemy();
            newEnemy.enemy = new EnemyController(enemyView, enemyData);
            newEnemy.isUsed = true;
            pooledEnemies.Add(newEnemy);

            return newEnemy.enemy;
        }

        public class PooledEnemy
        {
            public EnemyController enemy;
            public bool isUsed;
        }
    }
}