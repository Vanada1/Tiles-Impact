using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

/// <summary>
/// Class for moving character.
/// </summary>
public class MoveCharacter : MonoBehaviour
{
	/// <summary>
	/// Distance in pixels for moving.
	/// </summary>
	private const float MovingDistance = 0f;

	/// <summary>
	/// Going flag.
	/// </summary>
	private bool _isGoing = false;

	/// <summary>
	/// All Tilemaps from <see cref="Grid"/>
	/// </summary>
	private List<Tilemap> _tilemaps;

	/// <summary>
	/// Movement vector.
	/// </summary>
	private Vector2 _movement;

	/// <summary>
	/// Character move speed.
	/// </summary>
	public float MoveSpeed = 5f;

	/// <summary>
	/// Character next point.
	/// </summary>
	public Transform NextPoint;

	/// <summary>
	/// All barrier on map.
	/// </summary>
	public LayerMask BarrierLayerMask;

	/// <summary>
	/// Event on turn changed.
	/// </summary>
	public UnityEvent TurnChanged = new();

	/// <summary>
	/// Grid Tilemaps.
	/// </summary>
	public Grid Grid;

	/// <summary>
	/// Ignored Tiles.
	/// </summary>
	public List<TileBase> IgnoredTiles;

	/// <summary>
	/// Animator.
	/// </summary>
	public Animator Animator;

	/// <summary>
	/// Sprite render.
	/// </summary>
	public SpriteRenderer SpriteRenderer;

	/// <summary>
	/// Returns and sets player position.
	/// </summary>
	public Vector3 Position
	{
		get => transform.position;
		set
		{
			transform.position = value;
			NextPoint.position = value;
		}
	}

	/// <summary>
	/// Start is called before the first frame update.
	/// </summary>
	void Start()
	{
		NextPoint.parent = null;
		_tilemaps = Grid.GetComponentsInChildren<Tilemap>().ToList();
	}

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update()
	{
		MovePlayerCharacter();
	}

	void FixedUpdate()
	{
		transform.position = Vector3.MoveTowards(transform.position, NextPoint.position,
			MoveSpeed * Time.deltaTime);
	}

	/// <summary>
	/// Move player character.
	/// </summary>
	private void MovePlayerCharacter()
	{
		var horizontal = Input.GetAxisRaw("Horizontal");
		var vertical = Input.GetAxisRaw("Vertical");
		if (!_isGoing)
		{
			_movement = new Vector2(horizontal, vertical);
		}

		SpriteRenderer.flipX = horizontal < 0;
		Animator.SetFloat("Horizontal", horizontal);
		Animator.SetFloat("Vertical", vertical);
		Animator.SetFloat("Speed", _movement.sqrMagnitude);

		if (Vector3.Distance(transform.position, NextPoint.position) > MovingDistance)
		{
			return;
		}

		if (_isGoing)
		{
			_isGoing = false;
			TurnChanged?.Invoke();
			return;
		}

		var isTurn = false;
		if (Mathf.Abs(horizontal) == 1f)
		{
			isTurn = MoveOnAxis(new Vector3(horizontal, 0f, 0f));
		}

		if (Mathf.Abs(vertical) == 1f && !isTurn)
		{
			MoveOnAxis(new Vector3(0f, vertical, 0f));
		}
	}

	/// <summary>
	/// Move on one axis.
	/// </summary>
	/// <param name="vector">Next trajectory</param>
	/// <returns>True if moving.</returns>
	private bool MoveOnAxis(Vector3 vector)
	{
		if (!CanMoving(vector))
		{
			return false;
		}

		_isGoing = true;
		NextPoint.position += vector;
		return true;
	}

	private bool CanMoving(Vector3 vector)
	{
		var nextPosition = NextPoint.position + vector;
		var isVoid =
			(from tilemap in _tilemaps
				let tileMapCoordinate = tilemap.WorldToCell(nextPosition)
				select tilemap.GetTile(tileMapCoordinate))
			.All(currentTile => currentTile == null ||
			                    IgnoredTiles.Contains(currentTile));

		if (isVoid)
		{
			return false;
		}

		return !Physics2D.OverlapCircle(nextPosition, 0.2f, BarrierLayerMask);
	}
}
