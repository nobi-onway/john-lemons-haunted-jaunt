using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Button _leftButton;
    [SerializeField]
    private Button _rightButton;
    [SerializeField]
    private Button _forwardButton;
    [SerializeField]
    private Button _backButton;
    [SerializeField]
    private Button _undoButton;
    [SerializeField]
    private Button _redoButton;

    [SerializeField]
    private PlayerMover _playerMover;

    private void Start()
    {
        AddEventListener();
    }

    private void OnDestroy()
    {
        RemoveEnventListener();
    }

    private void AddEventListener()
    {
        _leftButton.onClick.AddListener(() => RunPlayerMovementCommand(_playerMover, Vector3.left));
        _rightButton.onClick.AddListener(() => RunPlayerMovementCommand(_playerMover, Vector3.right));
        _backButton.onClick.AddListener(() => RunPlayerMovementCommand(_playerMover, Vector3.back));
        _forwardButton.onClick.AddListener(() => RunPlayerMovementCommand(_playerMover, Vector3.forward));

        _undoButton.onClick.AddListener(() => CommandInvoker.UndoCommand());
        _redoButton.onClick.AddListener(() => CommandInvoker.RedoCommand());
    }

    private void RemoveEnventListener()
    {
        _leftButton.onClick.RemoveAllListeners();
        _rightButton.onClick.RemoveAllListeners();
        _backButton.onClick.RemoveAllListeners();
        _forwardButton.onClick.RemoveAllListeners();

        _undoButton.onClick.RemoveAllListeners();
        _redoButton.onClick.RemoveAllListeners();
    }

    private void RunPlayerMovementCommand(PlayerMover playerMover, Vector3 movement)
    {
        if (!_playerMover) return;
        if (!playerMover.IsMovable(movement)) return;

        ICommand command = new MovementCommand(_playerMover, movement);
        CommandInvoker.ExecuteCommand(command);
    }
}
