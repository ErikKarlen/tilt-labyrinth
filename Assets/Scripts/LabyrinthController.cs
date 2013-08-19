using UnityEngine;
using System.Collections;
using Leap;

public class LabyrinthController : MonoBehaviour {

    public float smooth = 2.0F, pauseTime = 3f;
    public float tiltLimit = 30.0F;//, tiltLimiter = 10f;
    public GameObject guiManager;

    private Controller controller;
    private int lastHandID;
    private float pauseTimer;
    private bool paused = false;
    //private float lastTiltAroundX = 0f, lastTiltAroundZ = 0f;

    void Start()
    {
        controller = new Controller();
        pauseTimer = Time.time;
    }

    void Update()
    {
        //float tiltAroundZ = -Input.GetAxis("Horizontal") * tiltAngle;
        //float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
        Frame frame = controller.Frame();
        if (frame.IsValid)
        {
            if (!frame.Hands.IsEmpty && !paused)
            {
                Hand hand;
                if (frame.Hands[lastHandID].IsValid)
                {
                    hand = frame.Hands[lastHandID];
                    lastHandID = hand.Id;
                }
                else
                {
                    hand = frame.Hands.Rightmost;
                }

                float tiltAroundX = -hand.Direction.Pitch * 180f / Mathf.PI;
                float tiltAroundZ = hand.PalmNormal.Roll * 180f / Mathf.PI;
                //if (Mathf.Abs(lastTiltAroundX - tiltAroundX) > tiltLimiter)
                //     tiltAroundX = lastTiltAroundX + tiltAroundX / 30f;
                // if (Mathf.Abs(lastTiltAroundZ - tiltAroundZ) > tiltLimiter)
                //      tiltAroundZ = lastTiltAroundZ + tiltAroundZ / 30f;

                //lastTiltAroundX = tiltAroundX;
                //lastTiltAroundZ = tiltAroundZ;

                if (tiltAroundX < -tiltLimit)
                    tiltAroundX = -tiltLimit;// +tiltAroundX / 5f;
                if (tiltAroundX > tiltLimit)
                    tiltAroundX = tiltLimit;// + tiltAroundX / 5f;
                if (tiltAroundZ < -tiltLimit)
                    tiltAroundZ = -tiltLimit;// +tiltAroundZ / 5f;
                if (tiltAroundZ > tiltLimit)
                    tiltAroundZ = tiltLimit;// +tiltAroundZ / 5f;

                Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
                pauseTimer = Time.time;
            }
            else if (Time.time - pauseTimer >= pauseTime)
            {
                guiManager.GetComponent<GUIManager>().PauseGame(!paused);
                paused = !paused;
                pauseTimer = Time.time;
            }
            else if (!guiManager.GetComponent<GUIManager>().roundOngoing && frame.Hands.IsEmpty)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * smooth);
                pauseTimer = Time.time;
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * smooth);
                //lastTiltAroundZ = lastTiltAroundX = 0f;
            }
        }
	}
}
