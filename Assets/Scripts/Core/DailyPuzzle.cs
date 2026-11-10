using System;
using System.Collections.Generic;
using UnityEngine;

public static class DailyPuzzle
{
    private const string DoneKey = "dailyDone";
    private static readonly DateTime FirstDay = new DateTime(2026, 10, 7);

    public static bool IsDoneToday()
    {
        return PlayerPrefs.GetString(DoneKey) == Today();
    }

    public static void MarkDoneToday()
    {
        PlayerPrefs.SetString(DoneKey, Today());
        PlayerPrefs.Save();
    }

    public static bool TryGetToday(out int[] digits, out string fact)
    {
        digits = null;
        fact = null;

        TextAsset file = Resources.Load<TextAsset>("DailyPuzzles");
        if (file == null) return false;

        List<string> lines = new List<string>();
        foreach (string line in file.text.Split('\n'))
        {
            if (IsValid(line.Trim())) lines.Add(line.Trim());
        }

        if (lines.Count == 0) return false;

        int day = (int)(DateTime.Today - FirstDay).TotalDays;
        int index = ((day % lines.Count) + lines.Count) % lines.Count;
        string[] parts = lines[index].Split('|');

        digits = new int[4];
        for (int i = 0; i < 4; i++)
        {
            digits[i] = parts[0][i] - '0';
        }

        fact = parts[1].Trim();
        return true;
    }

    private static bool IsValid(string line)
    {
        string[] parts = line.Split('|');
        if (parts.Length != 2 || parts[0].Length != 4) return false;

        for (int i = 0; i < 4; i++)
        {
            if (!char.IsDigit(parts[0][i])) return false;
            if (parts[0].IndexOf(parts[0][i]) != i) return false;
        }

        return true;
    }

    private static string Today()
    {
        return DateTime.Today.ToString("yyyy-MM-dd");
    }
}
