using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int ballValue;

    private int _score;


    void Start()
    {
        //init
        _score = 0;
        UpdateScoreText();

    }

    void UpdateScoreText()
    {
        scoreText.text = "Score:\n" + _score;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _score += ballValue;
        UpdateScoreText();
    }
}
