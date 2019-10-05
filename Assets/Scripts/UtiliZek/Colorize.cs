using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtiliZek
{
    public class Colorize : MonoBehaviour
    {
        public Material colorMaterial;
        public Renderer[] renderers;

        void Start()
        {
            foreach (Renderer renderer in renderers)
                renderer.material = colorMaterial;
        }
    }
}