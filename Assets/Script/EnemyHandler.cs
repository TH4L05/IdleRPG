///<author>ThomasKrahl</author>

using System.Collections.Generic;
using UnityEngine;

using IdleGame.Unit;
using IdleGame.Unit.Stats;

namespace IdleGame
{
    public class EnemyHandler : MonoBehaviour
    {
        #region SerializedFields

        [SerializeField] private Transform unitRootObj;
        [SerializeField] private GameObject spawnPoint;
        [SerializeField] private List<GameObject> enemyTemplates = new List<GameObject>();
        [SerializeField] private List<GameObject> activeEnemiesList = new List<GameObject>();
        [SerializeField][Range(0, 100)] private int spawnFactorMin = 25;
        [SerializeField][Range(0, 200)] private int spawnFactorMax = 100;
        private int spawnFactor;

        #endregion

        #region PrivateFields

        private int enemyCount = 0;

        #endregion


        #region UNityFunctions

        private void Awake()
        {         
            spawnFactor = spawnFactorMin;
            Enemy.EnemyIsDead += EnemyDied;
            UnitStats.UpdateModifiedStatByName += CheckSpawn;
        }

        private void OnDestroy()
        {
            Enemy.EnemyIsDead -= EnemyDied;
            UnitStats.UpdateModifiedStatByName -= CheckSpawn;
        }

        #endregion

        public void SetPlayerStateToEnemies(UnitState state)
        {
            if (enemyCount == 0) return;

            foreach (var enemy in activeEnemiesList)
            {
                enemy.GetComponent<Enemy>().SetState(state);
            }
        }

        private void EnemyDied(GameObject obj)
        {
            activeEnemiesList.Remove(obj);
        }

        public void RemoveAllActiveEnemies()
        {
            foreach (var enemy in activeEnemiesList)
            {
                Destroy(enemy.gameObject, 0.1f);
            }
            activeEnemiesList.Clear();
            enemyCount = 0;
        }

        public void SpawnEnemy()
        {
            var randomIndex = UnityEngine.Random.Range(0, enemyTemplates.Count);
            var newEnemyObj = Instantiate(enemyTemplates[randomIndex], spawnPoint.transform.position, Quaternion.identity);
            activeEnemiesList.Add(newEnemyObj);
            enemyCount++;

            if (unitRootObj != null) newEnemyObj.transform.SetParent(unitRootObj);
            var enemy = newEnemyObj.GetComponent<Enemy>();
            enemy.SetState(Game.Instance.PlayerState);
            enemy.Setup();

        }

        private void CheckSpawn(string name, float distance)
        {
            if (name != "Distance") return;

            if ((int)distance %spawnFactor == 0 && distance > 0)
            {
                //int rand = UnityEngine.Random.Range(1, 11);
                //Debug.Log(rand);

                SpawnEnemy();
                spawnFactor = UnityEngine.Random.Range(spawnFactorMin, spawnFactorMax + 1);
            }
        }
    }
}

