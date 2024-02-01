using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
