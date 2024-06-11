using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCommand : ICommand
{
    private PlayerMover _playerMover;
    private Vector3 _movement;
    public MovementCommand(PlayerMover playerMover, Vector3 movement)
    {
        this._playerMover = playerMover;
        this._movement = movement;
    }

    public void Execute()
    {
        _playerMover.PathRenderer.AddPoint(_playerMover.transform.position + _movement);
        _playerMover.Move(_movement);
    }

    public void Undo()
    {
        _playerMover.PathRenderer.RemovePoint();
        _playerMover.Move(-_movement);
    }
}
