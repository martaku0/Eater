using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTheme : MonoBehaviour
{
    public Text menuText;

    // Start is called before the first frame update
    void Start()
    {
        
        if (!PlayerPrefs.HasKey("theme"))
        {
            PlayerPrefs.SetInt("theme", 0);
        }
        else if(PlayerPrefs.GetInt("theme") == 0)
        {
            tag = "Theme";
        }
        else
        {
            tag = "Theme_Changed";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        if(tag == "Theme")
        {
            tag = "Theme_Changed";
            PlayerPrefs.SetInt("theme", 1);
        }
        else
        {
            tag = "Theme";
            PlayerPrefs.SetInt("theme", 0);
        }

        menuText.text = "Theme changed";
        menuText.fontSize = 180;
    }
}
