using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using KillableEnemies.hooks;
using KillableEnemies.network;
using RuntimeNetcodeRPCValidator;
using System.Reflection;
using UnityEngine;

namespace KillableEnemies
{
    public class KillableEnemies
    {
        private const string modGUID = "Ovchinikov.KillableEnemies.Main";
        private const string modName = "KillableEnemies";
        private const string modVersion = "0.1.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static KillableEnemies instance;

        private NetcodeValidator netcodeValidator;

        internal ManualLogSource mls;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("Enabling KillableEnemies");

            InitializeConfigValues();

            harmony.PatchAll(typeof(VanillaHook));

            netcodeValidator = new NetcodeValidator(modGUID);
            netcodeValidator.PatchAll();
            netcodeValidator.BindToPreExistingObjectByBehaviour<KillableEnemyEmitter, PlayerControllerB>();

            mls.LogInfo("Finished Enabling KillableEnemies");
        }

        private void InitializeConfigValues()
        {
            mls.LogInfo("Parsing KillableEnemies config");

            mls.LogInfo("Config finished parsing");
        }
    }
}