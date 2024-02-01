using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeLeft = 59f; // time
    public TMP_Text timerText;

    public bool timerRunning = true;

    void Update()
    {
        if (timerRunning)
        {
            timeLeft -= Time.deltaTime; //keep timer running

            // show timer
            int seconds = Mathf.RoundToInt(timeLeft);
            timerText.text = seconds.ToString();

            
            if (timeLeft <= 0)
            {

                
                Time.timeScale = 0f; // freeze game
            
               
                timerRunning = false; //  stop timer

                Invoke("SwitchToMainMenu", 3f);
                Debug.Log("Switching to MainMenu");
            }
        }
    }

    void SwitchToMainMenu()
    {
    SceneManager.LoadScene(0);     
    }
}