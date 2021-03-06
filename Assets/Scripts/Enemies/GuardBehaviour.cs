﻿//Written by Rob Verhoef
using UnityEngine;
using System.Collections;

public class GuardBehaviour : MonoBehaviour
{
    public State currentState;

    private GameObject _player;
    private Vector3 _lastKnownPosition;
    public Transform[] waypoints;
    private Transform _nextWaypoint;
    private float _patrolSpeed = 1;
    private float _chaseSpeed = 2;
    public int i = 0;
    private bool _patrolCompleted = false;

    void Start()
    {
        SetCurrentState(State.PATROL);
        _nextWaypoint = waypoints[0];
    }

    void FixedUpdate()
    {
        StateSwitcher();
    }

    void SetCurrentState(State state)
    {
        currentState = state;
    }

    void StateSwitcher()
    {
        switch (currentState)
        {
            case State.PATROL:
                Patrol();
                break;
            case State.ALERT:
                Alert();
                break;
            case State.ARREST:
                Arrest();
                break;
            case State.INVESTIGATE:
                Investigate();
                break;
        }
    }

    void Patrol()
    {

        if (transform.position != _nextWaypoint.position)
        {
            float step = _patrolSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _nextWaypoint.position, step);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _nextWaypoint.position - transform.position);
        }
        else if (transform.position == _nextWaypoint.position && i != waypoints.Length && _patrolCompleted == false)
        {
            i++;
            _nextWaypoint = waypoints[i];
        }
        else if (transform.position == _nextWaypoint.position && i != 0 && _patrolCompleted == true)
        {
            i--;
            _nextWaypoint = waypoints[i];
        }
        else if (transform.position == _nextWaypoint.position && _nextWaypoint.name == "Waypoint" + waypoints.Length)
        {
            _patrolCompleted = true;
        }
        else if (transform.position == _nextWaypoint.position && _nextWaypoint.name == "Waypoint1")
        {
            _patrolCompleted = false;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 5, 1);
        if (hit.collider.gameObject.tag == "Player")
        {
            _player = hit.collider.gameObject;
            SetCurrentState(State.ALERT);
        }
    }

    void Alert()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 5, 1);
        if (hit.collider.gameObject.tag == "Player")
        {
            float step = _chaseSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, step);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _player.transform.position - transform.position);
        }
        else if (hit.collider.gameObject.tag != "Player")
        {
            _lastKnownPosition = _player.transform.position;
            _player = null;
            SetCurrentState(State.INVESTIGATE);
        }
    }

    void Arrest()
    {

    }

    void Investigate()
    {
        float step = _patrolSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _lastKnownPosition, step);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _lastKnownPosition - transform.position);

        if (transform.position == _lastKnownPosition)
        {

        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 5, 1);
        if (hit.collider.gameObject.tag == "Player")
        {
            _player = hit.collider.gameObject;
            SetCurrentState(State.ALERT);
        }
    }

    public enum State
    {
        PATROL,
        ALERT,
        ARREST,
        INVESTIGATE
    }
}
