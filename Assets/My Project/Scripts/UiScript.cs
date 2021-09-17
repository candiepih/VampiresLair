using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UiScript : MonoBehaviour
{
    // Respect order
    // 1. handgun 2. rapid 3. grenade launcher 4. flame thrower
    [SerializeField] List<GameObject> weaponsImg = new List<GameObject>();
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI ammoAmnt;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameWonScreen;
    [SerializeField] Image bossHealthBar;
    [SerializeField] TextMeshProUGUI gameWonScore;
    [SerializeField] GameObject highScoreIndicator;

    private int currentActive;

    // Start is called before the first frame update
    void Start()
    {
        currentActive = (int)SaveScript.weaponID;
        health.text = SaveScript.health.ToString();
        ammoAmnt.text = SaveScript.ammoAmnt.ToString();
        score.text = SaveScript.score.ToString("n0");
    }

    // Update is called once per frame
    void Update()
    {
        if (!SaveScript.gameOver)
        {
            SwitchWeapon();
            health.text = SaveScript.health.ToString();
            ammoAmnt.text = SaveScript.ammoAmnt.ToString();
            score.text = SaveScript.score.ToString("n0");
            if (SaveScript.ammoAmnt <= 0 && SaveScript.weaponID != 0)
                SaveScript.weaponID = 1;
            if (SaveScript.health <= 0)
                SaveScript.gameOver = true;
            bossHealthBar.fillAmount = (SaveScript.bossHealth / 100);
            if (SaveScript.bossHealth <= 0)
            {
                SaveScript.won = true;
                SaveScript.gameOver = true;
            }
        }
        else if (SaveScript.won && SaveScript.gameOver)
        {
            gameWonScreen.SetActive(true);
            UpdateHighScore();
            GameWonUI();
        }
        else
        {
            UpdateHighScore();
            gameOverScreen.SetActive(true);
        }
    }

    private void UpdateHighScore() {
        if (SaveScript.score > ScoreKeep.highScore)
        {
            ScoreKeep.highScore = SaveScript.score;
            PlayerPrefs.SetFloat("HighScore", ScoreKeep.highScore);
        }
    }

    IEnumerator HighScoreTimer()
    {
        yield return new WaitForSeconds(1);
        highScoreIndicator.SetActive(true);
    }

    private void GameWonUI()
    {
        gameWonScore.text = SaveScript.score.ToString("n0");
        Cursor.visible = true;
        if (SaveScript.score > ScoreKeep.highScore)
        {
            StartCoroutine("HighScoreTimer");
        }
    }

    private void SwitchWeapon()
    {
        if (currentActive != (SaveScript.weaponID - 1) && SaveScript.weaponID > 0)
        {
            weaponsImg[(int)(SaveScript.weaponID - 1)].SetActive(true);
            weaponsImg[currentActive].SetActive(false);
            this.currentActive = ((int)SaveScript.weaponID - 1);
            SaveScript.ammoAmnt = SaveScript.fullAmmo[this.currentActive];
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
}
