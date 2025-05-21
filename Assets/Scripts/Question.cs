using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Question : MonoBehaviour
{
    public Data data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(data.idQuestion);
        Debug.Log(data.idReponse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
