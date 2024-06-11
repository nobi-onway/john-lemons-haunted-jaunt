using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Stack<GameObject> _markPoints;
    [SerializeField]
    private GameObject _sphere;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _markPoints = new Stack<GameObject>();
    }
    
    public void AddPoint(Vector3 point)
    {
        Vector3 newPoint = point;
        newPoint.y = 1;
        GameObject markedPoint = Instantiate(_sphere, newPoint, Quaternion.identity);
        _markPoints.Push(markedPoint);
        RenderPath();
    }

    public void RemovePoint()
    {
        GameObject markPoint = _markPoints.Pop();
        Destroy(markPoint);
        RenderPath();
    }

    public List<Vector3> GetPathList()
    {
        List<Vector3> pathList = _markPoints.Select(mark => mark.transform.position).ToList();
        pathList.Reverse();
        return pathList;
    }

    public void ShowPathIf(bool canShow)
    {
        if (_markPoints == null) return;

        List<GameObject> markPoints = _markPoints.ToList();
        for(int i = 0; i < markPoints.Count; i++)
        {
            markPoints[i].SetActive(canShow);
        }
        gameObject.SetActive(canShow);
    }

    public void ResetPath()
    {
        _markPoints.Clear();
    }

    private void RenderPath()
    {
        _lineRenderer.positionCount = _markPoints.Count;
        _lineRenderer.SetPositions(GetPathList().ToArray());
    }
}
