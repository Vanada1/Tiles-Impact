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
    /// Stakes for death.
    /// </summary>
    public TileBase Stakes;

    /// <summary>
    /// Trap whole.
    /// </summary>
    public TileBase TrapWhole;

    /// <summary>
    /// Trap tile map.
    /// </summary>
    public Tilemap TrapTileMap;

    // Start is called before the first frame update
    void Start()
    {
        _mainScript = gameObject.GetComponent<MainScript>();
        var moveCharacter = Player.gameObject.GetComponent<MoveCharacter>();
        moveCharacter.TurnChanged.AddListener(OnTurnChanged);
        var deathScript = Player.gameObject.GetComponent<DeathScript>();
        deathScript.CharacterKilled.AddListener(OnCharacterKilled);
        _trapBounds = TrapTileMap.cellBounds;
        SetStakesStartStatus();
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
	    var cellCoordinate = TrapTileMap.WorldToCell(characterPosition);
	    return TrapTileMap.GetTile(cellCoordinate) == Stakes;
    }

    /// <summary>
    /// Event handler TurnChanged.
    /// </summary>
    private void OnTurnChanged()
    {
        if (_mainScript.CurrentTurn % SwitchTurnCount == 0 &&
            _mainScript.CurrentTurn != 0)
        {
	        SwitchTraps();
        }
    }

    /// <summary>
    /// Event handler CharacterKilled
    /// </summary>
    private void OnCharacterKilled()
    {
	    var points = _trapBounds.allPositionsWithin;
	    do
	    {
		    var coordinate = points.Current;
		    var currentTile = TrapTileMap.GetTile(coordinate);
		    if (currentTile == null)
		    {
			    continue;
		    }

		    TrapTileMap.SetTile(coordinate, TrapWhole);
	    } while (points.MoveNext());
        SetStakesStartStatus();
    }

    /// <summary>
    /// Switch trap on <see cref="TrapTileMap"/>.
    /// </summary>
    private void SwitchTraps()
    {
	    var points = _trapBounds.allPositionsWithin;
	    do
	    {
		    var coordinate = points.Current;
		    var currentTile = TrapTileMap.GetTile(coordinate);
		    if (currentTile == null)
		    {
                continue;
		    }

		    TrapTileMap.SetTile(coordinate,
			    currentTile == TrapWhole ? Stakes : TrapWhole);
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
		    var currentTile = TrapTileMap.GetTile(coordinate);
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
			    var neighborTile = TrapTileMap.GetTile(neighborCoordinate);
			    if (neighborTile == TrapWhole || neighborTile == null)
			    {
				    TrapTileMap.SetTile(coordinate, Stakes);
			    }
			    else
			    {
				    TrapTileMap.SetTile(coordinate, TrapWhole);
                    break;
                }
		    }
	    } while (points.MoveNext());
    }
}
