using UnityEngine.SceneManagement;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject Home;
    public GameObject Character;

    private void OnEnable()
    {
        EventManager.EndGame += EndGame;
    }
    private void OnDisable()
    {
        EventManager.EndGame -= EndGame;
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Restart"))
        {
            Character.SetActive(true);
            Home.SetActive(false);
        }
    }
    private void EndGame()
    {
        GameOverPanel.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("Restart", 1);
    }
    public void RestartHome()
    {
        SceneManager.LoadScene(0); 
        if (PlayerPrefs.HasKey("Restart"))
        {
            PlayerPrefs.DeleteKey("Restart");
        }
    }
}
