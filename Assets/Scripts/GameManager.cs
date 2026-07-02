using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 30f;
    
    [SerializeField] private Ball _ball;
    [SerializeField] private CoinCollector _coinCollector;
    [SerializeField] private GameObject[] _coins;
    
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _messageText;
    
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private Vector3 _startPosition;
    
    [SerializeField] private float _fallThreshold = -5f;
    [SerializeField] private float _xLimit = 20f;
    [SerializeField] private float _zLimit = 20f;

    private float _remainingTime;
    private bool _gameOver;

    private void Start()
    {
        _remainingTime = _timeLimit;
        _gameOver = false;
        _coinCollector.ResetCoins();
        
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

        if (_gameOver) return;

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

        if (_coinCollector.CollectedCoins >= _coins.Length)
        {
            GameOver(true);
            return;
        }

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
        _remainingTime = _timeLimit;

        Vector3 respawnPos = _respawnPoint != null ? _respawnPoint.position : _startPosition;
        _ball.ResetBall(respawnPos);

        foreach (GameObject coin in _coins)
        {
            coin.SetActive(true);
        }
        _coinCollector.ResetCoins();

        _messageText.text = "";
        UpdateUI();
    }

    private void UpdateUI()
    {
        _timerText.text = _remainingTime.ToString("F2");
        _coinText.text = "Монеты: " + _coinCollector.CollectedCoins + "/" + _coins.Length;
    }
}