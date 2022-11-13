using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cassettes;
    float highScore;
    public float HighScore => highScore;
    [SerializeField] UIManager ui;

    void Awake()
    {
        cassettes = PlayerPrefs.GetInt("cass", 0);
        highScore = PlayerPrefs.GetFloat("highScore", 0);
    }


    public void UpdateValues(int cass, float score)
    {
        if (score > highScore)
        {
            highScore = score;
        }

        cassettes += cass;
        PlayerPrefs.SetInt("cass", cassettes);
        PlayerPrefs.SetFloat("highScore", highScore);
    }

}
