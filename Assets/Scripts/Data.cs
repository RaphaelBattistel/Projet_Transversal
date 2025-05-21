using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "Data", order = 1)]
public class Data : ScriptableObject
{
    public Sprite sprite;
    public int idQuestion;
    public int idReponse;
    public int idBonneRep;

    public enum QA
    {
        Question,
        Reponse
    }
    
}
