using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class OffOnScriptBase : MonoBehaviour
{
	/// <summary>
	/// Is active trap flag.
	/// </summary>
	private bool _isActive;

	/// <summary>
	/// On tile.
	/// </summary>
	public TileBase OnTile;

	/// <summary>
	/// Off tile.
	/// </summary>
	public TileBase OffTile;

	/// <summary>
	/// Returns and Sets is active trap flag.
	/// </summary>
	public bool IsActive
	{
		get => _isActive;
		set
		{
			_isActive = value;
			ChangeAllTrap();
		}
	}

	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Off or on all traps
    /// </summary>
	protected abstract void ChangeAllTrap();
}
