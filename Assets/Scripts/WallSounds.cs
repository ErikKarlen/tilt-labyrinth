using UnityEngine;
using System.Collections;

public class WallSounds : MonoBehaviour {

    public AudioClip wallHitSmall, wallHitMedium, wallHitLarge;
    public float collisionScaler = 10f;
    public GameObject ball;

    void OnCollisionEnter(Collision collision)
    {
        if (GUIManager.soundOn)
        {
            if (collision.gameObject == ball)
            {
                if (collision.impactForceSum.magnitude > 1.1f)
                    audio.PlayOneShot(wallHitLarge, collision.relativeVelocity.sqrMagnitude / collisionScaler);
                else if (collision.relativeVelocity.magnitude > 0.7f)
                    audio.PlayOneShot(wallHitMedium, collision.relativeVelocity.sqrMagnitude / collisionScaler);
                else if (collision.relativeVelocity.magnitude > 0.4f)
                    audio.PlayOneShot(wallHitSmall, collision.relativeVelocity.sqrMagnitude / collisionScaler);
            }
        }
    }
}
