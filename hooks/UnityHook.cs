using BepInEx.Logging;
using HarmonyLib;
using KillableEnemies.managers;
using Unity.Netcode;

namespace KillableEnemies.hooks
{
    internal class UnityHook
    {

        private const string modGUID = "Ovchinikov.KillableEnemies.Unity";

        private static ManualLogSource mls;

        static UnityHook()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(NetworkManager), "DespawnObject")]
        static void PrefixDespawn(NetworkSpawnManager __instance, NetworkObject networkObject, bool destroyObject)
        {
            EnemyAI enemyAI = networkObject.GetComponent<EnemyAI>();
            if (enemyAI != null ) 
            {
                KillableEnemyManager.Instance.EnemyAIToNetworkObjectID.Remove(enemyAI);
                KillableEnemyManager.Instance.NetworkObjectIDToEnemyAI.Remove(enemyAI.NetworkObjectId);
                mls.LogInfo("Monster removed from map");
            }
        }
    }
}
