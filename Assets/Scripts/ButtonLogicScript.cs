using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ButtonLogicScript : MonoBehaviour
{
    /// <summary>
    /// Turn left.
    /// </summary>
	private int? _turnLeft;

	/// <summary>
    /// Tilemap current game object.
    /// </summary>
	private Tilemap _thisTilemap;

    /// <summary>
    /// Button coordinate.
    /// </summary>
	private Vector3Int _buttonCoordinate;

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

    /// <summary>
    /// Trap turn off count turn
    /// </summary>
    public int TrapTurnOffCountTurn = -1;

    // Start is called before the first frame update
    void Start()
    {
        _thisTilemap = gameObject.GetComponent<Tilemap>();
        var mainObject = GameObject.FindGameObjectWithTag("MainObject");
        var mainScript = mainObject.GetComponent<MainScript>();
        if (mainScript != null)
        {
	        mainScript.TurnChanged.AddListener(OnTurnChanged);
        }
    }

    // Update is called once per frame
    void Update()
    {
	    CheckPressButton();
    }

    /// <summary>
    /// Turn changed handle.
    /// </summary>
    private void OnTurnChanged()
    {
	    if (_turnLeft < 0)
	    {
		    return;
	    }

	    var activateScript = TrapTileMap.GetComponent<OffOnScriptBase>();
	    if (_turnLeft > 0 &&
	        !activateScript.IsActive)
	    {
		    _turnLeft--;
	    }
	    else if (!activateScript.IsActive)
	    {
		    activateScript.IsActive = true;
		    _thisTilemap.SetTile(_buttonCoordinate, UnpressedButton);
	    }
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

	    _buttonCoordinate = playerCoordinate;
	    _thisTilemap.SetTile(_buttonCoordinate, PressedButton);
	    var trapActivateScript = TrapTileMap.GetComponent<OffOnScriptBase>();
	    if (trapActivateScript != null && trapActivateScript.IsActive)
	    {
		    trapActivateScript.IsActive = false;
		    _turnLeft = TrapTurnOffCountTurn;
	    }
    }
}
