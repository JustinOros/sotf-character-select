using System;
using System.IO;
using RedLoader;
using RedLoader.Utils;
using Sons.Wearable.Race;
using SonsSdk;
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
        RLog.Msg($"CharacterSelect loaded. Saved race: {_index} ({(PlayerRace.Race)_index}). F9 cycles.");
    }

    private void OnSpawned()
    {
        _applied = false;
    }

    private void OnUpdate()
    {
        var system = LocalPlayer.RaceSystem;
        if (!system)
            return;

        if (!_applied)
        {
            _applied = true;
            Apply(system, false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            _index = (_index + 1) % RaceCount;
            Apply(system, true);
            Save();
        }
    }

    private void Apply(PlayerRaceSystem system, bool announce)
    {
        var race = (PlayerRace.Race)_index;
        system.ApplyRace(race);
        if (announce)
            SonsTools.ShowMessage($"{_index}: {race}");
        RLog.Msg($"Applied race {_index} ({race})");
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
