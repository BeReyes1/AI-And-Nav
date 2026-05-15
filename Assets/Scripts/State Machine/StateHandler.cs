using System;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    private Enemy enemy;
    private StateMachine stateMachine;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        stateMachine = new StateMachine();

        //States defined
        var patrol = new PatrolState(enemy);
        var chase = new ChaseState(enemy);
        var returnState = new ReturnState(enemy);

        //Transitions defined
        Any(chase, () => enemy.PlayerInSight);
        At(chase, returnState, () => !enemy.PlayerInSight);
        At(returnState, patrol, () => returnState.ReachedWaypoint);

        stateMachine.SetState(patrol);

        /*At: Go from specific state to other specific state based on condition
          Any: Go to a specific state from any other state based on condition
        */
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    private void OnDrawGizmos()
    {
        if (stateMachine != null)
        {
            Gizmos.color = stateMachine.GetGizmosColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, 0.4f);
        }
    }
}
