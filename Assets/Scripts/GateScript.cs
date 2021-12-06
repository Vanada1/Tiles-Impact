using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GateScript : OffOnScriptBase
{
    // Start is called before the first frame update
    void Start()
    {
	    IsActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /// <inheritdoc/>
    protected override void ChangeAllTrap()
    {
	    SetTiles(IsActive ? OnTile : OffTile);
    }

	/// <summary>
	/// Set all tiles.
	/// </summary>
	/// <param name="tile">Changed tile.</param>
    private void SetTiles(TileBase tile)
    {
	    var tilemap = GetComponent<Tilemap>();
	    var bounds = tilemap.cellBounds;
		var points = bounds.allPositionsWithin;
	    do
	    {
		    var coordinate = points.Current;
		    var currentTile = tilemap.GetTile(coordinate);
		    if (currentTile == null)
		    {
			    continue;
		    }

		    tilemap.SetTile(coordinate, tile);
	    } while (points.MoveNext());
    }
}
