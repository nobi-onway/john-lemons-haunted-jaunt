using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField]
    private GameEnding _gameEnding;
    [SerializeField]
    private Transform _playerTransform;

    private PlayerMover _playerMover;

    private void Start()
    {
        _playerMover = _playerTransform.GetComponent<PlayerMover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == _playerTransform && !_playerMover.IsOnPlanning)
        {
            _gameEnding.CaughtPlayer();
        }
    }
}
