using Firebase.Database;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerManager : MonoBehaviour
{
    private string userID;
    private DatabaseReference dbReference;
    private DatabaseReference hostsRef;

    public bool validSession;
    public int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        validSession = false;

        hostsRef = dbReference.Child("hosts");
        hostsRef.ValueChanged += OnHostsDataChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValidatingSession()
    {
        validSession = true;
    }

    private void OnHostsDataChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError("Firebase Database Error: " + args.DatabaseError.Message);
            // Handle the error (e.g., inform the user, retry)
            return;
        }

        // 'args.Snapshot' contains the data at the "hosts" location
        DataSnapshot snapshot = args.Snapshot;

        // Check if the "hosts" node exists and has data
        if (snapshot.Exists)
        {
            Debug.Log("Data at 'hosts' updated:");

            // 4. Extract the specific values you want from the snapshot

            // Get the 'currentQuestion' value
            // We use snapshot.Child("childName").GetValue(false) to get the raw value
            // Then we can convert it to the desired type (int)
            if (snapshot.Child("currentQuestion").Exists)
            {
                object currentQuestionObject = snapshot.Child("currentQuestion").Value;
                if (currentQuestionObject is long longValue) // Realtime Database typically returns numbers as long
                {
                    int currentQuestion = (int)longValue; // Cast to int if within range
                    Debug.Log("  currentQuestion: " + currentQuestion);
                    // TODO: Your game logic or UI update based on currentQuestion
                }
                else if (currentQuestionObject is int intValue)
                {
                    int currentQuestion = intValue;
                    Debug.Log("  currentQuestion: " + currentQuestion);
                    // TODO: Your game logic or UI update based on currentQuestion
                }
                else
                {
                    Debug.LogWarning("'currentQuestion' exists but is not a number.");
                }
            }
            else
            {
                Debug.Log("  currentQuestion not found or is null");
                // TODO: Handle missing currentQuestion
            }


            // Get the 'startTimer' value
            if (snapshot.Child("startTimer").Exists)
            {
                object startTimerObject = snapshot.Child("startTimer").Value;
                if (startTimerObject is bool startTimer)
                {
                    Debug.Log("  startTimer: " + startTimer);
                    // TODO: Your game logic or UI update based on startTimer
                }
                else
                {
                    Debug.LogWarning("'startTimer' exists but is not a boolean.");
                }
            }
            else
            {
                Debug.Log("  startTimer not found or is null");
                // TODO: Handle missing startTimer
            }

            if (!snapshot.Child("currentQuestion").Exists && !snapshot.Child("startTimer").Exists && snapshot.HasChildren)
            {
                Debug.Log("Neither 'currentQuestion' nor 'startTimer' exist, but other data might be present.");
            }
            else if (!snapshot.Exists)
            {
                Debug.Log("'hosts' node does not exist"); // This case is unlikely if snapshot.Exists is true, but good for completeness
            }


        }
        else
        {
            // Handle the case where the 'hosts' node itself is deleted or doesn't exist yet
            Debug.Log("'hosts' node does not exist");
            // TODO: Potentially reset UI or state if 'hosts' node disappears
        }
    }
}
