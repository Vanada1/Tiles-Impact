using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static UnityEngine.Application;

public class MainScript : MonoBehaviour
{
	/// <summary>
	/// Returns and sets current turn.
	/// </summary>
	public int CurrentTurn { get; set; } = 0;

	/// <summary>
	/// Turn changed event.
	/// </summary>
	public UnityEvent TurnChanged = new();

	/// <summary>
	/// Get player object.
	/// </summary>
	public GameObject Player;

	// Start is called before the first frame update
	void Start()
	{
		var moveCharacter = Player.gameObject.GetComponent<MoveCharacter>();
		moveCharacter.TurnChanged.AddListener(OnTurnChanged);
		var deathScript = Player.gameObject.GetComponent<DeathScript>();
		deathScript.CharacterKilled.AddListener(OnCharacterKilled);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	/// <summary>
	/// Event handler TurnChanged.
	/// </summary>
	private void OnTurnChanged()
	{
		CurrentTurn++;
		TurnChanged?.Invoke();
	}

	/// <summary>
	/// Event handler CharacterKilled
	/// </summary>
	private void OnCharacterKilled()
	{
		CurrentTurn = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
