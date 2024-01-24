using KillableEnemies.managers;
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

        public void DoDamage(int amount)
        {
            var args = new HitEventArgs();
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
                DoDeath();
            }
        }

        public void DoDeath()
        {
            var args = new DeathEventArgs();
            OnDeathEvent(args);

            if (args.Cancel)
            {
                return;
            }

            EnemyAI.NetworkObject.Despawn();
            EnemyAI.isEnemyDead = true;

            KillableEnemyManager.Instance.EnemyAIToNetworkObjectID.Remove(enemyAI);
            KillableEnemyManager.Instance.NetworkObjectIDToEnemyAI.Remove(enemyAI.NetworkObjectId);
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
        public bool Cancel { get; set; } = false;
    }

    public class HitEventArgs : EventArgs
    {
        public bool Cancel { get; set; } = false;
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
