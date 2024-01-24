using HarmonyLib;
using KillableEnemies.managers;
using KillableEnemies.models;
using Unity.Netcode;

namespace KillableEnemies.network
{
    public class KillableEnemyEmitter : NetworkBehaviour
    {
        [ServerRpc(RequireOwnership = false)]
        public void DoDamageServerRpc(ulong objectId, int damage)
        {
            DoDamageClientRpc(objectId, damage);
        }

        [ServerRpc(RequireOwnership = false)]
        public void DoKillServerRpc(ulong objectId)
        {
            DoKillClientRpc(objectId);
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
        public void DoKillClientRpc(ulong objectId)
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
