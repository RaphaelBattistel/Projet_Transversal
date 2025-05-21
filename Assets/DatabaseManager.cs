using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
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
    private PlayerManager playerManager;

    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        gameManager = GetComponent<GameManager>();
        playerManager = GetComponent<PlayerManager>();
    }
   
    //Rejoindre une session
    public void CreateUser()
    {
        //Cherche les Données stockées sous 'hosts'
        dbReference.Child("hosts").GetValueAsync().ContinueWithOnMainThread(saveTask =>
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

                else
                {
                    // Triage des données selon leur types
                    if (snapshot.Value is Dictionary<string, object> hostsData)
                    {
                        Debug.Log("Successfully read 'hosts' data as a dictionary!");

                        // Si on trouve une variable sessionID
                        if (hostsData.TryGetValue("sessionID", out object sessionIDObject))
                        {
                            // Enregistrement de la valeur
                            string sessionID = sessionIDObject as string;
                            if (sessionID != null)
                            {
                                Debug.Log($"Session ID: {sessionID}");

                                if(SessionID.text == sessionID)
                                {
                                    statusText.text = "Session Found, joining...";
                                    User newUser = new User(Name.text, sessionID);

                                    string json = JsonUtility.ToJson(newUser);
                                    dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json); 
                                    playerManager.ValidatingSession();
                                }

                                else
                                {
                                    Debug.Log($"Session ID is Invalid, your written id is {SessionID.text}, it should be {sessionID}");
                                }

                            }
                            else
                            {
                                Debug.Log("'sessionID' value is not a string.");
                            }
                        }
                        else
                        {
                            Debug.Log("'sessionID' key not found in hosts data.");
                        }
                    }
                }
            }
        });
    }

    // Création de session 
    public void CreateHost()
    {
        sessionID = GenerateSessionID(8);

        Host newHost = new Host(Name.text, sessionID, false, 0);
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

    // Génération d'ID de sessions
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
