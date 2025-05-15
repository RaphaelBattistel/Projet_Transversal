using System;
using UnityEditor.Overlays;
using UnityEngine;

[Serializable]
public class Data
{
    public Sprite sprite;
    public string name;
    public enum STYLE
    {
        Fusion,
        Cafe,
        Ballad,
        Waltz
    }
    public STYLE style;
    public string solutionImg;
    public int value;
}
