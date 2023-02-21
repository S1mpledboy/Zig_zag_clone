using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    private int score;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    // Update is called once per frame
    private void Awake()
    {
        PlayerController.speedMultiplier = 1;
        ChangeScoreUI();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        } 
    }
    private void StartGame()
    {
        gameStarted = true;
        FindObjectOfType<PathManager>().StarPathManager();
        startText.gameObject.SetActive(false);
    }
    public void EndGame()
    {
        gameStarted = false;
        score = 0;
        SceneManager.LoadScene(0);

    }
    public void IncreesScore()
    {
        score++;
        SetHigScore();
        ChangeScoreUI();
        IncreesePlayerSpeed();
    }
    private void ChangeScoreUI()
    {
        scoreUI.text = score.ToString();
        highScoreUI.text ="Best: "+ GetPlayerHighScore().ToString();
    }
    private void SetHigScore()
    {
        if(score > GetPlayerHighScore())
        {
            PlayerPrefs.SetInt("HighScore", score); 
        }
    }
    private int GetPlayerHighScore()
    {
        int i = PlayerPrefs.GetInt("HighScore");
        return i;
    }
    private void IncreesePlayerSpeed()
    {
        PlayerController.speedMultiplier = 1 + (score * 0.1f);
    }
}
