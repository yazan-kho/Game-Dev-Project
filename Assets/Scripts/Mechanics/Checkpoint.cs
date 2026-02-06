using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class Checkpoint : MonoBehaviour
    {
        public int checkpointIndex;
        PlatformerModel model;

        void Awake() => model = Simulation.GetModel<PlatformerModel>();

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerController>() == null) return;

            if (checkpointIndex > model.currentCheckpointIndex)
                model.currentCheckpointIndex = checkpointIndex;
        }
    }
}
