using System;
using System.IO;
using System.Text;
using RedLoader;
using RedLoader.Utils;
using Sons.Wearable.Race;
using SonsSdk;
using SonsSdk.Attributes;
using TheForest.Utils;
using UnityEngine;

namespace CharacterSelect;

public class CharacterSelect : SonsMod
{
    private const int RaceCount = 8;

    private static int _index;
    private static bool _applied;
    private static string _configPath;

    public CharacterSelect()
    {
        OnUpdateCallback = OnUpdate;
    }

    protected override void OnSdkInitialized()
    {
        _configPath = Path.Combine(LoaderEnvironment.UserDataDirectory, "CharacterSelect.txt");
        Load();
        SdkEvents.OnAfterSpawn.Subscribe(OnSpawned);
        RLog.Msg($"CharacterSelect loaded. Saved character: {_index} ({(PlayerRace.Race)_index}). Press F9 or type: character");
    }

    private void OnSpawned()
    {
        _applied = false;
    }

    private void OnUpdate()
    {
        if (!LocalPlayer.RaceSystem)
            return;

        if (!_applied)
        {
            _applied = true;
            ApplyIndex(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            _index = (_index + 1) % RaceCount;
            ApplyIndex(true);
            Save();
        }
    }

    [DebugCommand("character")]
    private static void CharacterCommand(string args)
    {
        args = (args ?? string.Empty).Trim();

        if (args.Length == 0)
        {
            ShowList();
            return;
        }

        if (args.Equals("next", StringComparison.OrdinalIgnoreCase))
        {
            _index = (_index + 1) % RaceCount;
            ApplyIndex(true);
            Save();
            return;
        }

        if (int.TryParse(args, out var value) && value >= 0 && value < RaceCount)
        {
            _index = value;
            ApplyIndex(true);
            Save();
            return;
        }

        SonsTools.ShowMessage("Usage: character [0-7] or character next", 5f);
        RLog.Msg("Usage: character [0-7] or character next");
    }

    private static void ShowList()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Current: {_index} ({(PlayerRace.Race)_index})");
        for (int i = 0; i < RaceCount; i++)
            sb.AppendLine($"{i}: {(PlayerRace.Race)i}");

        var text = sb.ToString().TrimEnd();
        SonsTools.ShowMessage(text, 8f);
        RLog.Msg(text);
    }

    private static void ApplyIndex(bool announce)
    {
        var system = LocalPlayer.RaceSystem;
        if (!system)
            return;

        var race = (PlayerRace.Race)_index;
        system.ApplyRace(race);
        if (announce)
            SonsTools.ShowMessage($"{_index}: {race}");
        RLog.Msg($"Applied character {_index} ({race})");
    }

    private static void Load()
    {
        try
        {
            if (!File.Exists(_configPath))
                return;
            if (int.TryParse(File.ReadAllText(_configPath).Trim(), out var v) && v >= 0 && v < RaceCount)
                _index = v;
        }
        catch (Exception e)
        {
            RLog.Error($"CharacterSelect could not read config: {e.Message}");
        }
    }

    private static void Save()
    {
        try
        {
            File.WriteAllText(_configPath, _index.ToString());
        }
        catch (Exception e)
        {
            RLog.Error($"CharacterSelect could not write config: {e.Message}");
        }
    }
}
