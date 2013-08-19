using UnityEngine;
using System.Collections;

public class ResetGame : MonoBehaviour {

    public GameObject objectToReset;
    public GameObject guiManager;

    private Vector3 startPosition;

	void Start () {
        startPosition = objectToReset.transform.localPosition;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            objectToReset.transform.localPosition = startPosition;
            guiManager.GetComponent<GUIManager>().IncreaseTries();
            if (!objectToReset.rigidbody.isKinematic)
                objectToReset.rigidbody.velocity = new Vector3(0f, 0f, 0f);
            guiManager.GetComponent<GUIManager>().ResetTimer(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == objectToReset)
        {
            objectToReset.transform.localPosition = startPosition;
            guiManager.GetComponent<GUIManager>().IncreaseTries();
            collision.rigidbody.velocity = new Vector3(0f, 0f, 0f);
            guiManager.GetComponent<GUIManager>().ResetTimer(false);
        }
    }
}
