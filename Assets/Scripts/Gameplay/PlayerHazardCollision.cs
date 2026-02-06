using Platformer.Core;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;
namespace Platformer.Gameplay
{
    public class PlayerHazardCollision : Simulation.Event<PlayerHazardCollision>
    {
        public PlayerController player;

        public override void Execute()
        {
            Schedule<PlayerDeath>();
        }
    }
}
