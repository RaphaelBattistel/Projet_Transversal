using UnityEngine;

public class ScoreManager : MonoBehaviour 
{
    static ScoreManager instance;
    public int score;
    void Awake()
    {
        if(instance = null)
            instance = this;
        else
            Destroy(this);
    }

    public void UpdateScore(int valeur, bool bonneRep)
    {
        if(score <= 0 && (bonneRep = true))
        {
            score = valeur;
        }
        else if(score > 0 && (bonneRep = true))
        {
            score += valeur;
        }
        else
        {
            return;
        }
    }

    public bool CheckScore(int id1, int id2)
    {
        return id1 == id2;
    }
}
