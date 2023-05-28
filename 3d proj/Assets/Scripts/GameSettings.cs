using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool _isMuted;
    private const string _settingsFileName = "/settings.txt";
    public static bool IsMuted
    {
        get => _isMuted;
        set { _isMuted = value; SaveSettings(); }
    }

    private static float _backgroundVolume;

    public static float BackgroundVolume
    {
        get => _backgroundVolume;
        set { _backgroundVolume = value; SaveSettings(); }
    }

    public static void SaveSettings()
    {
        string path = Application.persistentDataPath + '/' + _settingsFileName;
        string data = $"{_isMuted}\n{_backgroundVolume}";
        File.WriteAllText(path, data);
    }

    public static void LoadSettings()
    {
        string path = Application.persistentDataPath + "/" + _settingsFileName;
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            _isMuted = (lines.Length > 0) ? Convert.ToBoolean(lines[0]) : false;
            _backgroundVolume = (lines.Length > 1) ? Convert.ToSingle(lines[1]) : 0;
        }
    }
}
