using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using KillableEnemies.managers;
using KillableEnemies.models;
using KillableEnemies.network;
using System;

namespace KillableEnemies.hooks
{

    internal class VanillaHook
    {

        private const string modGUID = "Ovchinikov.KillableEnemies.Vanilla";

        private static ManualLogSource mls;

        static VanillaHook()
        {
            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(EnemyAI), "Start")]
        static void PostfixStartEnemy(EnemyAI __instance)
        {
            switch (__instance)
            {
                case ForestGiantAI forestgiantAI:
                    {
                        if (!KillableEnemyManager.Instance.DoGiantKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = forestgiantAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.GiantHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.GiantHP;
                        killableEnemy.EnemyAI = forestgiantAI;
                        killableEnemy.EnemyType = models.EnemyType.GIANT;
                        killableEnemy.NetworkObjectId = forestgiantAI.NetworkObjectId;
                        mls.LogInfo("Found a Forest Giant. Registering killable data.");
                        break;
                    }
                case DressGirlAI dressGirlAI:
                    {
                        if (!KillableEnemyManager.Instance.DoGhostGirlKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = dressGirlAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.GhostGirlHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.GhostGirlHP;
                        killableEnemy.EnemyAI = dressGirlAI;
                        killableEnemy.EnemyType = models.EnemyType.GHOST_GIRL;
                        killableEnemy.NetworkObjectId = dressGirlAI.NetworkObjectId;
                        mls.LogInfo("Found a Ghost Girl. Registering killable data.");
                        break;
                    }
                case PufferAI pufferAI:
                    {
                        if (!KillableEnemyManager.Instance.DoSporeLizardKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = pufferAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.SporeLizardHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.SporeLizardHP;
                        killableEnemy.EnemyAI = pufferAI;
                        killableEnemy.EnemyType = models.EnemyType.SPORE_LIZARD;
                        killableEnemy.NetworkObjectId = pufferAI.NetworkObjectId;
                        mls.LogInfo("Found a Spore Lizard. Registering killable data.");
                        break;
                    }
                case SpringManAI springManAI:
                    {
                        if (!KillableEnemyManager.Instance.DoCoilHeadKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = springManAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.CoilHeadHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.CoilHeadHP;
                        killableEnemy.EnemyAI = springManAI;
                        killableEnemy.EnemyType = models.EnemyType.COIL_HEAD;
                        killableEnemy.NetworkObjectId = springManAI.NetworkObjectId;
                        mls.LogInfo("Found a Coilhead. Registering killable data.");
                        break;
                    }
                case JesterAI jesterAI:
                    {
                        if (!KillableEnemyManager.Instance.DoJesterKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = jesterAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.CoilHeadHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.CoilHeadHP;
                        killableEnemy.EnemyAI = jesterAI;
                        killableEnemy.EnemyType = models.EnemyType.JESTER;
                        killableEnemy.NetworkObjectId = jesterAI.NetworkObjectId;
                        mls.LogInfo("Found a Jester. Registering killable data.");
                        break;
                    }
            }
        }

        // for vanilla mele attacks
        [HarmonyPostfix]
        [HarmonyPatch(typeof(EnemyAI), "HitEnemy")]
        static void HitPostfix(EnemyAI __instance, int force = 1, PlayerControllerB playerWhoHit = null, bool playHitSFX = false)
        {
            if (!__instance.IsHost && !__instance.IsServer) return;
            KillableEnemy killableEnemy = __instance.gameObject.GetComponent<KillableEnemy>();
            if (killableEnemy)
            {
                StartOfRound.Instance.allPlayerScripts[0].GetComponent<KillableEnemyEmitter>().DoDamageServerRpc(__instance.NetworkObjectId, force);
            }
        }

        private static void InitializeKillableData(EnemyAI enemyAI)
        {
            enemyAI.gameObject.AddComponent<KillableEnemy>();
            KillableEnemyManager.Instance.EnemyAIToNetworkObjectID[enemyAI] = enemyAI.NetworkObjectId;
            KillableEnemyManager.Instance.NetworkObjectIDToEnemyAI[enemyAI.NetworkObjectId] = enemyAI;
        }
    }
}