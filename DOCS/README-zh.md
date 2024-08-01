# HFF Config Wizard 🥝

[[Document (English)](README.md) |
_当前页面：_ [使用文档（中文](README-zh.md)]
------------------------------------------------------------


Config Wizard 是一款基于 [BepInEx](https://github.com/BepInEx/BepInEx) 的 *人类：一败涂地* 插件。本插件为一些常用插件（主要是速通相关插件）提供了一个外部配置系统。

本插件旨在解决这些插件缺乏保存在磁盘上的配置系统的问题，如此一来玩家每次重启游戏时就不再需要手动调整设置，也不用为了玩不同的速通项目而重启游戏来启用禁用插件。

如果你有任何插件支持、新功能上的建议，或者是漏洞反馈，请 [创建一个新 Issue](https://github.com/Kirisoup/HFF-ConfigWizard/issues/new) 向我反馈。多语言支持正在计划中，敬请期待<3。你也可以通过 fork 本仓库并创建一个 PR 来帮助改进本插件 :3

## 使用方式：

在使用此插件前，首先需要将 BepInEx 安装到你的游戏目录（即游戏可执行文件所在的目录，对于人败来说就是 `human.exe` 所在的目录）。

> 前往 [此处](https://github.com/BepInEx/BepInEx/releases/latest) 下载 BepInEx 的最新发行版本。  
> 记得下载与你的游戏相匹配的版本（对于 Windows 端人类一败涂地，应下载 `win_x86` 版本）！

接下来，你需要将本插件安装至 BepInEx 的插件目录 `<游戏目录>/BepInEx/plugins`。

> 你可以在 [这里](https://github.com/Kirisoup/HFF-ConfigWizard/releases/latest) 安装最新版本的插件。

一切就绪后，当你启动游戏时，一个名为“ConfigWizard”的新文件夹将会在自动创建于 BepInEx 配置目录 `<game_root>/BepInEx/config`。

默认情况下，配置将在游戏启动时，和通过在游戏控制台中输入 `cwiz reload` 时加载。你可以通过修改 ConfigWizard 的主配置文件 `<game_root>/BepInEx/config/ConfigWizard/##Main.cfg` 来自定义此行为。你甚至可以将插件设置成在每次修改配置文件后自动重新加载！