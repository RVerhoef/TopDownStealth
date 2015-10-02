//Written by Rob Verhoef
using UnityEngine;
using System.Collections;

public class GuardBehaviour : MonoBehaviour
{
    public State currentState;

	void Start ()
    {

	}

	void Update ()
    {
	
	}

    void SetCurrentState(State state)
    {
        currentState = state;
    }

    void SwitchState()
    {
        switch (currentState)
        {

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
