using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    #region Sounds
    #endregion
    #region Data
    private GameObject _startPosSpawn;
    public bool _gamePaused;
    #endregion
    #region Menus
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameObject _detectionPanel;
    #endregion
    #region Loot
    #endregion
    #region Detection
    //will need to be individual to the enemy
    [SerializeField] IntVariables _soundCounter;
    private int _soundMeterChangeTracker;
    private TMP_Text _detectionNumberTxt;
    public bool _playerIsDetected;
    public bool _playerIsCaught;
    public bool _canCooldown;
    private float _cooldownDuration = 2f;
    private float _cooldownCounter;
    private GameObject _player;
    private GameObject[] _enemyArray;
    private int _enemyCount;
    #endregion

    private void Awake()
    {
        _detectionNumberTxt = GameObject.Find("DetectionTxt").GetComponent<TMP_Text>();
        _soundMeterChangeTracker = 0;
        _startPosSpawn = GameObject.Find("RestartPoint");

        _enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        _detectionNumberTxt.text = (_soundCounter.value + "/100");
    }

    // Update is called once per frame
    void Update()
    {
        if (_soundCounter.value != _soundMeterChangeTracker)
        {
            _detectionNumberTxt.text = (_soundCounter.value + "/100");
            _soundMeterChangeTracker = _soundCounter.value;
        }

        if (_canCooldown && _cooldownCounter > _cooldownDuration && _soundCounter.value > 0)
        {
            _soundCounter.value -= 4;
            _cooldownCounter = 0f;
        }
        else { _cooldownCounter += Time.deltaTime; }
        
        //verifier si au moins un enemy est a portee
        foreach (GameObject g in _enemyArray)
        {
            if ((_player.transform.position - g.transform.position).magnitude > 10)
            {
                _enemyCount -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !_gamePaused)
        {
            PauseGame();
            _gamePaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _gamePaused)
        {
            ResumeGame();
            _gamePaused = false;
        }

        if (_playerIsDetected)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        //Time.timeScale = 0f;
        _gameOverPanel.SetActive(true);
        _detectionPanel.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        _pausePanel.SetActive(true);
        _detectionPanel.SetActive(false);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
        _detectionPanel.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        _gameOverPanel.SetActive(false);
        _detectionPanel.SetActive(true);
    }
}
