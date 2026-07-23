using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    
    
    [SerializeField] private float _initialTime =10;
    [SerializeField] private bool _countDownOnGoing = true;
    [SerializeField]private float _currentTime;
    [SerializeField]private int _currentSec;
    

    private void Awake() {
        Instance = this;
    }


    private void Start()
    {
        _currentTime = _initialTime;
        _currentSec = Mathf.FloorToInt(_currentTime);
        StaticEvent.OnStageComplete += StaticEventOnOnStageComplete;
    }

    private void OnDestroy()
    {
        StaticEvent.OnStageComplete -= StaticEventOnOnStageComplete;
    }

    private void StaticEventOnOnStageComplete(object sender, EventArgs e)
    {
        _countDownOnGoing = false;
    }

    private void Update() {
        if (!_countDownOnGoing) return;
        _currentTime-= Time.deltaTime;
        if (_currentTime <= 0)
        {
            StaticEvent.DoOnGameOver();
            StaticEvent.DoOnBlockPlayerControl(true);
            _countDownOnGoing = false;
            return;
        }
        if (_currentSec != Mathf.FloorToInt(_currentTime))
        {
            _currentSec = Mathf.FloorToInt(_currentTime);
            StaticEvent.DoOnTimeChange(_currentSec);
        }
    }

    public void GetAdditionalTime(float AdditionalTime) {
        _currentTime+=  AdditionalTime;
        _currentSec = Mathf.FloorToInt(_currentTime);
        StaticEvent.DoOnTimeChange(_currentSec);
    }

    public void StartGame() {
        StaticEvent.DoOnGameStart();
        _countDownOnGoing = true;
    }
}