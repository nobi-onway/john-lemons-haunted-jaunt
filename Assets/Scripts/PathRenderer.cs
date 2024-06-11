using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathRenderer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;
    private Stack<GameObject> _markPoints;
    [SerializeField]
    private GameObject _sphere;

    private Stack<GameObject> GetMarkPointStack()
    {
        if (_markPoints == null) _markPoints = new Stack<GameObject>();

        return _markPoints;
    }
    
    public void AddPoint(Vector3 point)
    {
        Vector3 newPoint = point;
        newPoint.y = 1;
        GameObject markedPoint = Instantiate(_sphere, newPoint, Quaternion.identity);
        GetMarkPointStack().Push(markedPoint);
        RenderPath();
    }

    public void RemovePoint()
    {
        GameObject markPoint = GetMarkPointStack().Pop();
        Destroy(markPoint);
        RenderPath();
    }

    public List<Vector3> GetPathList()
    {
        List<Vector3> pathList = GetMarkPointStack().Select(mark => mark.transform.position).ToList();
        pathList.Reverse();
        return pathList;
    }

    public void ShowPathIf(bool canShow)
    {
        if (_markPoints == null) return;

        List<GameObject> markPoints = GetMarkPointStack().ToList();
        for(int i = 0; i < markPoints.Count; i++)
        {
            markPoints[i].SetActive(canShow);
        }
        gameObject.SetActive(canShow);
    }

    public void ResetPath()
    {
        while(GetMarkPointStack().Count > 0)
        {
            GameObject markedPoint = GetMarkPointStack().Pop();
            Destroy(markedPoint);
        }

        GetMarkPointStack().Clear();
        RenderPath();
    }

    private void RenderPath()
    {
        _lineRenderer.positionCount = GetMarkPointStack().Count;
        _lineRenderer.SetPositions(GetPathList().ToArray());
    }
}
