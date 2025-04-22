using cwiz.ext;
using System;
using System.Linq;
using System.Collections.Generic;
using BepInEx.Configuration;

namespace cwiz {
    public static class CwizEntries {

        public static readonly Dictionary<string, CwizEntry.BaseGroup> DefaultConfigs =
            // this shit is huge
            // TODO (consider): globalization™
            new CwizEntry.PluginGroup[] {
                new("com.plcc.hff.timer",
                    new CwizEntry.Group("1_Timer",
                        new CwizEntry.Option("TimerOpened", false,
                            "Whether should the timer be displayed on gui",
                            v => Timer.Timer.timerOpened = (bool)v
                        ),
                        new CwizEntry.Option("AutoReset", false,
                            "Whether should the timer auto-reset when you leave the level",
                            v => Timer.Main.timer.autoReset = (bool)v
                        ),
                        new CwizEntry.Option("TimeWhenPaused", false,
                            "Keep timing even when the game is paused",
                            v => Timer.Timer.timeWhenPaused = (bool)v
                        ),
                        new CwizEntry.Option("TimeInMenu", false,
                            "Keep timing even when you are back in the main menu (Keep on in Achievement%)",
                            v => Timer.Main.timer.timeInMenu = (bool)v
                        ),
                        new CwizEntry.Group("Style",
                            new CwizEntry.Option("TimerStyle", Timer.TimerStyle.Normal,
                                "The style in which the timer is shown on gui",
                                v => Timer.Main.timer.timerStyle = (Timer.TimerStyle)v
                            ),
                            new CwizEntry.Option("LiveSplitMode", "multilevel",
                                new ConfigDescription("If timer style is set to Livesplit, whether should the timer show in multiple levels or single level",
                                    new AcceptableValueList<string>("multilevel", "singlelevel")),
                                v => Timer.Main.timer.multilevel = ((string)v).ToLetters().ToLower() switch {
                                    "multilevel" => true,
                                    "singlelevel" => false,
                                    _ => throw new ArgumentException("unexpected value for LiveSplitMode option"),
                                }
                            )
                        )
                    ),
                    new CwizEntry.Group("2_Speedrun",
                        new CwizEntry.Group("Category",
                            new CwizEntry.Option("Mode", Timer.CheckpointMode.Any,
                                "Set the speedrun category to enable builtin support for some categories (mainly checkpoint tracking)",
                                v => Timer.Speedrun.checkpoint = (Timer.CheckpointMode)v
                            ),
                            new CwizEntry.Option("Jumpless", false,
                                "Introduce jumpless as an requirement fot the category",
                                v => Timer.Speedrun.jumpless = (bool)v
                            )
                        ),
                        new CwizEntry.Group("Retry",
                            new CwizEntry.Option("RetryLevel", -1,
                                new[] {
                                    "Specify the id of the level to retry",
                                    " (while playing workshop levels, it will always retry the current level)"
                                }.ToLines(),
                                v => Timer.Speedrun.curLevel = (int)v < 0 ? "" : ((int)v).ToString()
                            ),
                            new CwizEntry.Option("Type", WorkshopItemSource.BuiltIn,
                                "The type of the level to retry",
                                v => Timer.Speedrun.levelType = (WorkshopItemSource)v
                            ),
                            new CwizEntry.Option("RetryKey", UnityEngine.KeyCode.R,
                                new ConfigDescription("The hotkey to trigger retry (currently only letters (a-z) are supported)",
                                    new AcceptableValueRange<UnityEngine.KeyCode>(UnityEngine.KeyCode.A, UnityEngine.KeyCode.Z)),
                                v => {
                                    UnityEngine.KeyCode key = (UnityEngine.KeyCode)v;
                                    Timer.Main.speedrun.curRetryKey = (key > UnityEngine.KeyCode.A && key <= UnityEngine.KeyCode.Z) ? key.ToString().ToLower() : "";
                                    // implement extended hotkey support
                                }
                            )
                        ),
                        new CwizEntry.Option("ShowKeys", false,
                            "Whether should the timer display keystrokes",
                            v => Timer.Main.speedrun.showKeys = (bool)v
                        ),
                        new CwizEntry.Option("ShowAttempts", false,
                            "Whether should the timer display your number of attempts",
                            v => Timer.Speedrun.showAttempts = (bool)v
                        ),
                        new CwizEntry.Option("AutoRetry", false,
                            "Whether should the timer auto retry when you die",
                            v => Timer.Speedrun.autoRetry = (bool)v
                        ),
                        new CwizEntry.Option("FixCheckpoint", true,
                            new[] {
                                "Fix multiplayer client-side checkpoint recieve bug",
                                "This should always be on if there's no specific reason for it to be off, especiallly while playing CP% coop"
                            }.ToLines(),
                            v => Timer.Speedrun.fixCheckpoint = (bool)v
                        )
                    )
                ),
                new("com.plcc.hff.humanmod", // TODO: description
                    new CwizEntry.Group("1_General",
                        new CwizEntry.Option("ClimbCheat", false,
                            "",
                            v => CheatCodes.climbCheat = (bool)v
                        ),
                        new CwizEntry.Option("ThrowCheat", false,
                            "",
                            v => CheatCodes.throwCheat = (bool)v
                        ),
                        new CwizEntry.Option("FlyCheat", false,
                            "",
                            v => Human_Mod.Main.flyCheat = (bool)v
                        ),
                        new CwizEntry.Option("PointOfRecovery", false,
                            "",
                            v => Human_Mod.Main.pointCheat = (bool)v
                        ),
                        new CwizEntry.Option("PointOfRecoveryMode", Human_Mod.PointMode.LOW,
                            "",
                            v => Human_Mod.Main.pointMode = (Human_Mod.PointMode)v
                        ),
                        new CwizEntry.Option("DebugInfo", false,
                            "",
                            v => Human_Mod.Main.debugCheat = (bool)v
                        ),
                        new CwizEntry.Option("EnableHotkeys", false,
                            "",
                            v => Human_Mod.Main.enableHotkeys = (bool)v
                        ),
                        new CwizEntry.Option("PlayerXray", false,
                            "",
                            v => Human_Mod.Main.xrayPlayers = (bool)v
                        ),
                        new CwizEntry.Option("AchievementStats", false,
                            "",
                            v => Human_Mod.Main.achievementCheat = (bool)v
                        ),
                        new CwizEntry.Option("UnconsciousCheat", false,
                            "",
                            v => Human_Mod.Main.unconsciousCheat = (bool)v
                        ),
                        new CwizEntry.Option("ControlObjects", false,
                            "",
                            v => Human_Mod.Main.controlCheat = (bool)v
                        ),
                        new CwizEntry.Option("Minimap", false,
                            "",
                            v => Human_Mod.Main.minimap = (bool)v
                        ),
                        new CwizEntry.Option("PlayerList", false,
                            "",
                            v => Human_Mod.Main.playersCheat = (bool)v
                        )
                    ),
                    new CwizEntry.Group("2_Extra",
                        new CwizEntry.Option("AccelerationCheat", false,
                            "",
                            v => Human_Mod.Main.liuhai = (bool)v
                        ),
                        new CwizEntry.Group("1_RendererUtils",
                            new CwizEntry.Option("HideFakeObjects", false,
                                "",
                                v => Human_Mod.Main.deleteFakeObjects = (bool)v
                            ),
                            new CwizEntry.Option("ShowCheckpoints", false,
                                "",
                                v => Human_Mod.Main.showCheckpoint = (bool)v
                            ),
                            new CwizEntry.Option("ShowPassTriggers", false,
                                "",
                                v => Human_Mod.Main.showLoadingZone = (bool)v
                            ),
                            new CwizEntry.Option("ShowFallTriggers", false,
                                "",
                                v => Human_Mod.Main.showDeathZone = (bool)v
                            ),
                            new CwizEntry.Option("ShowTriggersAndInvisWalls", false, // TODO: Seperate
                                "",
                                v => Human_Mod.Main.showAirWall = (bool)v
                            )
                        ),
                        new CwizEntry.Option("NoPlayerCollision", false,
                            "",
                            v => Human_Mod.Main.ignoreCollision = (bool)v
                        ),
                        new CwizEntry.Option("AutoReach", false,
                            "",
                            v => Human_Mod.Main.autoReach = (bool)v
                        ),
                        new CwizEntry.Option("AutoBhop", false,
                            "",
                            v => Human_Mod.Main.autoBhop = (bool)v
                        ),
                        new CwizEntry.Group("2_ModifyAttributes",
                            new CwizEntry.Group("ArmLength",
                                new CwizEntry.Option("ModifyArmLength", false,
                                    "",
                                    v => Human_Mod.Main.modifyHand = (bool)v
                                ),
                                new CwizEntry.Option("ArmLengthRelaxed", -1,
                                    "",
                                    v => Human_Mod.Main.curHand = ((int)v) >= 0 ? ((int)v).ToString() : ""
                                ),
                                new CwizEntry.Option("ArmLengthReaching", -1,
                                    "",
                                    v => Human_Mod.Main.curExtendedHand = ((int)v) >= 0 ? ((int)v).ToString() : ""
                                )
                            ),
                            new CwizEntry.Group("MovementSpeed",
                                new CwizEntry.Option("ModifySpeed", false,
                                    "",
                                    v => Human_Mod.Main.modifySpeed = (bool)v
                                ),
                                new CwizEntry.Option("Speed", 1,
                                    "",
                                    v => Human_Mod.Main.curSpeed = ((int)v).ToString()
                                )
                            ),
                            new CwizEntry.Group("PlayerScale",
                                new CwizEntry.Option("ModifyScale", false,
                                    "",
                                    v => Human_Mod.Main.modifyScale = (bool)v
                                ),
                                new CwizEntry.Option("Scale", 1,
                                    "",
                                    v => Human_Mod.Main.curScale = ((int)v).ToString()
                                )
                            ),
                            new CwizEntry.Group("JumpForce",
                                new CwizEntry.Option("JumpForceModifier", "",
                                    "",
                                    v => Human_Mod.Main.curSuperJump = (string)v
                                )
                            ),
                            new CwizEntry.Group("Gravity",
                                new CwizEntry.Option("GravityModifier", "",
                                    "",
                                    v => Human_Mod.Main.curGravity = (string)v
                                )
                            )
                        ),
                        new CwizEntry.Option("HookMode", false,
                            "",
                            v => Human_Mod.Main.hookMode = (bool)v
                        ),
                        new CwizEntry.Group("3_MenuFixes",
                            new CwizEntry.Option("PreviewOnly", false,
                                "",
                                v => Human_Mod.Main.previewOnly = (bool)v
                            ),
                            new CwizEntry.Option("NoWorkshopReload", false,
                                "",
                                v => Human_Mod.Main.noWorkshopReload = (bool)v
                            ),
                            new CwizEntry.Option("NoUIDelay", false,
                                "",
                                v => Human_Mod.Main.noDelay = (bool)v
                            )
                        ),
                        new CwizEntry.Option("LoadingPreview", false,
                            "",
                            v => Human_Mod.Main.loadingPreview = (bool)v
                        ),
                        new CwizEntry.Option("FixSkin", false,
                            "",
                            v => Human_Mod.Main.fixSkin = (bool)v
                        ),
                        new CwizEntry.Option("NewbieLevelMode", false,
                            "",
                            v => Human_Mod.Main.newbieMode = (bool)v
                        ),
                        new CwizEntry.Option("FollowHost", false,
                            "",
                            v => Human_Mod.Main.followServer = (bool)v
                        )
                    )
                ),
                new("org.bepinex.plugins.humanfallflat.achievements",
                    new CwizEntry.Option("enabled", false,
                        "Whether is AchievementTracker (for achievement% category) enabled",
                        v => UnityEngine.Object
                            .FindObjectsOfType<HFF_AchievementTracker.Tracker>()
                            .ForEach(Tracker =>
                                ((Dictionary<string, Action<string>>)
                                    HarmonyLib.AccessTools.Field(typeof(HFF_AchievementTracker.Tracker), "commands")
                                    .GetValue(Tracker))
                                [(bool)v ? "enable" : "disable"](null)
                            )
                    )
                ),
                new("top.zman350x.hff.objectgrabber",
                    new CwizEntry.Option("enabled", false,
                        "Whether is ObjectGrabber (for minimum grab category) enabled",
                        v => {
                            ObjectGrabber.GrabberTracker.instance.isEnabled = (bool)v;
                            ((UnityEngine.GameObject)HarmonyLib.AccessTools.Field(typeof(ObjectGrabber.GrabberTracker), "textObj")
                                .GetValue(ObjectGrabber.GrabberTracker.instance))
                                .SetActive((bool)v);
                            HarmonyLib.AccessTools.Field(typeof(ObjectGrabber.GrabberTracker), "grabs")
                            .SetValue(ObjectGrabber.GrabberTracker.instance, 0U);
                            ((TMPro.TextMeshProUGUI)HarmonyLib.AccessTools.Field(typeof(ObjectGrabber.GrabberTracker), "textVisuals")
                                .GetValue(ObjectGrabber.GrabberTracker.instance)).text = "Grabs: 0";
                        }
                    )
                )
            }.ToDictionary(p => p.Key, p => p.Entries);
    }
}