# HFF Config Wizard 🥝

[[Document (English) *\<current page>*](..\DOCS\README.en.md) |
[使用文档（中文）](..\DOCS\README.zh.md)]
------------------------------------------------------------


Config Wizard is a plugin for *Human: Fall Flat* based on
[BepInEx](https://github.com/BepInEx/BepInEx).
It provides an external config system for some commonly used plugins, mainly the
ones for HFF speedrunning.

It is developped in an attempt to fix their lack of a saved-on-disk config system,
so that the players don't need to manually tweak the settings every time they
restart the game, nor do they need to restart their game whenever they need to
turn on/off some plugins to play different categories.

Please 
[create a new issue](https://github.com/Kirisoup/HFF-ConfigWizard/issues/new)
if you want to seggest plugins to support, new features, report a bug. 
Multi-language support has being planned. You can help the plugin by forking it
and create a pull request :3 

## Usage:

To use the plugin, you would fist have to install BepInEx to your game directory 
(i.e. where the game executable file is located, which for HFF would the 
directory where `human.exe` is located).

> Download the latest BepInEx release
> [here](https://github.com/BepInEx/BepInEx/releases/latest).  
> Remember to download the correct version for your game (if you are playing HFF
> on Windows, you should download the `win_x86` version)!

Next, you need to install this plugin to BepInEx's Plugin directory 
`<game_root>/BepInEx/plugins`. 

> You can install the latest release of the plugin
[here](https://github.com/Kirisoup/HFF-ConfigWizard/releases/latest)

After that, a new folder named "ConfigWizard" will
be created at the config directory `<game_root>/BepInEx/config` once you launch 
the game.

By default, the configs will be applied when the game launches, and by typing 
`cwiz reload` in the game console. You can customize this behavior by modifying
ConfigWizard's master config at 
`<game_root>/BepInEx/config/ConfigWizard/##Main.cfg`. You can even make the configs
auto reload whenever you modify the config files!

