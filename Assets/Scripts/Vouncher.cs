using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Vouncher : MonoBehaviour
{
    public Text menuText;
    string[] descriptions = {"\ndescription",
    "\ndescription",
    "\ndescription",
    "\ndescription",
    "\ndescription",
    "\ndescription",
    "\ndescription",
    "\ndescription",
    "\ndescription"};

    int number;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        number = int.Parse(GetComponent<SpriteRenderer>().sprite.name.ToCharArray()[1].ToString())-1;
        if (PlayerPrefs.HasKey("v" + number.ToString() + "_unlocked"))
        {
            GetComponent<Renderer>().material.color = new Color(255, 255, 255, 70);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseUp()
    {

        if (PlayerPrefs.HasKey("v" + number.ToString() + "_unlocked"))
        {
            menuText.fontSize = 150;
            menuText.text = "WYKORZYSTANO" + descriptions[number];
            GetComponent<Renderer>().material.color = new Color(255, 255, 255, 70);
            //menuText.text = "WYKORZYSTANO " + number.ToString();
        }
        else
        {
            if (GetComponent<SpriteRenderer>().sprite.name.Length == 4)
            {
                menuText.text = "KUPON NA" + descriptions[number];
                //menuText.text = "opis " + number.ToString();
                menuText.fontSize = 150;

                clicked++;
                if (clicked == 1) clicktime = Time.time;

                if (clicked > 2 && Time.time - clicktime < clickdelay)
                {
                    clicked = 0;
                    clicktime = 0;
                    PlayerPrefs.SetInt("v" + number.ToString() + "_unlocked", 1);
                    menuText.text = "WYKORZYSTANO" + descriptions[number];
                    GetComponent<Renderer>().material.color = new Color(255, 255, 255, 70);
                    //menuText.text = "WYKORZYSTANO " + number.ToString();
                }
                else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
            }
            else
            {
                menuText.text = "Click to start";
                menuText.fontSize = 200;
            }
        }
    }

    /*bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
        return false;
    }*/

    void OnDestroy()
    {
        PlayerPrefs.Save();
    }
}
