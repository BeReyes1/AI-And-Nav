using System.Collections.Generic;
using System;
using UnityEngine;

public class StateMachine
{
    private IState currentState;

    private readonly Dictionary<Type, List<Transition>> transitions = new();
    private List<Transition> currentTransitions = new();
    private readonly List<Transition> anyTransitions = new();

    private readonly static List<Transition> emptyTransitions = new(0);

    //Essentially update function
    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }

        currentState?.Tick();
    }

    public void SetState(IState state)
    {
        if (state == currentState) return;

        currentState?.OnExit();
        currentState = state;

        transitions.TryGetValue(currentState.GetType(), out currentTransitions);
        if (currentTransitions == null)
        {
            currentTransitions = emptyTransitions;
        }

        currentState.OnEnter();
    }

    //Adds a transition from a specific state to another specific state based on bool condition
    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (transitions.TryGetValue(from.GetType(), out var trans) == false)
        {
            trans = new List<Transition>();
            transitions[from.GetType()] = trans;
        }

        trans.Add(new Transition(to, predicate));
    }

    //Adds a transition to a specific state from any state based on bool condition
    public void AddAnyTransition(IState state, Func<bool> predicate)
    {
        anyTransitions.Add(new Transition(state, predicate));
    }

    public Color GetGizmosColor()
    {
        if (currentState != null)
        {
            return currentState.GizmosColor();
        }
        return Color.gray;
    }

    private class Transition
    {
        public Func<bool> Condition { get; }
        public IState To { get; }

        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }
    }

    private Transition GetTransition()
    {
        foreach (var transition in anyTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        foreach (var transition in currentTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }

        return null;
    }
}
