using UnityEngine;

public class ReturnState : IState
{
    private readonly Enemy enemy;
    public bool ReachedWaypoint => !enemy.Agent.pathPending && enemy.Agent.remainingDistance < 0.5f;

    public ReturnState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        enemy.UpdateColor(GizmosColor());
        enemy.Agent.SetDestination(enemy.Waypoints[enemy.CurrentWaypoint].position);
    }

    public void Tick() { } 

    public void OnExit() { }

    public Color GizmosColor() => Color.yellow;
}
