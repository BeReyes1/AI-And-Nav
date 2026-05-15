using UnityEngine;

public interface IState
{
    //Update
    void Tick();
    
    //When entering the state
    void OnEnter();

    //When exiting the state
    void OnExit();

    //Debug color on gizmos
    Color GizmosColor();
}
