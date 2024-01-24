# Changelog

## 0.1.1
- [Feature] Reference to KillableEnemy in events.

## 0.1.0
- [Feature] Config options to modify damage and invulnerability of all invulnerable enemies.
- [Feature] Easy to hook into API, cancelable damage and kill events

# KillableEnemies API Usage

The `KillableEnemies` mod provides an API that allows other mods to interact with and extend the functionality of various enemy types in the game. Below are the instructions on how to use this API in your mod.

## Getting Started

First, ensure that your project references the `KillableEnemies` mod. You will need to have the mod's DLL file available in your development environment.

## Adding as a Dependency

Add this above your Main class.

```csharp
[BepInDependency("Ovchinikov.KillableEnemies.Main", BepInDependency.DependencyFlags.HardDependency)]
```

## Listening to Enemy Events

The `KillableEnemy` class emits events that your mod can listen to. These events are `DeathEvent` and `HitEvent`.

### Subscribing to DeathEvent

To react when an enemy dies, subscribe to the `DeathEvent`. This event is triggered when an enemy's health reaches zero.

Example:
```csharp
[HarmonyPostfix]
[HarmonyPatch(typeof(EnemyAI), "Start")]
static void PostfixStartEnemy(EnemyAI __instance)
{
    KillableEnemy enemy = __instance.GetComponent<KillableEnemy>();
    // Since some don't have this component
    if (enemy != null)
    {
        enemy.DeathEvent += DoSomething;
    }
}

private static void DoSomething(KillableEnemy sender, DeathEventArgs e)
{
    // Your code here. Example:
    Console.WriteLine($"{e.Enemy.EnemyType} has died.");
}
```

### Subscribing to HitEvent

To perform actions when an enemy is hit, subscribe to the `HitEvent`.

Example:
```csharp
[HarmonyPostfix]
[HarmonyPatch(typeof(EnemyAI), "Start")]
static void PostfixStartEnemy(EnemyAI __instance)
{
    KillableEnemy enemy = __instance.GetComponent<KillableEnemy>();
    // Since some don't have this component
    if (enemy != null)
    {
        enemy.HitEvent += DoSomething;
    }
}

private static void DoSomething(KillableEnemy sender, HitEventArgs e)
{
    // Your code here. Example:
    Console.WriteLine($"{e.Enemy.EnemyType} has been hit.");
}
```

### Cancelling Events
```csharp
[HarmonyPostfix]
[HarmonyPatch(typeof(EnemyAI), "Start")]
static void PostfixStartEnemy(EnemyAI __instance)
{
    KillableEnemy enemy = __instance.GetComponent<KillableEnemy>();
    // Since some don't have this component
    if (enemy != null)
    {
        enemy.HitEvent += Cancel;
    }
}

private static void Cancel(KillableEnemy sender, HitEventArgs e)
{
    e.Cancel = true;
}
```
