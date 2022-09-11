using UnityEngine;

namespace Mirror_Scripts
{
    /// <summary>
    /// Adds the right texture to be rendered by the right camera
    /// </summary>
    public class MirrorTextureSetup : MonoBehaviour
    {
        /// <summary>
        /// Variables
        /// </summary>
        [SerializeField] private Camera[] cameras;
        [SerializeField] private Material[] cameraMaterials;

        private Resolution _currentResolution;
        
        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake ()
        {
            _currentResolution = Screen.currentResolution;
            
            SetupTextures();
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            RefreshTextures();
        }

        /// <summary>
        /// Refresh Mirror Textures at screen resolution change
        /// </summary>
        private void RefreshTextures()
        {
            if (_currentResolution.width == Screen.width && _currentResolution.height == Screen.height) return;
            
            SetupTextures();
                
            _currentResolution = Screen.currentResolution;
        }

        /// <summary>
        /// Setup cameras and textures relation
        /// </summary>
        private void SetupTextures()
        {
            for(var i = 0; i < cameras.Length; i++)
                RenderTexture(cameras[i], cameraMaterials[i]);
        }
        
        /// <summary>
        /// Render textures
        /// </summary>
        /// <param name="kam"> Camera </param>
        /// <param name="mat"> Material </param>
        private static void RenderTexture(Camera kam, Material mat)
        {
            if (kam.targetTexture != null)
                kam.targetTexture.Release();

            kam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            mat.mainTexture = kam.targetTexture;
        }
    }
}
