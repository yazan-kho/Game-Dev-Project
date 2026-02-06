using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when player loses HP but is still alive.
    /// Respawn to last checkpoint (no dead state).
    /// </summary>
    public class PlayerRespawn : Simulation.Event<PlayerRespawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;

            player.controlEnabled = false;

            
            var spawn = model.checkpoints[model.currentCheckpointIndex];
            player.Teleport(spawn.position);

            player.jumpState = PlayerController.JumpState.Grounded;

            // play hurt but NOT dead
            player.animator.SetTrigger("hurt");
            player.animator.SetBool("dead", false);

            model.virtualCamera.Follow = player.transform;
            model.virtualCamera.LookAt = player.transform;

            // re-enable after short delay
            Simulation.Schedule<EnablePlayerAfterRespawn>(0.6f);
        }
    }

    public class EnablePlayerAfterRespawn : Simulation.Event<EnablePlayerAfterRespawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            player.collider2d.enabled = true;
            player.controlEnabled = true;
        }
    }
}
