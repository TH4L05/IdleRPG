///<author>ThomasKrahl</author>

using UnityEngine;
using IdleGame.Unit;

namespace IdleGame
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private MeshRenderer mr;
        [SerializeField] private float speed = 0.25f;

        private void Awake()
        {
            Player.PlayerStateChanged += MoveBackground;
        }

        private void OnDestroy()
        {
            Player.PlayerStateChanged -= MoveBackground;
        }

        private void MoveBackground(UnitState state)
        {
            if (state != UnitState.Running) return;

            float dist = speed * Time.deltaTime;

            if (dist >= 1)
            {
                dist = 0;
            }

            mr.material.mainTextureOffset += new Vector2(dist, 0f);
        }
    }
}

