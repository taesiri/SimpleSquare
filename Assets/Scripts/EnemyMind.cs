using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class EnemyMind : MonoBehaviour
    {
        public static Random RandomGenerator = new Random(DateTime.Now.Millisecond);
        public EnemyScript[] Enemies;
        public GameObject EnemyPrefab;
        public int MaxEnemies = 100;
        public float NearDistance = 65f;
        public float TimeThreshold = 2.0f;

        private void Start()
        {
            StartCoroutine(DoSpawn());
        }

        public IEnumerator DoSpawn()
        {
            var es = GetIdleEnemy();
            if (es != null)
            {

                es.EnemyState = EnemyState.Active;
                es.transform.position = GenerateCoordinates();
                
            }

            yield return new WaitForSeconds(TimeThreshold);

            StartCoroutine(DoSpawn());
        }

        public Vector3 GenerateCoordinates()
        {
            var rVec = new Vector3(RandomGenerator.Next(1, 120) - 60, RandomGenerator.Next(0, 72) - 36, 0);

            if (rVec.sqrMagnitude > NearDistance)
            {
                rVec.z = -10;
                return rVec;
            }

            return GenerateCoordinates();
        }

        public EnemyScript GetIdleEnemy()
        {
            for (var i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].EnemyState == EnemyState.Idle)
                {
                    return Enemies[i];
                }
            }
            return null;
        }
    }
}