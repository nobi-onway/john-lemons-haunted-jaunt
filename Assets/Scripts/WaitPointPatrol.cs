using UnityEngine;
using UnityEngine.AI;

public class WaitPointPatrol : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private Transform[] _waypoints;

    private int _currentWaypointIndex = 0;

    private void Update()
    {
        if (_navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.SetDestination(_waypoints[_currentWaypointIndex % _waypoints.Length].position);
            _currentWaypointIndex++;
        }
    }
}
