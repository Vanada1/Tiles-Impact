using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using UnityEngine;

public class InGameMenuScript : MonoBehaviour
{
	/// <summary>
	/// Menu object name.
	/// </summary>
	private const string MenuName = "Menu";

	private IPauseable _mainScript;

	void Start()
	{
		_mainScript = InterfaceFounder.FindInterface<IPauseable>().First();
	}

	/// <summary>
	/// Resume to game level.
	/// </summary>
    public void Resume()
	{
		_mainScript.SetPause(false);
	}

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
	    Application.LoadLevel(MenuName);
    }
}
