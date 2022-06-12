///<author>ThomasKrahl</author>

using UnityEngine;

namespace IdleGame
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private MeshRenderer mr;
        [SerializeField] private float speed = 0.25f;

        void Update()
        {
            float dist = speed * Time.deltaTime;

            if (dist >= 1)
            {
                dist = 0;
            }

            mr.material.mainTextureOffset += new Vector2(dist, 0f);
        }
    }
}
