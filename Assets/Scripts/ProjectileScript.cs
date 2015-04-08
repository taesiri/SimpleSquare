using UnityEngine;

namespace Assets.Scripts
{
    public class ProjectileScript : MonoBehaviour
    {
        public Vector3 Direction;
        public float Speed = 20.0f;
        public float ZAngle;

        public void Start()
        {
            transform.eulerAngles = new Vector3(0, 0, ZAngle);
        }

        private void Update()
        {
            ApplyMovement();

            if (Mathf.Abs(transform.position.x) > 110 || Mathf.Abs(transform.position.y) > 110)
            {
                gameObject.SetActive(false);
            }
        }

        public void ApplyMovement()
        {
            rigidbody.AddForce(Direction*Speed, ForceMode.Acceleration);
            rigidbody.velocity = rigidbody.velocity.normalized*Speed;
        }
    }
}