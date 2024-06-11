using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private PathRenderer _pathRenderer;
    public PathRenderer PathRenderer => _pathRenderer;

    private float _boadSpacing = 1.0f;

    private void Start()
    {
        _pathRenderer.AddPoint(transform.position);
    }

    public void Move(Vector3 movement)
    {
        transform.position += movement;
    }

    public bool IsMovable(Vector3 movement)
    {
        return !Physics.Raycast(transform.position + Vector3.up, movement, _boadSpacing, _layerMask);
    }
}
