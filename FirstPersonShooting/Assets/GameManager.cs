using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI text;

    public int bombs = 0;
    public int keys = 0;
    int score;
    public bool hasBK = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int scoreIncrement)
    {
        score += scoreIncrement;
        Update();
    }

    public void ChangeBombs(int bombIncrement)
    {
        bombs += bombIncrement;
        Update();
    }
    public void ChangeKeys(int keyIncrement)
    {
        keys += keyIncrement;
        Update();
    }

    public void Update()
    {
        text.text = "Score: " + score.ToString() + "\nBombs: " + bombs.ToString() + "\nKeys: " + keys.ToString() + "\nBoss Key?: ";
        text.text += hasBK ? "yes" : "no";
    }
}
