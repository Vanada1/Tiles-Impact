using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuScript : MonoBehaviour
{
	private const string MainMenuName = "Menu";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Restart current level.
    /// </summary>
    public void RestartLevel()
    {
	    Application.LoadLevel(Application.loadedLevel);
    }

    /// <summary>
    /// Load main menu.
    /// </summary>
    public void LoadMainMenu()
    {
	    Application.LoadLevel(MainMenuName);
    }
}
