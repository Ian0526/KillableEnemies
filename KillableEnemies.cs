using BepInEx.Configuration;
using BepInEx;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using KillableEnemies.hooks;
using KillableEnemies.network;
using LethalConfig.ConfigItems.Options;
using LethalConfig.ConfigItems;
using LethalConfig;
using RuntimeNetcodeRPCValidator;
using KillableEnemies.managers;

namespace KillableEnemies
{

    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency("NicholaScott.BepInEx.RuntimeNetcodeRPCValidator", BepInDependency.DependencyFlags.HardDependency)]
    public class KillableEnemies : BaseUnityPlugin
    {
        private const string modGUID = "Ovchinikov.KillableEnemies.Main";
        private const string modName = "KillableEnemies";
        private const string modVersion = "0.1.1";

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

            ConfigEntry<bool> killableJesterEntry = this.Config.Bind<bool>("Killable Enemies", "Killable Jester", false, "Should Jesters be killable?");
            ConfigEntry<bool> killableGhostGirlEntry = this.Config.Bind<bool>("Killable Enemies", "Killable Ghost Girl", false, "Should Ghost Girls be killable?");
            ConfigEntry<bool> killableGiantEntry = this.Config.Bind<bool>("Killable Enemies", "Killable Giant", false, "Should Giants be killable?");
            ConfigEntry<bool> killableCoilHeadEntry = this.Config.Bind<bool>("Killable Enemies", "Killable Coil Head", false, "Should Coil Heads be killable?");
            ConfigEntry<bool> killableSporeLizardEntry = this.Config.Bind<bool>("Killable Enemies", "Killable Spore Lizard", false, "Should Spore Lizards be killable?");

            ConfigEntry<int> jesterHPEntry = this.Config.Bind<int>("Enemy Health", "Jester Health", 100, "Health points for Jesters.");
            ConfigEntry<int> ghostGirlHPEntry = this.Config.Bind<int>("Enemy Health", "Ghost Girl Health", 100, "Health points for Ghost Girls.");
            ConfigEntry<int> giantHPEntry = this.Config.Bind<int>("Enemy Health", "Giant Health", 100, "Health points for Giants.");
            ConfigEntry<int> coilHeadHPEntry = this.Config.Bind<int>("Enemy Health", "Coil Head Health", 100, "Health points for Coil Heads.");
            ConfigEntry<int> sporeLizardHPEntry = this.Config.Bind<int>("Enemy Health", "Spore Lizard Health", 100, "Health points for Spore Lizards.");

            IntSliderOptions hpSliderOptions = new IntSliderOptions { RequiresRestart = false, Min = 1, Max = 1000 };

            BoolCheckBoxConfigItem killableJesterCheckBox = new BoolCheckBoxConfigItem(killableJesterEntry);
            BoolCheckBoxConfigItem killableGhostGirlCheckBox = new BoolCheckBoxConfigItem(killableGhostGirlEntry);
            BoolCheckBoxConfigItem killableGiantCheckBox = new BoolCheckBoxConfigItem(killableGiantEntry);
            BoolCheckBoxConfigItem killableCoilHeadCheckBox = new BoolCheckBoxConfigItem(killableCoilHeadEntry);
            BoolCheckBoxConfigItem killableSporeLizardCheckBox = new BoolCheckBoxConfigItem(killableSporeLizardEntry);

            IntSliderConfigItem jesterHPSlider = new IntSliderConfigItem(jesterHPEntry, hpSliderOptions);
            IntSliderConfigItem ghostGirlHPSlider = new IntSliderConfigItem(ghostGirlHPEntry, hpSliderOptions);
            IntSliderConfigItem giantHPSlider = new IntSliderConfigItem(giantHPEntry, hpSliderOptions);
            IntSliderConfigItem coilHeadHPSlider = new IntSliderConfigItem(coilHeadHPEntry, hpSliderOptions);
            IntSliderConfigItem sporeLizardHPSlider = new IntSliderConfigItem(sporeLizardHPEntry, hpSliderOptions);

            LethalConfigManager.AddConfigItem(jesterHPSlider);
            LethalConfigManager.AddConfigItem(ghostGirlHPSlider);
            LethalConfigManager.AddConfigItem(giantHPSlider);
            LethalConfigManager.AddConfigItem(coilHeadHPSlider);
            LethalConfigManager.AddConfigItem(sporeLizardHPSlider);

            LethalConfigManager.AddConfigItem(killableJesterCheckBox);
            LethalConfigManager.AddConfigItem(killableGhostGirlCheckBox);
            LethalConfigManager.AddConfigItem(killableGiantCheckBox);
            LethalConfigManager.AddConfigItem(killableCoilHeadCheckBox);
            LethalConfigManager.AddConfigItem(killableSporeLizardCheckBox);

            KillableEnemyManager.Instance.DoJesterKill = killableJesterEntry.Value;
            KillableEnemyManager.Instance.DoGhostGirlKill = killableGhostGirlEntry.Value;
            KillableEnemyManager.Instance.DoGiantKill = killableGiantEntry.Value;
            KillableEnemyManager.Instance.DoCoilHeadKill = killableCoilHeadEntry.Value;
            KillableEnemyManager.Instance.DoSporeLizardKill = killableSporeLizardEntry.Value;

            KillableEnemyManager.Instance.JesterHP = jesterHPEntry.Value;
            KillableEnemyManager.Instance.GhostGirlHP = ghostGirlHPEntry.Value;
            KillableEnemyManager.Instance.GiantHP = giantHPEntry.Value;
            KillableEnemyManager.Instance.CoilHeadHP = coilHeadHPEntry.Value;
            KillableEnemyManager.Instance.SporeLizardHP = sporeLizardHPEntry.Value;

            killableJesterEntry.SettingChanged += (sender, args) =>
            {
                if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
                {
                    KillableEnemyManager.Instance.DoJesterKill = killableJesterEntry.Value;
                }
            };

            killableGhostGirlEntry.SettingChanged += (sender, args) =>
            {
                if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
                {
                    KillableEnemyManager.Instance.DoGhostGirlKill = killableGhostGirlEntry.Value;
                }
            };

            killableGiantEntry.SettingChanged += (sender, args) =>
            {
                if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
                {
                    KillableEnemyManager.Instance.DoGiantKill = killableGiantEntry.Value;
                }
            };

            killableCoilHeadEntry.SettingChanged += (sender, args) =>
            {
                if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
                {
                    KillableEnemyManager.Instance.DoCoilHeadKill = killableCoilHeadEntry.Value;
                }
            };

            killableSporeLizardEntry.SettingChanged += (sender, args) =>
            {
                if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
                {
                    KillableEnemyManager.Instance.DoSporeLizardKill = killableSporeLizardEntry.Value;
                }
            };

            jesterHPEntry.SettingChanged += (sender, args) =>
            {
                if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
                {
                    KillableEnemyManager.Instance.JesterHP = jesterHPEntry.Value;
                }
            };

            ghostGirlHPEntry.SettingChanged += (sender, args) =>
            {
                if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
                {
                    KillableEnemyManager.Instance.GhostGirlHP = ghostGirlHPEntry.Value;
                }
            };

            giantHPEntry.SettingChanged += (sender, args) =>
            {
                if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
    {
                    KillableEnemyManager.Instance.GiantHP = giantHPEntry.Value;
                }
            };

            coilHeadHPEntry.SettingChanged += (sender, args) =>
            {
                if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
    {
                    KillableEnemyManager.Instance.CoilHeadHP = coilHeadHPEntry.Value;
                }
            };

            sporeLizardHPEntry.SettingChanged += (sender, args) =>
            {
                if (HUDManager.Instance.IsHost || HUDManager.Instance.IsServer)
    {
                    KillableEnemyManager.Instance.SporeLizardHP = sporeLizardHPEntry.Value;
                }
            };

            mls.LogInfo("Config finished parsing");
        }
    }
}