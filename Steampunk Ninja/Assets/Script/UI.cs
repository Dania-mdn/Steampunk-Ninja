using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class UI : MonoBehaviour
{
    public GameDistribution GameDistribution;
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
    public Toggle audio;
    private bool isMuteAudio;
    public AudioSource song;
    public AudioSource button;

    public TextMeshProUGUI BuyText;

    private void OnEnable()
    {
        EventManager.EndGame += EndGame;
        EventManager.Stop += Step;
        EventManager.MuteAudio += AudioMute;
        EventManager.PlayAudio += AudioPlay;
    }
    private void OnDisable()
    {
        EventManager.EndGame -= EndGame;
        EventManager.Stop -= Step;
        EventManager.MuteAudio -= AudioMute;
        EventManager.PlayAudio -= AudioPlay;
    }
    private void Start()
    {
        GameDistribution.ShowAd();
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
            Money = 0;
        }
        MoneyText.text = Money.ToString();
        Score = 0;

        if(PlayerPrefs.GetInt("MuteAudio") == 1)
        {
            audio.isOn = false;
        }

        for (int j = 0; j < Scin.Length; j++)
        {
            if (PlayerPrefs.GetInt("BuyScin") == j)
            {
                CharacterSc.Skin[j].SetActive(true);
            }
            else
            {
                CharacterSc.Skin[j].SetActive(false);
            }
        }
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

        if (PlayerPrefs.GetInt("Buy" + i) == 1)
        {
            BuyText.text = "Apply".ToString();
        }
        else
        {
            if (i == 0)
            {
                BuyText.text = "Apply".ToString();
            }
            else
            {
                BuyText.text = (50 * i).ToString();
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


        if (PlayerPrefs.GetInt("Buy" + i) == 1)
        {
            BuyText.text = "Apply".ToString();
        }
        else
        {
            if (i == 0)
            {
                BuyText.text = "Apply".ToString();
            }
            else
            {
                BuyText.text = (50 * i).ToString();
            }
        }
    }
    public void Buy()
    {
        if(PlayerPrefs.GetInt("Buy" + i) == 1)
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
        else 
        {
            if (SetMoney(50 * i))
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
                PlayerPrefs.SetInt("Buy" + i, 1);
                PlayerPrefs.SetInt("BuyScin", i);
                BuyText.text = "Apply".ToString();
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
    public void Audio()
    {
        if (isMuteAudio == false)
        {
            isMuteAudio = true;
            EventManager.DoMuteAudio();
            PlayerPrefs.SetInt("MuteAudio", 1);
        }
        else
        {
            isMuteAudio = false;
            EventManager.DoPlayAudio();
            PlayerPrefs.DeleteKey("MuteAudio");
        }
    }
    public void delete()
    {
        PlayerPrefs.DeleteAll();
    }
    public void AudioMute()
    {
        song.mute = true;
        button.mute = true;
    }
    public void AudioPlay()
    {
        song.mute = false;
        button.mute = true;
    }
}
