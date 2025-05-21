using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public InputField Name;
    public InputField SessionID;

    public Text joinText;
    public Text statusText;
    public Text hostText;
    public Text sessionText;

    private string userID;
    private DatabaseReference dbReference;
    private string sessionID;

    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        gameManager = GetComponent<GameManager>();
    }

    public void CreateUser()
    {
        dbReference.Child("hosts").Child(sessionID).GetValueAsync().ContinueWithOnMainThread(saveTask =>
        {
            if (saveTask.IsFaulted)
            {
                Debug.Log("Erreur, pas la bonne formulation");
            }

            else if (saveTask.IsCompleted)
            {
                DataSnapshot snapshot = saveTask.Result;

                if(snapshot.Value == null)
                {
                    Debug.Log("Error, no value found");
                }

                
            }
        });



        CheckIfSessionIdExistsInHosts(
        SessionID.text, 

        (isFound) => 
        {
            Debug.Log($"CheckIfSessionIdExistsInHosts reported result: {isFound}");
            if (isFound)
            {
                // --- Session ID is VALID! Proceed with saving the user ---
                Debug.Log($"Entered Session ID '{SessionID.text}' is valid. Proceeding to save user data.");

                // Create the User object
                User newUser = new User(Name.text, SessionID.text); // Make sure your User class has sessionId
                string json = JsonUtility.ToJson(newUser);

                // Start the SAVE operation, with its own continuation
                dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json) // Corrected to SetRawJsonValueAsync based on your previous code
                    .ContinueWithOnMainThread(saveTask =>
                    {
                        if (saveTask.IsFaulted)
                        {
                            Debug.LogError("Failed to save user data: " + saveTask.Exception);
                            joinText.text = "Save Failed";
                            statusText.text = "Failed to save user data.";
                        }
                        else if (saveTask.IsCompleted)
                        {
                            Debug.Log("User data saved successfully!");
                            joinText.text = "Joined";
                            statusText.text = $"Joined session {SessionID.text}";
                            // User is now joined! Proceed with game logic.
                        }
                    });
            }
            else
            {
                // --- Session ID is INVALID! ---
                Debug.LogWarning($"Entered Session ID '{SessionID.text}' not found.");
                joinText.text = "Join Failed";
                statusText.text = $"Session ID '{SessionID.text}' not found.";
                // Do NOT proceed with saving the user data
            }
        },

        
        (errorMessage) => 
        {
            // Handle the error that occurred while trying to *fetch* the host data
            Debug.LogError("Error during session ID check: " + errorMessage);
            joinText.text = "Error";
            statusText.text = "Error checking session ID.";
            // Do NOT proceed with saving the user data
        }
    );
    }

    public void CreateHost()
    {
        sessionID = GenerateSessionID(8);

        Host newHost = new Host(Name.text, sessionID);
        string json = JsonUtility.ToJson(newHost);

        statusText.text = "Creating a Session";



        dbReference.Child("hosts").SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
            }
            else if (task.IsCompleted)
            {
                hostText.text = "Creating";
                sessionText.text = "Session ID : " + sessionID;
            }
        });

        
    }

    public void CheckIfSessionIdExistsInHosts(string sessionIdToCheck, Action<bool> onCheckComplete, Action<string> onError)
    {
        // Basic input validation
        if (string.IsNullOrEmpty(sessionIdToCheck))
        {
            onError?.Invoke("Session ID to check cannot be empty.");
            Debug.LogWarning("CheckIfSessionIdExistsInHosts: session ID is empty.");
            return;
        }

        Debug.Log($"Checking if session ID '{sessionIdToCheck}' exists as a KEY directly under /hosts...");

        // --- This is the key step for your structure! ---
        // We get a reference DIRECTLY to the potential path: /hosts/{sessionIdToCheck}
        // By creating this reference, we are saying "I want to look at the node
        // that would have the key 'sessionIdToCheck' directly under 'hosts'".
        DatabaseReference potentialSessionRef = dbReference.Child("hosts");

        // --- Then, we simply try to read from that specific location. ---
        // GetValueAsync() will attempt to fetch the data at the path potentialSessionRef points to.
        potentialSessionRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            // This code runs AFTER the GetValueAsync task is completed (success or failure)

            if (task.IsFaulted)
            {
                // Handle any network or permission errors during the read
                Debug.LogError("CheckIfSessionIdExistsInHosts: Failed to read potential host data: " + task.Exception);
                // Call the error callback with the exception message
                onError?.Invoke("Failed to read host data: " + task.Exception.ToString());
                return; // Stop execution here
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                // --- This is the check! ---
                // The DataSnapshot object has an 'Exists' property.
                // If snapshot.Exists is TRUE, it means a node *was* found
                // at the exact path we referenced (/hosts/{sessionIdToCheck}).
                // This means a child with the key 'sessionIdToCheck' exists directly under /hosts.
                bool exists = snapshot.Exists;

                Debug.Log($"Check complete for session ID '{sessionIdToCheck}'. Found: {exists}");

                // Call the success callback with the result (true or false)
                onCheckComplete?.Invoke(exists);
            }
        });

        // Code here executes immediately after starting the async operation,
        // but BEFORE the result is ready.
        Debug.Log("CheckIfSessionIdExistsInHosts operation started...");
    }

    string GenerateSessionID(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] stringChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[UnityEngine.Random.Range(0, chars.Length)];
        }

        return new string(stringChars);
    }



}
