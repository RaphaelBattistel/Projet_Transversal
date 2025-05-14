using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Database", menuName = "Database", order = 1)]

public class Database : ScriptableObject
{
    public enum CATEGORY
    {
        NONE,
        MUSIC,
        PHOTO,
        AREA
    }
    public CATEGORY category;

    [SerializeField] private List<Data> datas = new();
    public Data GetData(int id, bool random = false)
    {
        if (random && (id < 0 || id >= datas.Count))
            id = Random.Range(0, datas.Count);
        else
            id = Mathf.Clamp(id, 0, datas.Count - 1);

        return datas[id];
    }
}