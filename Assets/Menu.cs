using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Transform centerPoint;
    public Image logo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(Screen.currentResolution);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition - centerPoint.position);
        //Debug.Log(new Vector2(Input.mousePosition.x - centerPoint.position.x, Input.mousePosition.y - centerPoint.position.y));
    }
}
