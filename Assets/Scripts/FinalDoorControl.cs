using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalDoorControl : MonoBehaviour
{
    public Text GameOverText;

    [HideInInspector] public bool DoorOpen = false;
    private bool GameOverState = false;

    void Update()
    {
        if (GameOverState && Input.GetKeyDown("r"))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Main");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && DoorOpen == true)
        {
            Time.timeScale = 0;
            GameOverState = true;
            GameOverText.text = "Level Complete!\nFinal Score: " + FindObjectOfType<playerController>().score;
            GameOverText.gameObject.SetActive(true);
        }
    }

    private void OnGUI()
    {
        const int buttonWidth = 125;
        const int buttonHeight = 40;

        if (GameOverState)
        {
            if (GUI.Button(
                new Rect((Screen.width / 2.5f) - (buttonWidth / 2),
                          (Screen.height / 2f) + 50 - (buttonWidth / 2),
                          buttonWidth,
                          buttonHeight),
                    "Jogar novamente"))
            {
                SceneManager.LoadScene("Main");
            }

            if (GUI.Button(
                new Rect((Screen.width / 1.66f) - (buttonWidth / 2),
                          (Screen.height / 2f) + 50 - (buttonWidth / 2),
                          buttonWidth,
                          buttonHeight),
                    "Sair"))
            {
                Application.Quit();
            }
        }
    }
}