using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadSceneAsync("StealthDemo");
    }
}
