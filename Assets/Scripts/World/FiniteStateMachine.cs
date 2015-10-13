//Written by Rob Verhoef
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum Transition
{
    nullTranition = 0,
}

public enum StateId
{
    nullStateId = 0,
}

public abstract class FiniteStateMachine 
{
    protected Dictionary <Transition, StateId> map = new Dictionary<Transition, StateId>();
    protected StateId stateId;
    public StateId id { get { return stateId; } }

    public void AddTransition (Transition transition2, StateId id2)
    {
        if (transition2 == Transition.nullTranition)
        {
            return;
        }

        if (id == StateId.nullStateId)
        {
            return;
        }

        if(map.ContainsKey(transition2))
        {
            map.Remove(transition2);
            return;
        }

        map.Add(transition2, id2);
    }

    //public StateId getOutputState(Transition transition2)
    //{

    //}


}
