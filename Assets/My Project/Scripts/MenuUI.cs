using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUI : MonoBehaviour
{
    [SerializeField] GameObject scoreKeeper;
    [SerializeField] TextMeshProUGUI highScoreText;

    private void Start() {
        DontDestroyOnLoad(scoreKeeper);
        highScoreText.text = PlayerPrefs.GetFloat("HighScore").ToString("n0");
        Cursor.visible = true;
    }

    private void Update() {
        // highScoreText.text = ScoreKeep.highScore.ToString("n0");
    }

    public void Play() {
        SceneManager.LoadScene(1);
    }

    public void Info() {
        SceneManager.LoadScene(2);
    }

    public void Quit() {
        Application.Quit();
    }
}
