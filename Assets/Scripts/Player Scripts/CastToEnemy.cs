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
        
        private const float VignetteChangingSpeed = 5f;
        private const float InitialVignette = .25f;
        private const float NewVignette = .5f;

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
            
            if (_distanceToEnemy < 5f)
            {
                ChangeVignette(NewVignette);
                
                // Correct Vignette Value
                if (_vignette.intensity.value + .0005f > NewVignette)
                    _vignette.intensity.value = NewVignette;
            }
            else
            {
                ChangeVignette(InitialVignette);

                // Correct Vignette Value
                if (_vignette.intensity.value + .0005f < InitialVignette)
                    _vignette.intensity.value = InitialVignette;
            }
        }
        
        /// <summary>
        /// Smoothly change vignette
        /// </summary>
        /// <param name="newValue"></param>
        private void ChangeVignette(float newValue)
        {
            _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, newValue, VignetteChangingSpeed * Time.deltaTime);
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
