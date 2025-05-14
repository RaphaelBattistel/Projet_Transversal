using UnityEngine;

public class User
{
    public string username;
    public int score;
    public bool hasAnswered;

    public User() {
        }

    public User(string username, int score, bool hasAnswered)
    {
        this.username = username;
        this.score = score;
        this.hasAnswered = hasAnswered;
    }
}
