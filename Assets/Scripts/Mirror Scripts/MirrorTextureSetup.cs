using UnityEngine;

namespace Mirror_Scripts
{
    // Adds the right texture to be rendered by the right camera
    public class MirrorTextureSetup : MonoBehaviour
    {
        // Variables
        [SerializeField] private Camera[] cameras;
        [SerializeField] private Material[] cameraMaterials;

        private Resolution _currentResolution;
        
        // Called before Start function
        private void Awake ()
        {
            _currentResolution = Screen.currentResolution;
            
            SetupTextures();
        }
        
        // Update is called once per frame
        private void Update()
        {
            RefreshTextures();
        }
        
        // Refresh Mirror Textures at screen resolution change
        private void RefreshTextures()
        {
            if (_currentResolution.width == Screen.width && _currentResolution.height == Screen.height) return;
            
            SetupTextures();
                
            _currentResolution = Screen.currentResolution;
        }
        
        // Setup cameras and textures relation
        private void SetupTextures()
        {
            for(var i = 0; i < cameras.Length; i++)
                RenderTexture(cameras[i], cameraMaterials[i]);
        }
        
        // Render textures
        private static void RenderTexture(Camera kam, Material mat)
        {
            // If camera from mirror has a texture, delete it
            if (kam.targetTexture != null)
                kam.targetTexture.Release();

            // Apply texture to camera
            kam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            mat.mainTexture = kam.targetTexture;
        }
    }
}
