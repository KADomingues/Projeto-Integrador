using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Config_Menu : MonoBehaviour
{
    [HideInInspector] public bool SomLigado = true;
    private float Volume;
    private int Som;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PPVolume"))
        {
            Volume = PlayerPrefs.GetFloat("PPVolume") * 10;
        }
        else
        {
            PlayerPrefs.SetFloat("PPVolume", Volume * 10);
        }
        if (PlayerPrefs.HasKey("PPSom"))
        {
            Som = PlayerPrefs.GetInt("PPSom");
            if (Som == 1)
            {
                SomLigado = true;
            }
            else
            {
                SomLigado = false;
            }
        }
        else
        {
            PlayerPrefs.SetFloat("PPSom", Som);
        }
    }

    void OnGUI()
    {

        SomLigado = GUI.Toggle(new Rect(Screen.width / 2 - 50,
                                         2 * Screen.height / 5f - 30, 100, 30), SomLigado, " Som Ligado");
        GUI.Label(new Rect(Screen.width / 2 - 50,
                            2 * Screen.height / 5f, 100, 30), "Volume: ");
        Volume = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 50,
                                                      2 * Screen.height / 5f + 20, 100, 30), Volume, 0.0F, 10.0F);
        if (SomLigado)
        {
            Som = 1;
        }
        else
        {
            Som = 0;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 50,
                                 2 * Screen.height / 5f + 60, 100, 30), "Salvar"))
        {
            PlayerPrefs.SetFloat("PPVolume", Volume / 10);
            //Debug.Log ("Volume:::"+Volume/10);
            PlayerPrefs.SetInt("PPSom", Som);
            SceneManager.LoadScene("Intro");
        }
    }
}
