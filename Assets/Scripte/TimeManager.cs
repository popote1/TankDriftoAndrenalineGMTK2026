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
    [SerializeField]private int _currentMilSec;
    [SerializeField] private float _runTime=0;

    public float RunTime => _runTime;
    public float LeftTime => _currentTime;
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
        _runTime += Time.deltaTime;
        _currentTime-= Time.deltaTime;
        if (_currentTime <= 0)
        {
            StaticEvent.DoOnGameOver();
            StaticEvent.DoOnBlockPlayerControl(true);
            _countDownOnGoing = false;
            return;
        }
        if (_currentSec != Mathf.FloorToInt(_currentTime)) {
            _currentSec = Mathf.FloorToInt(_currentTime);
            StaticEvent.DoOnTimeChange(_currentSec);
        }

        if (_currentMilSec != Mathf.FloorToInt(_runTime * 100))
        {
            _currentMilSec = Mathf.FloorToInt(_runTime* 100);
            StaticEvent.DoOnTimeChangeMilSec(_currentMilSec);
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