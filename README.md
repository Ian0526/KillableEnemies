# KillableEnemies

KillableEnemies is an API and Mod allowing the deaths of invulnerable enemies.

## Notes
I haven't made a single animation, the enemies will just despawn but there are events you can hook into yourself to add in sounds and whatnot. At the very least, I will have the enemies ragdoll eventually.

## Changelog

### 0.1.1
- [Bug Fix] Don't register killable data if not defined in config (you could kill disabled enemies)

### 0.1.1
- [QOL] Add reference to KillableEnemy object in events.

### 0.1.0
- [Feature] Config options to modify damage and invulnerability of all invulnerable enemies.
- [Feature] Easy to hook into API, cancelable damage and kill events

## Dependencies
- LethalConfig

## Usage
Open the settings menu with ESC and edit it through the Mod Config button.
For API usage, visit the Git link.

## Contributing
Feel free to contribute. If it improves the system, I'm more than happy to implement your changes.