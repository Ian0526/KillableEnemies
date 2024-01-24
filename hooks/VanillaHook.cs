using GameNetcodeStuff;
using HarmonyLib;
using KillableEnemies.managers;
using KillableEnemies.models;
using KillableEnemies.network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KillableEnemies.hooks
{

    internal class VanillaHook
    {

        [HarmonyPostfix]
        [HarmonyPatch(typeof(EnemyAI), "Start")]
        static void PrefixTeleportPlayer(EnemyAI __instance, int force = 1, PlayerControllerB playerWhoHit = null, bool playHitSFX = false)
        {
            __instance.gameObject.AddComponent<KillableEnemy>();
            switch (__instance)
            {
                case ForestGiantAI forestgiantAI:
                    {
                        KillableEnemy killableEnemy = forestgiantAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.GiantHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.GiantHP;
                        killableEnemy.EnemyAI = forestgiantAI;
                        killableEnemy.EnemyType = models.EnemyType.GIANT;
                        killableEnemy.NetworkObjectId = forestgiantAI.NetworkObjectId;
                        break;
                    }
                case DressGirlAI dressGirlAI:
                    {
                        KillableEnemy killableEnemy = dressGirlAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.GhostGirlHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.GhostGirlHP;
                        killableEnemy.EnemyAI = dressGirlAI;
                        killableEnemy.EnemyType = models.EnemyType.GHOST_GIRL;
                        killableEnemy.NetworkObjectId = dressGirlAI.NetworkObjectId;
                        break;
                    }
                case PufferAI pufferAI:
                    {
                        KillableEnemy killableEnemy = pufferAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.SporeLizardHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.SporeLizardHP;
                        killableEnemy.EnemyAI = pufferAI;
                        killableEnemy.EnemyType = models.EnemyType.SPORE_LIZARD;
                        killableEnemy.NetworkObjectId = pufferAI.NetworkObjectId;
                        break;
                    }
                case SpringManAI springManAI:
                    {
                        KillableEnemy killableEnemy = springManAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.CoilHeadHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.CoilHeadHP;
                        killableEnemy.EnemyAI = springManAI;
                        killableEnemy.EnemyType = models.EnemyType.COIL_HEAD;
                        killableEnemy.NetworkObjectId = springManAI.NetworkObjectId;
                        break;
                    }
                case JesterAI jesterAI:
                    {
                        KillableEnemy killableEnemy = jesterAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.CoilHeadHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.CoilHeadHP;
                        killableEnemy.EnemyAI = jesterAI;
                        killableEnemy.EnemyType = models.EnemyType.JESTER;
                        killableEnemy.NetworkObjectId = jesterAI.NetworkObjectId;
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
                killableEnemy.DoDamage(force);
                // playercontrollers will still have object binded, serverrpc can be called by host & does not require ownership
                playerWhoHit.GetComponent<KillableEnemyEmitter>().DoDamageServerRpc(__instance.NetworkObjectId, force);
            }
        }
    }
}