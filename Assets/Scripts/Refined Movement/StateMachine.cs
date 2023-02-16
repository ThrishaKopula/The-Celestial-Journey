using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
