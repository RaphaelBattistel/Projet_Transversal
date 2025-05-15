using Firebase.Database;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private DatabaseReference dbReference;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
