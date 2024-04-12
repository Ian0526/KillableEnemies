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

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayerControllerB), "Awake")]
        static void onAwake(PlayerControllerB __instance)
        {
            if (__instance.gameObject.GetComponent<KillableEnemyEmitter>() == null)
            {
                __instance.gameObject.AddComponent<KillableEnemyEmitter>();
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayerControllerB), "Start")]
        static void onStart(PlayerControllerB __instance)
        {
            if (__instance.gameObject.GetComponent<KillableEnemyEmitter>() == null)
            {
                __instance.gameObject.AddComponent<KillableEnemyEmitter>();
            }
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
                case BaboonBirdAI baboonBirdAI:
                    {
                        if (!KillableEnemyManager.Instance.DoBaboonBirdKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = baboonBirdAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.BaboonBirdHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.BaboonBirdHP;
                        killableEnemy.EnemyAI = baboonBirdAI;
                        killableEnemy.EnemyType = models.EnemyType.BABOON_BIRD;
                        killableEnemy.NetworkObjectId = baboonBirdAI.NetworkObjectId;
                        mls.LogInfo("Found a Baboon Bird. Registering killable data.");
                        break;
                    }
                case ButlerEnemyAI butlerEnemyAI:
                    {
                        if (!KillableEnemyManager.Instance.DoButlerEnemyKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = butlerEnemyAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.ButlerEnemyHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.ButlerEnemyHP;
                        killableEnemy.EnemyAI = butlerEnemyAI;
                        killableEnemy.EnemyType = models.EnemyType.BUTLER_ENEMY;
                        killableEnemy.NetworkObjectId = butlerEnemyAI.NetworkObjectId;
                        mls.LogInfo("Found a Butler Enemy. Registering killable data.");
                        break;
                    }

                case CrawlerAI crawlerAI:
                    {
                        if (!KillableEnemyManager.Instance.DoCrawlerKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = crawlerAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.CrawlerHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.CrawlerHP;
                        killableEnemy.EnemyAI = crawlerAI;
                        killableEnemy.EnemyType = models.EnemyType.CRAWLER;
                        killableEnemy.NetworkObjectId = crawlerAI.NetworkObjectId;
                        mls.LogInfo("Found a Crawler. Registering killable data.");
                        break;
                    }
                case HoarderBugAI hoarderBugAI:
                    {
                        if (!KillableEnemyManager.Instance.DoHoarderBugKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = hoarderBugAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.HoarderBugHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.HoarderBugHP;
                        killableEnemy.EnemyAI = hoarderBugAI;
                        killableEnemy.EnemyType = models.EnemyType.HOARDER_BUG;
                        killableEnemy.NetworkObjectId = hoarderBugAI.NetworkObjectId;
                        mls.LogInfo("Found a Hoarder Bug. Registering killable data.");
                        break;
                    }
                case LassoManAI lassoManAI:
                    {
                        if (!KillableEnemyManager.Instance.DoLassoManKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = lassoManAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.LassoManHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.LassoManHP;
                        killableEnemy.EnemyAI = lassoManAI;
                        killableEnemy.EnemyType = models.EnemyType.LASSO_MAN;
                        killableEnemy.NetworkObjectId = lassoManAI.NetworkObjectId;
                        mls.LogInfo("Found a Lasso Man. Registering killable data.");
                        break;
                    }
                case MouthDogAI mouthDogAI:
                    {
                        if (!KillableEnemyManager.Instance.DoMouthDogKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = mouthDogAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.MouthDogHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.MouthDogHP;
                        killableEnemy.EnemyAI = mouthDogAI;
                        killableEnemy.EnemyType = models.EnemyType.MOUTH_DOG;
                        killableEnemy.NetworkObjectId = mouthDogAI.NetworkObjectId;
                        mls.LogInfo("Found a Mouth Dog. Registering killable data.");
                        break;
                    }
                case NutcrackerEnemyAI nutcrackerEnemyAI:
                    {
                        if (!KillableEnemyManager.Instance.DoNutcrackerEnemyKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = nutcrackerEnemyAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.NutcrackerEnemyHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.NutcrackerEnemyHP;
                        killableEnemy.EnemyAI = nutcrackerEnemyAI;
                        killableEnemy.EnemyType = models.EnemyType.NUTCRACKER_ENEMY;
                        killableEnemy.NetworkObjectId = nutcrackerEnemyAI.NetworkObjectId;
                        mls.LogInfo("Found a Nutcracker Enemy. Registering killable data.");
                        break;
                    }
                case RadMechAI radMechAI:
                    {
                        if (!KillableEnemyManager.Instance.DoRadMechKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = radMechAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.RadMechHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.RadMechHP;
                        killableEnemy.EnemyAI = radMechAI;
                        killableEnemy.EnemyType = models.EnemyType.RAD_MECH;
                        killableEnemy.NetworkObjectId = radMechAI.NetworkObjectId;
                        mls.LogInfo("Found a Rad Mech. Registering killable data.");
                        break;
                    }
                case SandSpiderAI sandSpiderAI:
                    {
                        if (!KillableEnemyManager.Instance.DoSandSpiderKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = sandSpiderAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.SandSpiderHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.SandSpiderHP;
                        killableEnemy.EnemyAI = sandSpiderAI;
                        killableEnemy.EnemyType = models.EnemyType.SAND_SPIDER;
                        killableEnemy.NetworkObjectId = sandSpiderAI.NetworkObjectId;
                        mls.LogInfo("Found a Sand Spider. Registering killable data.");
                        break;
                    }
                case CentipedeAI centipedeAI:
                    {
                        if (!KillableEnemyManager.Instance.DoCentipedeKill) return;
                        InitializeKillableData(__instance);
                        KillableEnemy killableEnemy = centipedeAI.gameObject.GetComponent<KillableEnemy>();
                        killableEnemy.StartingHealth = KillableEnemyManager.Instance.CentipedeHP;
                        killableEnemy.CurrentHealth = KillableEnemyManager.Instance.CentipedeHP;
                        killableEnemy.EnemyAI = centipedeAI;
                        killableEnemy.EnemyType = models.EnemyType.CENTIPEDE;
                        killableEnemy.NetworkObjectId = centipedeAI.NetworkObjectId;
                        mls.LogInfo("Found a Centipede. Registering killable data.");
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
            if (killableEnemy && playerWhoHit)
            {
                playerWhoHit.GetComponent<KillableEnemyEmitter>().DoDamageServerRpc(__instance.NetworkObjectId, force);
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