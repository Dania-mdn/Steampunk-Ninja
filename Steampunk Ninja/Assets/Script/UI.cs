using UnityEngine.SceneManagement;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject GameOverPanel;

    private void OnEnable()
    {
        EventManager.EndGame += EndGame;
    }
    private void OnDisable()
    {
        EventManager.EndGame -= EndGame;
    }
    private void EndGame()
    {
        GameOverPanel.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
