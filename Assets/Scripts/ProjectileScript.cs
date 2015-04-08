using UnityEngine;

namespace Assets.Scripts
{
    public class ProjectileScript : MonoBehaviour
    {
        public Vector3 Direction;
        public bool IsCollided;
        public float Speed = 20.0f;
        public float ZAngle;
        public float LifeTime =5f;

        private float _startTime;
        public void Start()
        {
            transform.eulerAngles = new Vector3(0, 0, ZAngle);
            _startTime = Time.time;
        }

        private void Update()
        {
            if (!IsCollided)
                ApplyMovement();

            if (Mathf.Abs(transform.position.x) > 110 || Mathf.Abs(transform.position.y) > 110)
            {
                gameObject.SetActive(false);
            }

            if (Time.time - _startTime > LifeTime)
            {
                gameObject.SetActive(false);
            }
        }

        public void ApplyMovement()
        {
            rigidbody.AddForce(Direction*Speed, ForceMode.Acceleration);
            rigidbody.velocity = rigidbody.velocity.normalized*Speed;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                var enemyScript = collision.gameObject.GetComponent<EnemyScript>();
                enemyScript.EnemyState = EnemyState.Idle;

                IsCollided = true;
            }
        }
    }
}