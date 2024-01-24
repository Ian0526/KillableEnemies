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

## Listening to Enemy Events

The `KillableEnemy` class emits events that your mod can listen to. These events are `DeathEvent` and `HitEvent`.

### Subscribing to DeathEvent

To react when an enemy dies, subscribe to the `DeathEvent`. This event is triggered when an enemy's health reaches zero.

Example:
```csharp
KillableEnemy enemy = // get your instance of KillableEnemy
enemy.DeathEvent += OnEnemyDeath;

private void OnEnemyDeath(KillableEnemy sender, DeathEventArgs e)
{
    // Your code here. Example:
    Console.WriteLine($"{sender.enemyType} has died.");
}
```

### Subscribing to HitEvent

To perform actions when an enemy is hit, subscribe to the `HitEvent`.

Example:
```csharp
enemy.HitEvent += OnEnemyHit;

private void OnEnemyHit(KillableEnemy sender, HitEventArgs e)
{
    // Your code here. Example:
    Console.WriteLine($"{sender.enemyType} has been hit.");
}
```

### Subscribing to DeathEvent

To perform actions when an enemy is killed, subscribe to the 'DeathEvent'.

Example:
```csharp
enemy.HitEvent += OnEnemyDeath;

private void OnEnemyDeath(KillableEnemy sender, HitEventArgs e)
{
    // Your code here. Example:
    Console.WriteLine($"{sender.enemyType} has been kill.");
}
```


### Cancelling Events
```csharp
enemy.HitEvent += OnEnemyDeath;

private void OnEnemyDeath(KillableEnemy sender, HitEventArgs e)
{
    if (someConditionThatCancels)
	e.Cancel = true;
}
```
