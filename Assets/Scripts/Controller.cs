using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Controller : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;


    private void Awake()
    {
        TryGetComponent(out _spriteRenderer);
        TryGetComponent(out _collider);

    }

    private void Start()
    {
        enabled = false;
        Debug.Log("lol");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DropArea")
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaa");
    }



    //    Init();
    //}

    //private void Init()
    //{

    //    _spriteRenderer.sprite = _data.sprite;
    //    _spriteRenderer.color = _data.color;
    //}


}
