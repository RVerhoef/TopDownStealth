//Written by Rob Verhoef
using UnityEngine;
using System.Collections;

public class GuardBehaviour : MonoBehaviour
{
    public State currentState;

    private GameObject _player;
    public Vector3 _lastKnownPosition;
    private float _speed = 1;

	void Start ()
    {
        SetCurrentState(State.PATROL);
    }

	void FixedUpdate ()
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
            float step = _speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, step);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _player.transform.position - transform.position);
        }
        else
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
        float step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _lastKnownPosition, step);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, _lastKnownPosition - transform.position);

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
