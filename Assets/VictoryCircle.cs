using UnityEngine;

public class VictoryCircle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        P4GameManager.Instance.Victory();
    }
}
