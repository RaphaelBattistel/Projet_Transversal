using UnityEngine;

public class HostCommands : MonoBehaviour
{

    public bool startTimer;
    public int currentQuestion;

    public HostCommands(bool startTimer, int currentQuestion)
    {
        this.startTimer = startTimer;
        this.currentQuestion = currentQuestion;
    }
}
