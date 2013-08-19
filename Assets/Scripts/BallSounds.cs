using UnityEngine;
using System.Collections;

public class BallSounds : MonoBehaviour {

    public float rollingScaler = 10f, pitchScaler = 10f;

    void Start()
    {
        audio.volume = 0f;
        audio.Play();
    }

    void OnCollisionStay(Collision collision)
    {
        if (GUIManager.soundOn && (collision.gameObject.tag == "floor" || collision.gameObject.tag == "wall" || collision.gameObject.tag ==  "border"))
        {
            audio.volume = rigidbody.velocity.sqrMagnitude > 0.5f ? rigidbody.velocity.sqrMagnitude / rollingScaler : 0f;
            audio.pitch = Mathf.Sqrt(rigidbody.velocity.magnitude) / pitchScaler;
        }
        else
        {
            audio.volume = 0f;
        }
    }

    void OnCollisionExit(Collision col)
    {
        audio.volume = 0f;
    }
}
