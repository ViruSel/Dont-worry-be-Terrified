using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Player_Scripts
{
    public class PlayerCastToEnemy : MonoBehaviour
    {
        // Variables
        [SerializeField] private Transform enemy;
        [SerializeField] private VolumeProfile volumeProfile;

        private float _distanceToEnemy;
        private Vignette _vignette;
        
        // Called once per frame
        private void Update()
        {
            CheckDistance();
        }
        
        // Cast a ray to find the distance between player and objects
        private void CheckDistance()
        {
            _distanceToEnemy = (enemy.position - transform.position).magnitude;

            if (!volumeProfile.TryGet(out _vignette)) return;
            
            if (_distanceToEnemy < PlayerProperties.CastToEnemyDistance)
            {
                ChangeVignette(PlayerProperties.VignetteNewValue);
                
                // Correct Vignette Value
                if (_vignette.intensity.value + PlayerProperties.VignetteCorrection > PlayerProperties.VignetteNewValue)
                    _vignette.intensity.value = PlayerProperties.VignetteNewValue;
            }
            else
            {
                ChangeVignette(PlayerProperties.VignetteDefaultValue);

                // Correct Vignette Value
                if (_vignette.intensity.value + PlayerProperties.VignetteCorrection < PlayerProperties.VignetteDefaultValue)
                    _vignette.intensity.value = PlayerProperties.VignetteDefaultValue;
            }
        }
        
        // Smoothly change vignette
        private void ChangeVignette(float newValue)
        {
            _vignette.intensity.value = Mathf.Lerp(_vignette.intensity.value, newValue, PlayerProperties.VignetteChangingSpeed * Time.deltaTime);
        }
        
        // Things to do on collision
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
                ChangeVignette(1f);
        }
    }
}
