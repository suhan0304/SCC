using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void Retry() {
        SceneManager.LoadScene("MainScene");
    }
}
