//Written by Rob Verhoef
using UnityEngine;
using System.Collections;

public class GuardBehaviour : MonoBehaviour
{
    public State currentState;

    public GameObject Player;

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 100);
        Debug.DrawRay(transform.position, Vector2.up, Color.green);
        if (hit.collider.gameObject.tag == "Player")
        {
            Debug.Log("I see you!");
            SetCurrentState(State.ALERT);
        }
    }

    void Alert()
    {

    }

    void Arrest()
    {

    }

    void Investigate()
    {

    }

    public enum State
    {
        PATROL,
        ALERT,
        ARREST,
        INVESTIGATE
    }
}
