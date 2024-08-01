using cwiz.ext;
using System;
using System.Collections.Generic;

public static class ShellRegist {

    private static readonly CommandRegistry _cmdreg =
        typeof(Shell).RefGetField<CommandRegistry>("commands");

    private static Lazy<Dictionary<string, Action>> Commands => new(() =>
        _cmdreg.RefGetField<Dictionary<string, Action>>("commands"));

    private static Lazy<Dictionary<string, Action<string>>> CommandsStr => new(() =>
        _cmdreg.RefGetField<Dictionary<string, Action<string>>>("commandsStr"));

    private static Lazy<Dictionary<string, string>> Description => new(() =>
        _cmdreg.RefGetField<Dictionary<string, string>>("description"));

    public static Lazy<string> HelpColor => new(() =>
        _cmdreg.RefGetField<string>("helpColor"));

    public class CommandBase(string key, string desc) {
        public string Key { get; } = key ?? throw new ArgumentException("command key can't be null");
        public string Desc { get; } = desc;
    }

    public class Command(
        string key,
        string desc,
        Action act
    ) : CommandBase(key, desc) {
        public Action Act { get; } = act;
    }

    public class SCommand(
        string key,
        string desc,
        Action<string> act
    ) : CommandBase(key, desc) {
        public Action<string> Act { get; } = act;
    }

    public static void Reg(this CommandBase cmd) {
        switch (cmd) {
            case Command c:
                Shell.RegisterCommand(c.Key, c.Act, c.Desc);
                break;
            case SCommand c:
                Shell.RegisterCommand(c.Key, c.Act, c.Desc);
                break;
        }
    }

    public static bool Destroy(this CommandBase cmd) {
        Description.Value.Remove(cmd.Key);
        return cmd switch {
            Command => Commands.Value.Remove(cmd.Key),
            SCommand => CommandsStr.Value.Remove(cmd.Key),
            _ => throw new NotImplementedException("Unimplemented Command type")
        };
    }

    public static void Help(this CommandBase cmd) =>
        _cmdreg.OnHelp(cmd.Key);

    public static string Help(string key, bool doformat = false) =>
        !Description.Value.TryGetValue(key, out var help) ? null :
            (doformat ?
                HelpColor.Value + help + "</color>" :
                help);
}