using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyScript : MonoBehaviour
    {
        private EnemyState _enemyState;
        private float _startTime;
        public bool DestroyWithTime = false;
        public float LifeTime = 5f;
        public float Speed = 20.0f;
        public bool StaticEnemies = false;

        public EnemyState EnemyState
        {
            get { return _enemyState; }
            set
            {
                if (value == EnemyState.Active)
                {
                    _startTime = Time.time;
                    renderer.material.color = Color.yellow;
                    rigidbody.isKinematic = false;
                }
                else if (value == EnemyState.Idle)
                {
                    renderer.material.color = Color.red;
                    _enemyState = EnemyState.Idle;
                }

                rigidbody.velocity = new Vector3(0f, 0f, 0f);
                rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);

                _enemyState = value;
            }
        }

        public void Start()
        {
            EnemyState = EnemyState.Idle;
            rigidbody.isKinematic = true;
        }

        public void Update()
        {
            if (EnemyState == EnemyState.Active)
            {
                if (DestroyWithTime)
                {
                    if (Time.time - _startTime > LifeTime)
                    {
                        EnemyState = EnemyState.Idle;
                    }
                }
                if (!StaticEnemies)
                    ApplyMovement();
            }

            Debug.DrawLine(transform.position, rigidbody.velocity);
        }

        public void ApplyMovement()
        {
            rigidbody.AddForce(-transform.position, ForceMode.Acceleration);
            rigidbody.velocity = rigidbody.velocity.normalized*Speed;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "SimpleSquare")
            {
                EnemyState = EnemyState.Idle;
            }
        }
    }
}