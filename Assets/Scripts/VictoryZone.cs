using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            ConditionManager.Instance.GameOver(true);
        }
    }
}
