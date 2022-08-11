using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuScript : MonoBehaviour
{
	private const string MainMenuName = "Menu";

	/// <summary>
	/// Restart current level.
	/// </summary>
	[System.Obsolete]
	public void RestartLevel()
    {
	    Application.LoadLevel(Application.loadedLevel);
    }

    /// <summary>
    /// Load main menu.
    /// </summary>
    [System.Obsolete]
    public void LoadMainMenu()
    {
	    Application.LoadLevel(MainMenuName);
    }
}
