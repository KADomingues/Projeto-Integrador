using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    private bool MovingBG;
    private bool EnableButtons = false;

    // Start is called before the first frame update
    void Start()
    {
        MovingBG = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ( transform.position.y <= -2f )
        {
            MovingBG = false;
            EnableButtons = true;
        }

        if ( MovingBG )
        {
            transform.Translate(Vector2.down * 1 * Time.deltaTime);
        }

        if (Input.anyKey && EnableButtons == false)
        {
            transform.position = new Vector3(transform.position.x, -2f, transform.position.z);
            MovingBG = false;
            EnableButtons = true;
        }
    }

    private void OnGUI()
    {
        const int buttonWidth = 100;
        const int buttonHeight = 30;

        if (EnableButtons)
            {
            if (GUI.Button(
                new Rect(Screen.width / 1.33f - (buttonWidth / 2),
                          (1.33f * Screen.height / 5f) + 50 - (buttonWidth / 2),
                          buttonWidth,
                          buttonHeight),
                    "Jogar"))
            {
                SceneManager.LoadScene("Main");
            }

            /*if (GUI.Button(
                new Rect(Screen.width / 1.33f - (buttonWidth / 2),
                          (1.33f * Screen.height / 5f) + 100 - (buttonWidth / 2),
                          buttonWidth,
                          buttonHeight),
                    "Configuraçoes"))
            {
                SceneManager.LoadScene("Config");
            }*/

            if (GUI.Button(
                new Rect(Screen.width / 1.33f - (buttonWidth / 2),
                          (1.33f * Screen.height / 5f) + 150 - (buttonWidth / 2),
                          buttonWidth,
                          buttonHeight),
                    "Sair"))
            {
                Application.Quit();
            }
        }
    }
}