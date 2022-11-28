using TMPro;
using UnityEngine;
using Zenject;

public class ScoreTextChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    [Inject] private Player _player;

    private void OnEnable()
    {
        _player.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _player.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }
}
