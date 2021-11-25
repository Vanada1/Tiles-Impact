using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using TileData = Assets.Scripts.Core.TileData;

public class TrapActivateScript : MonoBehaviour
{
	/// <summary>
    /// Main script object.
    /// </summary>
    private MainScript _mainScript;

    /// <summary>
    /// All trap tiles.
    /// </summary>
    private List<TileData> _trapTiles;

    /// <summary>
    /// Number of moves to switch traps.
    /// </summary>
    public int SwitchTurnCount = 3;

    /// <summary>
    /// Player object.
    /// </summary>
    public GameObject Player;

    /// <summary>
    /// Stakes for death.
    /// </summary>
    public TileBase Stakes;

    /// <summary>
    /// Death tile map.
    /// </summary>
    public Tilemap DeathTileMap;

    /// <summary>
    /// Trap tile map.
    /// </summary>
    public Tilemap TrapTileMap;

    // Start is called before the first frame update
    void Start()
    {
        _mainScript = gameObject.GetComponent<MainScript>();
        var moveCharacter = Player.gameObject.GetComponent<MoveCharacter>();
        moveCharacter.TurnChanged += OnTurnChanged;
        _trapTiles = GetAllTrapTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns all trap tiles.
    /// </summary>
    /// <returns>All trap tiles.</returns>
    private List<TileData> GetAllTrapTiles()
    {
	    var bounds = TrapTileMap.cellBounds;
	    var allTiles = TrapTileMap.GetTilesBlock(bounds);
	    var allTilesData = new List<TileData>();
	    for (var x = 0; x < bounds.size.x; x++)
	    {
		    for (var y = 0; y < bounds.size.y; y++)
		    {
			    var tile = allTiles[x + y * bounds.size.x];
			    if (tile != null)
			    {
				    allTilesData.Add(new TileData(x, y, tile));
			    }
		    }
	    }

        return allTilesData;
    }

    /// <summary>
    /// Event handler TurnChanged.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnTurnChanged(object sender, EventArgs e)
    {
        if (_mainScript.CurrentTurn % SwitchTurnCount == 0)
        {
	        SwitchTraps();
        }
    }

    /// <summary>
    /// Switch trap on <see cref="DeathTileMap"/>.
    /// </summary>
    private void SwitchTraps()
    {
	    foreach (var trapTile in _trapTiles)
	    {
		    var coordinate = DeathTileMap.WorldToCell(new Vector3(trapTile.X, trapTile.Y));
		    DeathTileMap.SetTile(coordinate, DeathTileMap.GetTile(coordinate) == null ? Stakes : null);
		    //DeathTileMap.SetTile(coordinate, IsNeighborDeath(trapTile) ? null : Stakes);
	    }
    }

    private bool IsNeighborDeath(TileData trapTile)
    {
	    var coordinate = DeathTileMap.WorldToCell(new Vector3(trapTile.X, trapTile.Y));
	    var neighborCoordinates = new[]
        {
		    coordinate + new Vector3Int(0, 1),
		    coordinate + new Vector3Int(-1, 0),
		    coordinate + new Vector3Int(1, 0),
		    coordinate + new Vector3Int(0, -1),
        };
	    return neighborCoordinates.Any(neighborCoordinate => DeathTileMap.GetTile(neighborCoordinate) == null);
    }
}
