using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        planning,
        running,
    }

    [SerializeField]
    private InputManager _inputManager;

    [SerializeField]
    private PlayerMover _playerMover;
    [SerializeField]
    private PlayerMovement _playerMovement;

    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _stopButton;
    private Vector3 _playerStartPosition;

    private GameState _state;
    private GameState State { get { return _state; } set { _state = value; OnStateChange?.Invoke(value); } }
    private Action<GameState> OnStateChange;

    private Coroutine _movementScheduleCoroutine;


    private void Start()
    {
        OnStateChange += (state) =>
        {
            if(state == GameState.planning)
            {
                if(_movementScheduleCoroutine != null) StopCoroutine(_movementScheduleCoroutine);
                CommandInvoker.ResetCommand();
                PlanForMoving();
            }

            if(state == GameState.running)
            {
                _movementScheduleCoroutine = StartCoroutine(InvokeMovementSchedule());
            }

            _startButton.gameObject.SetActive(state == GameState.planning);
            _stopButton.gameObject.SetActive(state == GameState.running);
            _inputManager.gameObject.SetActive(state == GameState.planning);
            _playerMover.PathRenderer.ShowPathIf(state == GameState.planning);
        };

        _startButton.onClick.AddListener(() => State = GameState.running);
        _stopButton.onClick.AddListener(() =>
        {
            _playerMover.transform.position = _playerStartPosition;
            State = GameState.planning;
        });

        State = GameState.planning;
    }


    private void PlanForMoving()
    {
        _playerMover.PathRenderer.ResetPath();
        _playerMover.PathRenderer.AddPoint(_playerMover.transform.position);
        _playerStartPosition = _playerMovement.transform.position;
    }

    private IEnumerator InvokeMovementSchedule()
    {
        _playerMovement.transform.position = _playerStartPosition;

        Queue<Vector3> pathQueue = new Queue<Vector3>();

        int numsOfPath = _playerMover.PathRenderer.GetPathList().Count;

        for (int i = 0; i < numsOfPath; i++)
        {
            pathQueue.Enqueue(_playerMover.PathRenderer.GetPathList()[i]);
        }


        while(pathQueue.Count > 0)
        {
            Vector3 destination = pathQueue.Dequeue();
            yield return StartCoroutine(MovePlayerTo(destination));
        }

        State = GameState.planning;
    }

    private IEnumerator MovePlayerTo(Vector3 destination)
    {
        destination.y = 0;
        Vector3 direction = (destination - _playerMovement.transform.position).normalized;

        while((_playerMovement.transform.position - destination).magnitude > 0.1f && _state == GameState.running)
        {
            _playerMovement.Direction = direction;
            yield return null;
        }

        _playerMovement.Direction = Vector3.zero;
    }
}
