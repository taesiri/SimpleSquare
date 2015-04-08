using UnityEngine;

namespace Assets.Scripts
{
    public class GameCoreLogic : MonoBehaviour
    {
        private float _lastFire;
        private float _projectileSpeed = 10.0f;
        private float _timeBetweenFire = 0.4f;
        public float FireRotationThreshold = 6.5f;
        public GameObject Projectile;
        public float RotationSpeed = 10.0f;
        public CubeScript TheCube;

        private void Start()
        {
            if (TheCube == null)
            {
                Debug.LogError("Cube is missing!");
            }
            else
            {
                _timeBetweenFire = TheCube.FireRate;
                _projectileSpeed = TheCube.FireProjectileSpeed;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


                var targetZ = Mathf.Atan2(pos.y, pos.x)*180/Mathf.PI - 90f;

                var delta = Mathf.Abs(TheCube.transform.eulerAngles.z - targetZ);
                if (delta > 350)
                    delta = Mathf.Abs(delta - 360);

                if (delta > FireRotationThreshold)
                {
                    TheCube.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(TheCube.transform.eulerAngles.z, targetZ, Time.deltaTime*RotationSpeed));
                }
                else
                {
                    ReadyToFire(pos, targetZ);
                }
            }
        }

        public void ReadyToFire(Vector3 pos, float zTarget)
        {
            if (Time.time - _lastFire > _timeBetweenFire)
            {
                Fire(new Vector3(pos.x, pos.y, 0).normalized, zTarget);
            }
        }

        public void Fire(Vector3 traget, float zTarget)
        {
            _lastFire = Time.time;

            var prjPosition = TheCube.transform.position + traget*2.6f;
            prjPosition.z = -10;

            var pr = (GameObject) Instantiate(Projectile, prjPosition, Quaternion.identity);
            var prscript = pr.GetComponent<ProjectileScript>();

            prscript.Speed = _projectileSpeed;
            prscript.Direction = traget;
            prscript.ZAngle = zTarget;
        }
    }
}