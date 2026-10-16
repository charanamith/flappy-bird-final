using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private bool scored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !scored)
        {
            scored = true;
            ScoreManager.Instance.AddScore();
        }
    }
}