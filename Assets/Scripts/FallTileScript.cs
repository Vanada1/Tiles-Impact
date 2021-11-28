using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallTileScript : MonoBehaviour
{
	/// <summary>
	/// Last coordinate tile, which has been triggered
	/// </summary>
	private Vector3Int? _titleTriggeredCoordinate;

	/// <summary>
	/// Player.
	/// </summary>
	public GameObject Player;
	
	/// <summary>
	/// Trap ground Tilemap.
	/// </summary>
	public Tilemap TrapGroundTileMap;

	/// <summary>
	/// Boundary tiles.
	/// </summary>
	public List<TileBase> BoundaryTiles;

	// Start is called before the first frame update
	void Start()
	{
		var mainScript = gameObject.GetComponent<MainScript>();
		mainScript.TurnChanged.AddListener(OnTurnChanged);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	/// <summary>
	/// Event handler turn changed.
	/// </summary>
	private void OnTurnChanged()
	{
		if (_titleTriggeredCoordinate != null)
		{
			FallTile();
		}

		var playerCoordinate = TrapGroundTileMap.WorldToCell(Player.transform.position);
		var currentTile = TrapGroundTileMap.GetTile(playerCoordinate);
		if (currentTile != null)
		{
			_titleTriggeredCoordinate = playerCoordinate;
		}
	}

	/// <summary>
	/// Drop last triggered tile.
	/// </summary>
	private void FallTile()
	{
		var currentCoordinate = (Vector3Int)_titleTriggeredCoordinate;
		var currentTile = TrapGroundTileMap.GetTile(currentCoordinate);
		if (BoundaryTiles.Contains(currentTile))
		{
			var bottomCoordinate = currentCoordinate - new Vector3Int(0, 1);
			var bottomTile = TrapGroundTileMap.GetTile(bottomCoordinate);
			if (BoundaryTiles.Contains(bottomTile))
			{
				TrapGroundTileMap.SetTile(bottomCoordinate, null);
			}
		}

		TrapGroundTileMap.SetTile(currentCoordinate, null);
	}
}
