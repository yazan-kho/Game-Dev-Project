using System;
using Platformer.Gameplay;
using Unity.VisualScripting;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        public GameObject[] livesIcons; // Array of UI icons representing the player's lives
        public GameObject playerDeadMessage; // UI element to display when the player dies
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 3;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        int currentHP;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            UpdateLivesUI();
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            //if (Time.time < nextDamageTime) return;
            //nextDamageTime = Time.time + invincibleTime;

            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            UpdateLivesUI();

            if (currentHP > 0) Schedule<PlayerRespawn>();
            else { var ev = Schedule<HealthIsZero>(); ev.health = this; }
        }


        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0)
            {
                Decrement();
            }
        }

        void Awake()
        {
            currentHP = maxHP;
            ResetToFull();
        }

        public void ResetToFull()
        {
            currentHP = maxHP;
            UpdateLivesUI();
        }
        void UpdateLivesUI()
        {
            if (livesIcons == null) return;

            for (int i = 0; i < livesIcons.Length; i++)
            {
                if (livesIcons[i] != null)
                    livesIcons[i].SetActive(i < currentHP);
            }
        }

        public void ShowPlayerDeadMessage(bool value)
        {
            if (playerDeadMessage != null)
                playerDeadMessage.SetActive(value);
        }
    }
}
