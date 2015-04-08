using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyScript : MonoBehaviour
    {
        private EnemyState _enemyState;
        private float _startTime;
        public float LifeTime = 5f;

        public EnemyState EnemyState
        {
            get { return _enemyState; }
            set
            {
                if (value == EnemyState.Active)
                {
                    _startTime = Time.time;
                    renderer.material.color = Color.yellow;
                }

                _enemyState = value;
            }
        }

        public void Start()
        {
            _enemyState = EnemyState.Idle;
        }

        public void Update()
        {
            if (EnemyState == EnemyState.Active)
            {
                if (Time.time - _startTime > LifeTime)
                {
                    _enemyState = EnemyState.Idle;

                    renderer.material.color = Color.red;
                }
            }
        }
    }
}