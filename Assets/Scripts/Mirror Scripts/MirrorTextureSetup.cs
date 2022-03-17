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

        /// <summary>
        /// Called before Start function
        /// </summary>
        private void Awake ()
        {
            SetupTextures();
        }

        /// <summary>
        /// Render textures
        /// </summary>
        /// <param name="kam"> Camera </param>
        /// <param name="mat"> Material </param>
        private void RenderTexture(Camera kam, Material mat)
        {
            if (kam.targetTexture != null)
                kam.targetTexture.Release();

            kam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            mat.mainTexture = kam.targetTexture;
        }

        /// <summary>
        /// Setup cameras and textures relation
        /// </summary>
        private void SetupTextures()
        {
            for(var i = 0; i < cameras.Length; i++)
                RenderTexture(cameras[i], cameraMaterials[i]);
        }
    }
}
