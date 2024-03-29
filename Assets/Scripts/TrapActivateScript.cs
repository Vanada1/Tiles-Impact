using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using TileData = Assets.Scripts.Core.TileData;

public class TrapActivateScript : OffOnScriptBase
{
	/// <summary>
	/// Trap tile map.
	/// </summary>
	private Tilemap _trapTileMap;

	/// <summary>
	/// Main script object.
	/// </summary>
	private MainScript _mainScript;

	/// <summary>
	/// Trap bounds.
	/// </summary>
	private BoundsInt _trapBounds;

	/// <summary>
	/// Number of moves to switch traps.
	/// </summary>
	public int SwitchTurnCount = 3;

	/// <summary>
	/// Player object.
	/// </summary>
	public GameObject Player;

	/// <summary>
	/// Main object.
	/// </summary>
	public GameObject MainObject;

	// Start is called before the first frame update
	void Start()
	{
		_trapTileMap = gameObject.GetComponent<Tilemap>();
		_mainScript = MainObject.GetComponent<MainScript>();
		_mainScript.TurnChanged.AddListener(OnTurnChanged);
		_trapBounds = _trapTileMap.cellBounds;
		IsActive = true;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	/// <summary>
	/// Is character in death zone.
	/// </summary>
	/// <returns>True if character is in death zone.</returns>
	public bool IsInDeathZone(Vector3 characterPosition)
	{
		var cellCoordinate = _trapTileMap.WorldToCell(characterPosition);
		return _trapTileMap.GetTile(cellCoordinate) == OnTile;
	}

	/// <summary>
	/// Event handler TurnChanged.
	/// </summary>
	private void OnTurnChanged()
	{
		if (IsActive && _mainScript.CurrentTurn % SwitchTurnCount == 0 &&
			_mainScript.CurrentTurn != 0)
		{
			SwitchTraps();
		}
	}

	/// <summary>
	/// Switch trap on <see cref="_trapTileMap"/>.
	/// </summary>
	private void SwitchTraps()
	{
		var points = _trapBounds.allPositionsWithin;
		do
		{
			var coordinate = points.Current;
			var currentTile = _trapTileMap.GetTile(coordinate);
			if (currentTile == null)
			{
				continue;
			}

			_trapTileMap.SetTile(coordinate,
				currentTile == OffTile ? OnTile : OffTile);
		} while (points.MoveNext());
	}

	/// <summary>
	/// Set start position for traps.
	/// </summary>
	private void SetStakesStartStatus()
	{
		var points = _trapBounds.allPositionsWithin;
		do
		{
			var coordinate = points.Current;
			var currentTile = _trapTileMap.GetTile(coordinate);
			if (currentTile == null)
			{
				continue;
			}

			var neighborCoordinates = new[]
			{
				coordinate + new Vector3Int(0, 1),
				coordinate + new Vector3Int(-1, 0),
				coordinate + new Vector3Int(1, 0),
				coordinate + new Vector3Int(0, -1),
			};

			foreach (var neighborCoordinate in neighborCoordinates)
			{
				var neighborTile = _trapTileMap.GetTile(neighborCoordinate);
				if (neighborTile == OffTile || neighborTile == null)
				{
					_trapTileMap.SetTile(coordinate, OnTile);
				}
				else
				{
					_trapTileMap.SetTile(coordinate, OffTile);
					break;
				}
			}
		} while (points.MoveNext());
	}


	/// <summary>
	/// Off or on all traps
	/// </summary>
	protected override void ChangeAllTrap()
	{
		if (IsActive)
		{
			SetStakesStartStatus();
		}
		else
		{
			TurnOffTraps();
		}
	}

	/// <summary>
	/// Turn off all traps.
	/// </summary>
	private void TurnOffTraps()
	{
		var points = _trapBounds.allPositionsWithin;
		do
		{
			var coordinate = points.Current;
			var currentTile = _trapTileMap.GetTile(coordinate);
			if (currentTile == null)
			{
				continue;
			}

			_trapTileMap.SetTile(coordinate, OffTile);

		} while (points.MoveNext());
	}
}
