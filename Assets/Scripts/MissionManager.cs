using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    private Queue<IMission> _missionQueue;
    private IMission _currentMission;

    [SerializeField]
    private TextMeshProUGUI _title;
    public IMission CurrentMission
    {
        get => _currentMission;
        private set
        {
            _currentMission = value;
        }
    }

    public bool IsCompleted => _missionQueue.Count <= 0 && _currentMission.IsCompleted;

    private void Start()
    {
        _missionQueue = new Queue<IMission>();
        _missionQueue.Enqueue(new CollectPumkinsMission(3, 0));

        LoadNewMission();
    }

    private void LoadNewMission()
    {
        if (_missionQueue.Count <= 0) return;

        _currentMission = _missionQueue.Dequeue();
        _title.text = _currentMission.Title;
    }

    public void ProcessMission() 
    {
        _currentMission.DoTask();
        _title.text = _currentMission.Title;
    } 
}
