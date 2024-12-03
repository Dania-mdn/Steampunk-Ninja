using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Diagnostics;

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

    public GameObject[] Scin;
    private int i = 0;
    private Character CharacterSc;

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
        CharacterSc = Character.GetComponent<Character>();

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
    public void Right()
    {
        if (i < 3)
        {
            i++;
        }
        else
        {
            i = 0;
        }

        for (int j = 0; j < Scin.Length; j++)
        {
            if(i == j)
            {
                Scin[j].SetActive(true);
            }
            else
            {
                Scin[j].SetActive(false);
            }
        }
    }
    public void Left()
    {
        if(i > 0)
        {
            i--;
        }
        else
        {
            i = 3;
        }

        for (int j = 0; j < Scin.Length; j++)
        {
            if(i == j)
            {
                Scin[j].SetActive(true);
            }
            else
            {
                Scin[j].SetActive(false);
            }
        }
    }
    public void Buy()
    {
        if (SetMoney(50))
        {
            for (int j = 0; j < Scin.Length; j++)
            {
                if (i == j)
                {
                    CharacterSc.Skin[j].SetActive(true);
                }
                else
                {
                    CharacterSc.Skin[j].SetActive(false);
                }
            }
        }
    }
    public bool SetMoney(int Price)
    {
        if(Money - Price >= 0)
        {
            Money = Money - Price;
            MoneyText.text = Money.ToString();
            PlayerPrefs.SetInt("Money", Money);
            return true;
        }
        else
        {
            return false;
        }
    }
}
