using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsPos : MonoBehaviour
{
    [SerializeField] private Transform[] _wayPoints;

    [SerializeField] private Transform _railParent;
    [SerializeField] private GameObject _leftRailPref;
    [SerializeField] private GameObject _rightRailPref;

    private void Awake()
    {
        PathManager.instance._generatePath.waypoints = new Transform[_wayPoints.Length];
        for (var i = 0; i < _wayPoints.Length; i++)
        {
            PathManager.instance._generatePath.waypoints[i] = _wayPoints[i];
            if (i % 2 == 0)
            {
                if ((i / 2) % 2 == 0)
                {
                    _wayPoints[i].position = new Vector3(-6f, (i - 1) * (-3f), 0);
                }
                else
                {
                    _wayPoints[i].position = new Vector3(6f, (i - 1) * (-3f), 0);
                }
            }
            else
            {
                var prevPos = _wayPoints[i - 1].position;
                _wayPoints[i].position = new Vector3(prevPos.x, prevPos.y - 3f, prevPos.z);
                if ((i / 2) % 2 == 0)
                {
                    Instantiate(_leftRailPref,
                        new Vector3(_wayPoints[i].position.x + 6f, _wayPoints[i].position.y - 2.5f, _wayPoints[i].position.z),
                        _leftRailPref.transform.rotation,
                        _railParent);
                }
                else
                {
                    Instantiate(_rightRailPref,
                        new Vector3(_wayPoints[i].position.x - 6f, _wayPoints[i].position.y - 2.5f, _wayPoints[i].position.z),
                        _rightRailPref.transform.rotation,
                        _railParent);
                }
            }
        }
    }
}
