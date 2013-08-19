using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public GameObject timerManager, ball;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = ball.transform.localPosition;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == ball)
        {
            ball.transform.localPosition = startPosition;
            collider.rigidbody.velocity = new Vector3(0f, 0f, 0f);
            timerManager.GetComponent<GUIManager>().ResetTimer(true);
        }
    }
}
