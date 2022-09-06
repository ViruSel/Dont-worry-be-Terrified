using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Player_Scripts
{
    public class CastToEnemy : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private Transform enemy;
        [SerializeField] private VolumeProfile volumeProfile;

        private float _distanceToEnemy;

        private Vignette _vignette;
        
        /// <summary>
        /// Called once per frame
        /// </summary>
        private void Update()
        {
            CheckDistance();
        }

        /// <summary>
        /// Cast a ray to find the distance between player and objects
        /// </summary>
        private void CheckDistance()
        {
            _distanceToEnemy = (enemy.position - transform.position).magnitude;

            if (!volumeProfile.TryGet(out _vignette)) return;
            
            if (_distanceToEnemy < PlayerProperties.CAST_TO_ENEMY_DISTANCE)
            {
                ChangeVignette(PlayerProperties.VIGNETTE_NEW_VALUE);
                
                // Correct Vignette Value
                if (_vignette.intensity.value + .0005f > PlayerProperties.VIGNETTE_NEW_VALUE)
                    _vignette.intensity.value = PlayerProperties.VIGNETTE_NEW_VALUE;
            }
            else
            {
                ChangeVignette(PlayerProperties.VIGNETTE_DEFAULT_VALUE);

                // Correct Vignette Value
                if (_vignette.intensity.value + .0005f < PlayerProperties.VIGNETTE_DEFAULT_VALUE)
                    _vignette.intensity.value = PlayerProperties.VIGNETTE_DEFAULT_VALUE;
            }
        }
        
        /// <summary>
        /// Smoothly change vignette
        /// </summary>
        /// <param name="newValue"></param>
        private void ChangeVignette(float newValue)
        {
            _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, newValue, PlayerProperties.VIGNETTE_CHANGING_SPEED * Time.deltaTime);
        }
        
        /// <summary>
        /// Things to do on collision
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
                ChangeVignette(1f);
        }
    }
}
