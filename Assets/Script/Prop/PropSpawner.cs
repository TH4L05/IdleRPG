///<author>ThomasKrahl</author>

using System.Collections.Generic;
using UnityEngine;

using IdleGame.Unit;
using IdleGame.Unit.Stats;

namespace IdleGame
{
    public class PropSpawner : MonoBehaviour
    {
        [SerializeField] private Transform propRootObj;
        [SerializeField] private List<GameObject> propTemplates = new List<GameObject>();
        [SerializeField] private List<GameObject> activeProps = new List<GameObject>();
        [SerializeField][Range(0, 100)] private int spawnFactorMin = 5;
        [SerializeField][Range(0, 100)] private int spawnFactorMax = 25;
        private int spawnFactor;

        private void Awake()
        {
            spawnFactor = spawnFactorMax;
            Prop.PropDeleted += PropDeleted;
            UnitStats.UpdateModifiedStatByName += CheckSpawn;
        }

        private void OnDestroy()
        {
            Prop.PropDeleted -= PropDeleted;
            UnitStats.UpdateModifiedStatByName -= CheckSpawn;
        }


        public void SpawnProp()
        {
            var randomIndex = UnityEngine.Random.Range(0, propTemplates.Count);
            var prop = Instantiate(propTemplates[randomIndex], transform.position, Quaternion.identity);
            if(propRootObj != null) prop.transform.SetParent(propRootObj);
            prop.GetComponent<Prop>().SetState(Game.Instance.PlayerState);
            activeProps.Add(prop);
        }

        private void PropDeleted(GameObject obj)
        {
            activeProps.Remove(obj);
        }

        private void CheckSpawn(string name, float distance)
        {
            if (name != "Distance") return;

            if ((int)distance %spawnFactor == 0 && distance > 0)
            {
                SpawnProp();
                spawnFactor = UnityEngine.Random.Range(spawnFactorMin, spawnFactorMax + 1);
            }
        }

        public void SetPlayerStateToProps(UnitState state)
        {
            if (activeProps.Count == 0) return;

            foreach (var prop in activeProps)
            {
                prop.GetComponent<Prop>().SetState(state);
            }
        }
    }
}


