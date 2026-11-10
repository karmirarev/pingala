using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Pingala/Palette")]
public class Palette : ScriptableObject
{
    [Serializable]
    public class Mode
    {
        public Color paper;
        public Color ink;
        public Color faint;
        public Color grid;
        public Color line;
        public Color onInk;
    }

    [Serializable]
    public class Theme
    {
        public string name;
        public Color lightAccent;
        public Color lightMark;
        public Color darkAccent;
        public Color darkMark;
    }

    [SerializeField] private Mode day;
    [SerializeField] private Mode night;
    [field: SerializeField] public Theme[] themes { get; private set; }

    public Color GetAccent(int theme, bool dark)
    {
        return dark ? themes[theme].darkAccent : themes[theme].lightAccent;
    }

    public Color GetMark(int theme, bool dark)
    {
        return dark ? themes[theme].darkMark : themes[theme].lightMark;
    }

    public Color GetColor(ColorRole role, int theme, bool dark)
    {
        Mode mode = dark ? night : day;

        switch (role)
        {
            case ColorRole.Paper: return mode.paper;
            case ColorRole.Ink: return mode.ink;
            case ColorRole.Faint: return mode.faint;
            case ColorRole.Grid: return mode.grid;
            case ColorRole.Line: return mode.line;
            case ColorRole.OnInk: return mode.onInk;
            case ColorRole.Accent: return GetAccent(theme, dark);
            default: return GetMark(theme, dark);
        }
    }
}
