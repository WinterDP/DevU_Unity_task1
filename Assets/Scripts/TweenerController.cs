using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenerController : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;

    private int _index = 0;

    private Mover _mover;

    private void Awake ()
    {
        _mover = GetComponent<Mover>();
    }

    private void Start ()
    {
        MoveToNextPoint();
    }

    private void MoveToNextPoint ()
    {
        if (_points.Count < 1) { return; }

        if (_index >= _points.Count) { return; }

        _mover.MoveTo(_points[_index].position, true, MoveToNextPoint);
        _index++;
    }
}
