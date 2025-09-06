using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameStarted;
    public GameObject platformSpawner;
    public Button HomeButton;

    [Header("For Player")]
    public GameObject[] Player;
    Vector3 playerStartPos = new Vector3(0, 2, 0);
    int selectedCar = 3;

    [Header("Game Over")]
    public GameObject GameoverPanal;
    public Text LastScoretext;
    public GameObject ScorePanal;


    [Header("Score")]
    public Text scoreText;
    public Text bestText;
    public Text heartText;
    public Text starText;

    int score = 0;
    int bestScore, totalStar, totalHeart;
    bool countScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //Get Selected Car
        selectedCar = PlayerPrefs.GetInt("SelectCar");

        Instantiate(Player[selectedCar], playerStartPos, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {

        //Total Star 
        totalHeart = PlayerPrefs.GetInt("totalHeart");
        heartText.text = totalHeart.ToString();

        //Total Heart
        totalStar = PlayerPrefs.GetInt("totalStar");
        starText.text = totalStar.ToString();

        //Best score
        bestScore = PlayerPrefs.GetInt("bestScore");
        bestText.text = bestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
            }
        }
    }

    public void GameStart()
    {
        isGameStarted = true;
        countScore = true;
        StartCoroutine(UpdateScore());
        platformSpawner.SetActive(true);
    }

    public void GameOver()
    {
        ScorePanal.SetActive(false);
        GameoverPanal.SetActive(true);
        LastScoretext.text = score.ToString();
        platformSpawner.SetActive(false);
        countScore = false;

        if (score > bestScore)
        {
            PlayerPrefs.SetInt("bestScore", score);
        }
    }

    IEnumerator UpdateScore()
    {
        while (countScore)
        {
            yield return new WaitForSeconds(1f);
            score++;
            if (score > bestScore)
            {
                bestText.text = score.ToString();
            }

            scoreText.text = score.ToString();

        }
    }

    public void Repaly()
    {
        SceneManager.LoadScene("GameScene");
    }


    public void GetStar()
    {
        int newStar = totalStar++;
        PlayerPrefs.SetInt("totalStar", newStar);
        starText.text = totalStar.ToString();
    }

    public void GetHeart()
    {
        int newHeart = totalHeart++;
        PlayerPrefs.SetInt("totalHeart", newHeart);
        heartText.text = totalHeart.ToString();
    }

    public void HomeButtonClick()
    {

        if (HomeButton == true)
        {
            SceneManager.LoadScene("GarugeScene");
        }
    }
}
