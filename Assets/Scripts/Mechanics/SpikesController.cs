using Platformer.Gameplay;
using Platformer.Mechanics;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class SpikesController : MonoBehaviour
    {
        [SerializeField] private float minY = -1.52f;
        [SerializeField] private float maxY = -0.14f;
        [SerializeField] private float travelTime = 1.0f;

        private Vector3 startPos;

        void Awake()
        {
            startPos = transform.position;
        }

        void Update()
        {
            // 0..1..0..1..
            float t = Mathf.PingPong(Time.time / Mathf.Max(0.01f, travelTime), 1f);

            // smooth easing (no hard turns)
            t = Mathf.SmoothStep(0f, 1f, t);

            float y = Mathf.Lerp(minY, maxY, t);
            transform.position = new Vector3(startPos.x, y, startPos.z);
        }
    
        void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                var ev = Schedule<PlayerHazardCollision>();
                ev.player = player;
            }
        }
    }
}
