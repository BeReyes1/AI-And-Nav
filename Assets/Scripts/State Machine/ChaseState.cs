using UnityEngine;

public class ChaseState : IState
{
    private readonly Enemy enemy;

    public ChaseState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        enemy.UpdateColor(GizmosColor());
    }

    public void Tick()
    {
        enemy.Agent.SetDestination(enemy.Target.position);

        if (Vector3.Distance(enemy.transform.position, enemy.Target.position) < 1.5f)
        {
            Debug.Log("You lose");
            ConditionManager.Instance.GameOver(false);
        }
    }

    public void OnExit() { }

    public Color GizmosColor() => Color.red;
}
