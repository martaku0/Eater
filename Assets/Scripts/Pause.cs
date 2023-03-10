using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    bool pause = false;
    public Text pauseText;
    public GameObject pauseButton;
    public Sprite pauseSprite;
    public Sprite playSprite;
    SpriteRenderer rendererSprite;

    // Start is called before the first frame update
    private void Awake()
    {
        Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        pauseButton.transform.position = new Vector3(topRightScreenPoint.x - 0.5f, topRightScreenPoint.y - 0.5f, 0f);
    }

    void Start()
    {
        rendererSprite = pauseButton.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (pause)
        {
            rendererSprite.sprite = playSprite;
            AudioListener.volume = 0;
        }
        else
        {
            rendererSprite.sprite = pauseSprite;
            AudioListener.volume = 0.5f;
        }
    }

    void OnMouseUp()
    {
        if (pause)
        {
            Time.timeScale = 1;
            pause = false;
            pauseText.enabled = false;
        }
        else
        {
            Time.timeScale = 0;
            pause = true;
            pauseText.enabled = true;
        }
    }

    private void OnApplicationPause()
    {
        pause = true;
        Time.timeScale = 0;
        pauseText.enabled = true;
        rendererSprite.sprite = playSprite;
    }

    void OnEnable()
    {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Food");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        otherObjects = GameObject.FindGameObjectsWithTag("Danger");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        otherObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }


    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("lastScore");
        PlayerPrefs.Save();
    }

}
