using UnityEngine;
using System.Collections.Generic;

public class SaveScript : MonoBehaviour
{
    public static float weaponID = 0;
    public static float score = 0;
    public static float ammoAmnt = 0;
    public static float health = 100;
    public static int minionsCount = 0;
    public static bool gameOver = false;
    public static List<int> fullAmmo = new List<int>() {500, 1500, 3, 300};
    public static float bossHealth = 100.0f;
    public static bool enteredBossLayer;
    public static bool won = false;
    // public static float highScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        weaponID = 0;
        score = 0;
        ammoAmnt = 0;
        health = 100;
        minionsCount = 0;
        gameOver = false;
        bossHealth = 100.0f;
        enteredBossLayer = false;
        won = false;
        // highScore = ScoreKeep.highScore;
    }
}
