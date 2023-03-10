using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public Sprite music1;
    public Sprite music2;

    bool playing;


    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("music") || PlayerPrefs.GetInt("music") == 1)
        {
            playing = true;
        }
        else
        {
            playing = false;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playing)
        {
            PlayerPrefs.SetInt("music", 1);
            GetComponent<SpriteRenderer>().sprite = music1;
            AudioListener.volume = 0.5f;
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
            GetComponent<SpriteRenderer>().sprite = music2;
            AudioListener.volume = 0;
        }
    }

    private void OnMouseUp()
    {
        playing = !playing;
    }

    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }
}
