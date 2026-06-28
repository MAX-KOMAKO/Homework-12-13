using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 30f;
    [SerializeField] private Ball _ball;
    [SerializeField] private GameObject[] _coins;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _fallThreshold = -1000f;
    [SerializeField] private float _xLimit = 50f;
    [SerializeField] private float _zLimit = 50f;

    private int _coinsCollected;
    private float _remainingTime;
    private bool _gameOver;

    private void Start()
    {
        _remainingTime = _timeLimit;
        _coinsCollected = 0;
        _gameOver = false;
        UpdateUI();
        _messageText.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
            return;
        }

        if (_gameOver)
        {
            return;
        }

        Vector3 pos = _ball.transform.position;

        if (pos.y < _fallThreshold ||
            Mathf.Abs(pos.x) > _xLimit ||
            Mathf.Abs(pos.z) > _zLimit)
        {
            GameOver(false);
            return;
        }

        _remainingTime -= Time.deltaTime;
        if (_remainingTime <= 0f)
        {
            _remainingTime = 0f;
            GameOver(false);
            return;
        }

        if (_coinsCollected >= _coins.Length)
        {
            GameOver(true);
            return;
        }

        UpdateUI();
    }

    public void CollectCoin()
    {
        _coinsCollected++;
        UpdateUI();
    }

    public void GameOver(bool won)
    {
        _gameOver = true;
        string message = won ? "Победа! Вы собрали все монеты!" : "Поражение! Мяч упал или вылетел!";
        Debug.Log(message);
        _messageText.text = message + "\nНажмите R для перезапуска";
        _timerText.text = "0.00";
        UpdateUI();
    }

    private void RestartGame()
    {
        _gameOver = false;
        _coinsCollected = 0;
        _remainingTime = _timeLimit;

        Vector3 respawnPos = _respawnPoint != null ? _respawnPoint.position : _startPosition;
        _ball.ResetBall(respawnPos);

        foreach (GameObject coin in _coins)
        {
            coin.SetActive(true);
        }
        _messageText.text = "";
        UpdateUI();
    }

    private void UpdateUI()
    {
        _timerText.text = _remainingTime.ToString("F2");
        _coinText.text = "Монеты: " + _coinsCollected + "/" + _coins.Length;
    }
}