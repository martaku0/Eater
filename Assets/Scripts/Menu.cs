using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public GameObject scoreTextObject;
    public GameObject menu;
    public Text highscoreText;
    public GameObject pause;
    public Text pauseText;
    public Text scoreText;
    public Text menuScoreText;
    int highScore;

    public GameObject v1;
    public GameObject v2;
    public GameObject v3;
    public GameObject v4;
    public GameObject v5;
    public GameObject v6;
    public GameObject v7;
    public GameObject v8;
    public GameObject v9;

    public Sprite s1_u;
    public Sprite s2_u;
    public Sprite s3_u;
    public Sprite s4_u;
    public Sprite s5_u;
    public Sprite s6_u;
    public Sprite s7_u;
    public Sprite s8_u;
    public Sprite s9_u;

    public GameObject player1;
    public GameObject player2;

    public GameObject theme_button;

    int unlockedVounchers;

    int game;

    float start_moment;

    public AudioSource menu_sound;
    public AudioSource game_sound;

    public GameObject music_button;


    // Start is called before the first frame update
    void Start()
    {
        music_button.SetActive(true);

        menu_sound.enabled = true;
        game_sound.enabled = false;

        GameObject[] vounchersArray = { v1, v2, v3, v4, v5, v6, v7, v8, v9 };
        Sprite[] unlockedSpritesArray = { s1_u, s2_u, s3_u, s4_u, s5_u, s6_u, s7_u, s8_u, s9_u };

        highScore = PlayerPrefs.GetInt("highScore", highScore);
        highscoreText.text = "Highscore: " + highScore.ToString();

        if (highScore < 10)
        {
            PlayerPrefs.SetInt("vounchers", 0);
        }
        else if (highScore >= 90)
        {
            PlayerPrefs.SetInt("vounchers", 9);
        }
        else if (highScore >= 80)
        {
            PlayerPrefs.SetInt("vounchers", 8);
        }
        else if (highScore >= 70)
        {
            PlayerPrefs.SetInt("vounchers", 7);
        }
        else if (highScore >= 60)
        {
            PlayerPrefs.SetInt("vounchers", 6);
        }
        else if (highScore >= 50)
        {
            PlayerPrefs.SetInt("vounchers", 5);
        }
        else if (highScore >= 40)
        {
            PlayerPrefs.SetInt("vounchers", 4);
        }
        else if (highScore >= 30)
        {
            PlayerPrefs.SetInt("vounchers", 3);
        }
        else if (highScore >= 20)
        {
            PlayerPrefs.SetInt("vounchers", 2);
        }
        else if (highScore >= 10)
        {
            PlayerPrefs.SetInt("vounchers", 1);
        }

        unlockedVounchers = PlayerPrefs.GetInt("vounchers", unlockedVounchers);

        for(int i = 0; i<unlockedVounchers; i++)
        {
            vounchersArray[i].GetComponent<SpriteRenderer>().sprite = unlockedSpritesArray[i];
            vounchersArray[i].tag = "Vouncher_Unlocked";
        }

        pauseText.enabled = false;

        game = 0;

        if(PlayerPrefs.HasKey("lastScore"))
        {
            menuScoreText.text = PlayerPrefs.GetString("lastScore", menuScoreText.text);
        }
        else
        {
            menuScoreText.text = ":)";
        }

        start_moment = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - start_moment > 1f)
        {
            game = 1;
        }

        if (Input.GetMouseButtonUp(0) && game == 1)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if(hit.collider.tag != "Button" && hit.collider.tag != "Vouncher_Unlocked" && hit.collider.tag != "Vouncher_Locked" && hit.collider.tag != "Theme" && hit.collider.tag != "Theme_Changed")
                {
                    goOn();
                }
            }
            else
            {
                goOn();
            }

            game = 0;
        }
    }

    void goOn()
    {
        if(PlayerPrefs.GetInt("theme") == 0)
        {
            player1.SetActive(true);
        }
        else
        {
            player2.SetActive(true);
        }
        scoreTextObject.SetActive(true);
        menu.SetActive(false);
        highscoreText.enabled = false;
        pause.SetActive(true);
        menuScoreText.enabled = false;
        music_button.SetActive(false);

        v1.SetActive(false);
        v2.SetActive(false);
        v3.SetActive(false);
        v4.SetActive(false);
        v5.SetActive(false);
        v6.SetActive(false);
        v7.SetActive(false);
        v8.SetActive(false);
        v9.SetActive(false);

        theme_button.SetActive(false);

        menu_sound.enabled = false;
        game_sound.enabled = true;

    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("lastScore");
        PlayerPrefs.Save();
    }

    
}
