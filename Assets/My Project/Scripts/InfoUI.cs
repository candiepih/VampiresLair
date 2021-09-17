using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoUI : MonoBehaviour
{
    private void Start() {
        Cursor.visible = true;
    }

    public void Back() {
        SceneManager.LoadScene(0);
    }
}
