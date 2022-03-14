using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEngine.Application;

public class MainScript : MonoBehaviour
{
	/// <summary>
	/// Loader scenes.
	/// </summary>
	private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();

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

	/// <summary>
	/// Flag tile.
	/// </summary>
	public TileBase FlagTile;

	/// <summary>
	/// Finish tile.
	/// </summary>
	public TileBase FinishTile;

	/// <summary>
	/// Start finish tile map.
	/// </summary>
	public Tilemap StartFinishTileMap;

	// Start is called before the first frame update
	void Start()
	{
		var moveCharacter = Player.gameObject.GetComponent<MoveCharacter>();
		moveCharacter.TurnChanged.AddListener(OnTurnChanged);
		var deathScript = Player.gameObject.GetComponent<DeathScript>();
		deathScript.CharacterKilled.AddListener(OnCharacterKilled);
		CurrentTurn = 0;
		LoadScenes();
		FindStart();
	}
	
	// Update is called once per frame
	void Update()
	{
		CheckFinish();
	}

	/// <summary>
	/// Loads all scenes from path.
	/// </summary>
	private void LoadScenes()
	{
		//_scenesToLoad.Add(SceneManager.LoadSceneAsync("Scene1", LoadSceneMode.Additive));
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
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	/// <summary>
	/// Find start flag.
	/// </summary>
	private void FindStart()
	{
		var position = new Vector3(0, 0);
		var points = StartFinishTileMap.cellBounds.allPositionsWithin;
		do
		{
			var coordinate = points.Current;
			var currentTile = StartFinishTileMap.GetTile(coordinate);
			if (currentTile == FlagTile)
			{
				position = StartFinishTileMap.GetCellCenterWorld(coordinate);
				break;
			}
		} while (points.MoveNext());

		var moveScript = Player.GetComponent<MoveCharacter>();
		moveScript.Position = position;
	}

	/// <summary>
	/// Checks player on finish tail.
	/// </summary>
	private void CheckFinish()
	{
		var moveScript = Player.GetComponent<MoveCharacter>();
		var point = StartFinishTileMap.WorldToCell(moveScript.Position);
		if (StartFinishTileMap.GetTile(point) == FinishTile)
		{
			var nextScene = (SceneManager.GetActiveScene().buildIndex + 1)
			                % SceneManager.sceneCountInBuildSettings;
			SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
			
		}
	}
}
