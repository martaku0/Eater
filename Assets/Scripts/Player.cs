using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    public float rotateAmount;
    float rot;

    int score;
    int highScore;
    public Text scoreText;
    public Text highscoreText;

    public GameObject foodPrefab;
    public GameObject dangerPrefab;

    public int numberOfFoodStart;
    public int numberOfDangerStart;
    public int newDanger;

    float dist = 2f;

    public GameObject music;

    //int sprint = 1;
    //float sprintStart = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfFoodStart; i++)
        {
            createObject(foodPrefab);
        }

        for (int i = 0; i < numberOfDangerStart; i++)
        {
            createObject(dangerPrefab);
        }

        score = 0;
        highScore = PlayerPrefs.GetInt("highScore", highScore);

        if (!PlayerPrefs.HasKey("music") || PlayerPrefs.GetInt("music") == 1)
        {
            music.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            music.GetComponent<AudioSource>().mute = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.timeScale == 1)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if (hit.collider.tag != "Button")
                {
                    if (mousePos.x < 0)
                    {
                        rot = rotateAmount;
                    }
                    else
                    {
                        rot = -rotateAmount;
                    }

                    transform.Rotate(0, 0, rot);
                }
            }
            else
            {
                if (mousePos.x < 0)
                {
                    rot = rotateAmount;
                }
                else
                {
                    rot = -rotateAmount;
                }

                transform.Rotate(0, 0, rot);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Danger" || collision.gameObject.tag == "Wall")
        {
            PlayerPrefs.SetString("lastScore", score.ToString());
            SceneManager.LoadScene("Game");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            score++;

            createObject(foodPrefab);

            scoreText.text = score.ToString();

            if (score != 0 && score % newDanger == 0)
            {
                createObject(dangerPrefab);
            }

            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("highScore", highScore);
            }
        }
    }

    bool isObjectHere(Vector3 pos, float d_max)
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        bool isSmth = false;
        foreach (GameObject go in allObjects)
        {
            float dist = Vector3.Distance(pos, go.transform.position);
            if (dist < d_max)
            {
                isSmth = true;
            }
        }
        return isSmth;
    }

    void createObject(GameObject obj)
    {
        Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));

        float vertical = bottomLeftScreenPoint[0] + 0.5f;
        float horizontal = bottomLeftScreenPoint[1] + 0.5f;

        float posX = Random.Range(-vertical, vertical);
        float posY = Random.Range(-horizontal, horizontal);
        int j = 0;


        while (isObjectHere(new Vector3(posX, posY, 0), dist))
        {
            posX = Random.Range(-vertical, vertical);
            posY = Random.Range(-horizontal, horizontal);
            if (j > 100 && dist == 2f)
            {
                dist = 1.5f;
            }
            else if (j > 100 && dist == 1.5f)
            {
                dist = 1f;
            }
            else if (j > 100 && dist == 1f)
            {
                dist = 0.5f;
            }
            else if (j > 100)
            {
                break;
            }
            j++;
        }
        Vector3 randomSpawnPosition = new Vector3(posX, posY, 0);
        Instantiate(obj, randomSpawnPosition, Quaternion.identity);
    }

    void OnDestroy()
    {
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("lastScore");
        PlayerPrefs.Save();
    }

}
