using HarmonyLib;
using KillableEnemies.managers;
using KillableEnemies.models;
using Unity.Netcode;

namespace KillableEnemies.network
{
    public class KillableEnemyEmitter : NetworkBehaviour
    {
        [ServerRpc]
        public void DoDamageServerRpc(ulong objectId, int damage)
        {
            DoDamageClientRpc(objectId, damage);
        }

        [ServerRpc]
        public void DoKillServerRpc(ulong objectId, int damage)
        {
            DoKillClientRpc(objectId, damage);
        }

        [ClientRpc]
        public void DoDamageClientRpc(ulong objectId, int damage)
        {
            EnemyAI enemyAI = KillableEnemyManager.Instance.NetworkObjectIDToEnemyAI.GetValueSafe(objectId);
            KillableEnemy killableEnemy = enemyAI.gameObject.GetComponent<KillableEnemy>();
            if (killableEnemy != null)
            {
                enemyAI.gameObject.GetComponent<KillableEnemy>().DoDamage(damage);
            }
        }

        [ClientRpc]
        public void DoKillClientRpc(ulong objectId, int damage)
        {
            EnemyAI enemyAI = KillableEnemyManager.Instance.NetworkObjectIDToEnemyAI.GetValueSafe(objectId);
            KillableEnemy killableEnemy = enemyAI.gameObject.GetComponent<KillableEnemy>();
            if (killableEnemy != null)
            {
                enemyAI.gameObject.GetComponent<KillableEnemy>().DoDeath();
            }
        }
    }
}
