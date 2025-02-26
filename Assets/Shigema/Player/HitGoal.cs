using UnityEngine;

public class HitGoal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var goalUIView = other.gameObject.GetComponent<GoalUIView>();
        if (goalUIView)
        {
            goalUIView.GoalView();
        }
    }
}