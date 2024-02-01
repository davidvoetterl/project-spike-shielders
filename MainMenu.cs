using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text countdownText;
    private float countdownTime = 5f;

    void CallResetCounters()
    {
        GameObject touchDestroyObject = GameObject.Find("ENEMY_3");

        if (touchDestroyObject != null)
        {
            TouchDestroy touchDestroyScript = touchDestroyObject.GetComponent<TouchDestroy>();

            if (touchDestroyScript != null)
            {
                touchDestroyScript.ResetCounters();
            }
        
        }
        
    }


    public void PlayGame()
    {
    
        Time.timeScale = 1f;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        // activate TMP
        countdownText.gameObject.SetActive(true);

        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        
        // go to game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // deactivate tmp countdown
        countdownText.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        CallResetCounters();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
}
