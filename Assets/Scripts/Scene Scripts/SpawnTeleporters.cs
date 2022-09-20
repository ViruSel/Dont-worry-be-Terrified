using System;
using UnityEngine;

namespace Scene_Scripts
{
    public class SpawnTeleporters : MonoBehaviour
    {
        private GameObject[] teleporters;

        private void Start()
        {
            teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
            
            ObjectManager.DisableObjects(teleporters);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ObjectManager.ActivateObjects(teleporters);
            }
        }
    }
}
