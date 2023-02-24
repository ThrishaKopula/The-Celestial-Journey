using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//StateMachine: This is the base code for how the character transitions from different states.
//When you make a "StateMachine", you pass whichever state you start off with.
//In the Character Class, we make an empty StateMachine on Line 67 and initialize it with the idle state in Line 74.
public class StateMachine {
    public States currentState;
    
    //Initialize: This function sets the player's initial state.
    //StartingState is the variable that the player wants to be in
    public void Initialize(States startingState)
    {   
        currentState = startingState;
        startingState.Enter();
    }

    //Change State: Changes the State of the Player Character
    //newState is the new state the player's new state
    public void ChangeState(States newState)
    {
        currentState.Exit();

        currentState = newState;
        newState.Enter();
    }
}
