using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private MissionManager _missionManager;
    [SerializeField]
    private Transform _playerTransform;

    private PlayerMover _playerMover;
    private void Start()
    {
        _playerMover = _playerTransform.GetComponent<PlayerMover>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _playerTransform && !_playerMover.IsOnPlanning)
        {
            _missionManager.ProcessMission();
            Destroy(gameObject);
        }
    }
}
