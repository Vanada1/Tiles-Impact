using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathScript : MonoBehaviour
{
	/// <summary>
	/// Character killed event.
	/// </summary>
	public UnityEvent CharacterKilled = new();

	/// <summary>
	/// Event system
	/// </summary>
	public GameObject EventSystem;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		var trapActivateScript = EventSystem.GetComponent<TrapActivateScript>();
		if (trapActivateScript.IsInDeathZone(transform.position))
		{
			KillCharacter();
		}
	}

	/// <summary>
	/// Kill character method.
	/// </summary>
	private void KillCharacter()
	{
		CharacterKilled.Invoke();
	}

}
