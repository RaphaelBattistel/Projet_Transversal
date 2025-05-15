using Firebase.Database;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private DatabaseReference dbReference;

    private void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void StartTimer()
    {

    }

    public void Pause()
    {

    }

    public void Skip()
    {

    }
}
