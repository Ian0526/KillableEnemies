using GameNetcodeStuff;
using KillableEnemies.managers;
using KillableEnemies.network;
using System;
using UnityEngine;

namespace KillableEnemies.models
{

    public class KillableEnemy : MonoBehaviour
    {

        private int startingHealth = 0;
        private int currentHealth = 0;
        private ulong networkObjectId;
        private EnemyType enemyType;
        private EnemyAI enemyAI;

        public EnemyType EnemyType { get => enemyType; set => enemyType = value; }
        public EnemyAI EnemyAI { get => enemyAI; set => enemyAI = value; }
        public ulong NetworkObjectId { get => networkObjectId; set => networkObjectId = value; }
        public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public int StartingHealth { get => startingHealth; set => startingHealth = value; }

        public delegate void DeathEventHandler(KillableEnemy sender, DeathEventArgs e);
        public event DeathEventHandler DeathEvent;

        public delegate void HitEventHandler(KillableEnemy sender, HitEventArgs e);
        public event HitEventHandler HitEvent;

        public void DoDamage(int amount, PlayerControllerB whoHit = null)
        {
            var args = new HitEventArgs(this, amount);
            OnHitEvent(args);

            if (args.Cancel)
            {
                return;
            }

            currentHealth -= amount;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            if (currentHealth == 0)
            {
                if (enemyAI.IsHost || enemyAI.IsServer)
                {
                    StartOfRound.Instance.allPlayerScripts[0].gameObject.GetComponent<KillableEnemyEmitter>().DoKillServerRpc(enemyAI.NetworkObjectId);
                }
            }
        }

        public void DoDeath()
        {
            if (enemyAI.IsHost || enemyAI.IsServer)
            { 
                var args = new DeathEventArgs(this, currentHealth);
                OnDeathEvent(args);

                if (args.Cancel)
                {
                    return;
                }

                EnemyAI.isEnemyDead = true;
                if (enemyAI.IsServer || enemyAI.IsHost)
                {
                    EnemyAI.NetworkObject.Despawn();
                }
            }
        }

        protected virtual void OnDeathEvent(DeathEventArgs e)
        {
            DeathEvent?.Invoke(this, e);
        }

        protected virtual void OnHitEvent(HitEventArgs e)
        {
            HitEvent?.Invoke(this, e);
        }
    }


    public class DeathEventArgs : EventArgs
    {
        public KillableEnemy Enemy { get; private set; }
        public bool Cancel { get; set; } = false;

        public DeathEventArgs(KillableEnemy enemy, int remainingHealth)
        {
            Enemy = enemy;
        }
    }

    public class HitEventArgs : EventArgs
    {
        public KillableEnemy Enemy { get; private set; }
        public int DamageDealt { get; private set; }
        public bool Cancel { get; set; } = false;

        public HitEventArgs(KillableEnemy enemy, int damageDealt)
        {
            Enemy = enemy;
            DamageDealt = damageDealt;
        }
    }

    public enum EnemyType
    {
        COIL_HEAD,
        GHOST_GIRL,
        GIANT,
        SPORE_LIZARD,
        JESTER
    }
}
