using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject Home;
    public GameObject Character;

    public int Money;
    public TextMeshProUGUI MoneyText;

    public int Score;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ScoreEndGameText;
    public TextMeshProUGUI BestScoreCountText;

    private void OnEnable()
    {
        EventManager.EndGame += EndGame;
        EventManager.Stop += Step;
    }
    private void OnDisable()
    {
        EventManager.EndGame -= EndGame;
        EventManager.Stop -= Step;
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Restart"))
        {
            Character.SetActive(true);
            Home.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Money"))
        {
            Money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            Money = 5000;
        }
        MoneyText.text = Money.ToString();
        Score = 0;
    }
    private void EndGame()
    {
        GameOverPanel.SetActive(true);
        ScoreEndGameText.text = "Score: " + Score.ToString(); 

        if (PlayerPrefs.HasKey("CubeCount"))
        {
            if (PlayerPrefs.GetInt("CubeCount") < Score)
            {
                PlayerPrefs.SetInt("CubeCount", Score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("CubeCount", Score);
        }

        BestScoreCountText.text = "BeastScore: " + PlayerPrefs.GetInt("CubeCount").ToString();

    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("Restart", 1);
    }
    public void Step()
    {
        Money = Money + 2;
        MoneyText.text = Money.ToString();
        Score = Score + 1;
        ScoreText.text = Score.ToString();
        PlayerPrefs.SetInt("Money", Money);
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
