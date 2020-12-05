using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // Stores the current state (using IState interface).
    private IState _currentState;

    // Dictionary of ALL states and the transitions associated with each one.
    private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
    // List of just the transitions associated with the CURRENT state.
    private List<Transition> _currentTransitions = new List<Transition>();
    // List of the transitions that can occur from ANY state at any time.
    private List<Transition> _anyTransitions = new List<Transition>();

    // Empty list of transitions.
    private static List<Transition> EmptyTransitions = new List<Transition>(0);


    /* Each AI character's Tick() function will call this one. It uses the GetTransition() function to
     * change the _currentState if necessary. Then it calls the current state's Tick() function. */
    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
            SetState(transition.To);

        _currentState?.Tick();
    }

    /* Gets called if a transition condition is met. First, checks to make sure it's not 
     * trying to transition to the state it's already in. If not, then it OnExit()s the current state
     * and switches to the new one. Then it puts the new currentState's transitions into the
     * _currentTransitions list. Then it OnEnter()s the new state. */
    public void SetState(IState state)
    {
        if (state == _currentState)
            return;

        _currentState?.OnExit();
        _currentState = state;

        _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
        if (_currentTransitions == null)
            _currentTransitions = EmptyTransitions;

        _currentState.OnEnter();
    }

    /* Will be called from AI character's script. I'll get into implementing it later, but
     * basically it adds a new Transition object (private class below) into a given state's
     * _transitions dictionary item (and if that state doesn't exist in the dictionary,
     * it adds a new item first). */
    public void AddTransition(IState from, IState to, Func<bool> predicate)
    {
        if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            _transitions[from.GetType()] = transitions;
        }

        transitions.Add(new Transition(to, predicate));
    }

    /* Same concept as ^, but it adds the new Transition object to the _anyTransitions
     * list, which doesn't associate the transition with any current ("from") state. */
    public void AddAnyTransition(IState state, Func<bool> predicate)
    {

    }

    /* Transition class just consists of a bool function for the condition, and a
     * destination ("to") state that it will tell the state machine to transition to.
     * It belongs in here because it will only be referenced in here. */
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

    /* Runs each tick. Goes through the list of transitions to check whether any of the
     * conditions were met to prompt one to occur. As soon as it finds one that does, it
     * returns that transition to the Tick() function, which uses SetState() to execute it. */
    private Transition GetTransition()
    {
        foreach (var transition in _anyTransitions)
            if (transition.Condition())
                return transition;

        foreach (var transition in _currentTransitions)
            if (transition.Condition())
                return transition;

        return null;
    }
    /* NOTE: lists will be indexed in order of priority. _anyTransitions have priority over
     * _currentTransitions. */
}
