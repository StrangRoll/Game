using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Player _player;

    private void ChangeScoredText(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }

    private void OnEnable()
    {
        _player.ScoreChanged += ChangeScoredText;
    }

    private void OnDisable()
    {
        _player.ScoreChanged -= ChangeScoredText;
    }
}
