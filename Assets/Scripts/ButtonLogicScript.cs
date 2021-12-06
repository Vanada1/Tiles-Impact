using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ButtonLogicScript : MonoBehaviour
{
	/// <summary>
    /// Tilemap current game object.
    /// </summary>
	private Tilemap _thisTilemap;

    /// <summary>
    /// Trap tile map.
    /// </summary>
    public Tilemap TrapTileMap;

    /// <summary>
    /// Player.
    /// </summary>
    public GameObject Player;

    /// <summary>
    /// Pressed button tile.
    /// </summary>
    public TileBase PressedButton;

    /// <summary>
    /// Unpressed button tile.
    /// </summary>
    public TileBase UnpressedButton;

    // Start is called before the first frame update
    void Start()
    {
        _thisTilemap = gameObject.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
	    CheckPressButton();
    }

    /// <summary>
    /// Check press button
    /// </summary>
    private void CheckPressButton()
    {
	    var playerCoordinate = _thisTilemap.WorldToCell(Player.transform.position);
	    var currentTile = _thisTilemap.GetTile(playerCoordinate);
	    if (currentTile == null)
	    {
		    return;
	    }

	    _thisTilemap.SetTile(playerCoordinate, PressedButton);
	    var trapActivateScript = TrapTileMap.GetComponent<TrapActivateScript>();
	    if (trapActivateScript != null && trapActivateScript.IsActive)
	    {
		    trapActivateScript.IsActive = false;
	    }
    }
}
