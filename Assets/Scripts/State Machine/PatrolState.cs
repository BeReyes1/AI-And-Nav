using UnityEngine;

public class PatrolState : IState
{
    private readonly Enemy enemy;

    public PatrolState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        enemy.UpdateColor(GizmosColor());
        enemy.Agent.SetDestination(enemy.Waypoints[enemy.CurrentWaypoint].position);
    }

    public void Tick()
    {
        if (enemy.Agent.pathPending) return;

        if (enemy.Agent.remainingDistance < 0.5f)
        {
            enemy.UpdateWaypoint((enemy.CurrentWaypoint + 1) % enemy.Waypoints.Length);
            enemy.Agent.SetDestination(enemy.Waypoints[enemy.CurrentWaypoint].position);
        }
    }

    public void OnExit() { }

    public Color GizmosColor() => Color.green;
}
