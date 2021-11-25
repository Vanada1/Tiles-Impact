using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
	/// <summary>
    /// Returns and sets current turn.
    /// </summary>
    public int CurrentTurn { get; set; } = 0;

	/// <summary>
	/// Get player object.
	/// </summary>
	public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        var moveCharacter = Player.gameObject.GetComponent<MoveCharacter>();
        moveCharacter.TurnChanged += OnTurnChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Event handler TurnChanged.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnTurnChanged(object sender, EventArgs e)
    {
        CurrentTurn++;
    }
}
