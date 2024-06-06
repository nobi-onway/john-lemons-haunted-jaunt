using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField]
    private GameEnding _gameEnding;
    [SerializeField]
    private Transform _playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == _playerTransform)
        {
            _gameEnding.CaughtPlayer();
        }
    }
}
