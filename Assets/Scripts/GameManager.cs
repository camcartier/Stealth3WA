using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Sounds
    #endregion
    #region Data
    #endregion
    #region Menus
    #endregion
    #region Loot
    #endregion
    #region Detection
    //will need to be individual to the enemy
    [SerializeField] IntVariables _soundCounter;
    private int _soundMeterChangeTracker;
    private TMP_Text _detectionNumberTxt;

    #endregion

    private void Awake()
    {
        _detectionNumberTxt = GameObject.Find("DetectionTxt").GetComponent<TMP_Text>();
        _soundMeterChangeTracker = 0;
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
    }

    public void GameOver()
    {

    }

}
