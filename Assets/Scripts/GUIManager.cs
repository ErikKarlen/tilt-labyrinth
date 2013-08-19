using UnityEngine;
using System.Collections;
using System.IO;

public class GUIManager : MonoBehaviour {

    public GUIText timerText, highScoreText, triesText, countDownText, pausedText;
    public GameObject ball, pauseCover;
    public static bool soundOn = true;
    public bool roundOngoing;

    private float highScoreTime, timerTime;
    private int tries;
    private string highScoreFileName = "HighScore.txt";

	void Start () 
    {
        if (!Application.isWebPlayer)
        {
            if (!File.Exists(highScoreFileName))
            {
                highScoreText.enabled = false;
                highScoreTime = float.PositiveInfinity;
                WriteHighScoreToFile();
            }
            else
            {
                ReadHighScoreFromFile();
                highScoreText.enabled = float.IsPositiveInfinity(highScoreTime) ? false : true;
            }
        }
        else
        {
            highScoreText.enabled = false;
            highScoreTime = float.PositiveInfinity;
        }
        tries = 0;
        ResetTimer(false);
	}

    void Update() 
    {
        if (roundOngoing)
        {
            timerText.text = "Time: " + ((int)timerTime / 60).ToString("d2") + ":" + ((int)timerTime % 60).ToString("d2") + ":"+ ((int)(timerTime * 100 % 100)).ToString("d2");
            timerTime += Time.deltaTime;
        }
        highScoreText.text = "High Score: " + ((int)highScoreTime / 60).ToString("d2") + ":" + ((int)highScoreTime % 60).ToString("d2") + ":" + ((int)(highScoreTime * 100 % 100)).ToString("d2");
        triesText.text = "Tries: " + tries;
        if (Input.GetButtonDown("Sound"))
        {
            soundOn = !soundOn;
        }
	}

    public void ResetTimer(bool updateHighScore)
    {
        if (updateHighScore)
        {
            if (timerTime < highScoreTime)
                highScoreTime = timerTime;
            if (!Application.isWebPlayer)
            {
                WriteHighScoreToFile();
                ReadHighScoreFromFile();
            }
            highScoreText.enabled = true;
        }
        StartCoroutine(StartCountdown());
        timerTime = 0f;
    }
    public void IncreaseTries()
    {
        tries++;
    }

    private void ReadHighScoreFromFile()
    {
        TextReader tr = new StreamReader(highScoreFileName);
        highScoreTime = float.Parse(tr.ReadLine());
        tr.Close();
    }

    private void WriteHighScoreToFile()
    {
        TextWriter tw = new StreamWriter(highScoreFileName);
        tw.WriteLine(highScoreTime);
        tw.Close();
    }

    private IEnumerator StartCountdown()
    {
        roundOngoing = false;
        countDownText.enabled = true;
        ball.rigidbody.isKinematic = true;
        for (int i = 3; i >= 1; i--)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countDownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countDownText.enabled = false;
        ball.rigidbody.isKinematic = false;
        roundOngoing = true;
    }

    public void PauseGame(bool pause)
    {
        ball.rigidbody.isKinematic = pause;
        roundOngoing = !pause;
        pausedText.enabled = pause;
        pauseCover.renderer.enabled = pause;
    }
}
