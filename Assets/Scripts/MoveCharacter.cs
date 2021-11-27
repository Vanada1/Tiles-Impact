using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
	/// Start is called before the first frame update.
	/// </summary>
	void Start()
	{
		NextPoint.parent = null;
	}

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update()
	{
		MovePlayerCharacter();
	}

	/// <summary>
	/// Move player character.
	/// </summary>
	private void MovePlayerCharacter()
	{
		transform.position = Vector3.MoveTowards(transform.position, NextPoint.position,
			MoveSpeed * Time.deltaTime);
		if (Vector3.Distance(transform.position, NextPoint.position) <= MovingDistance)
		{
			if (_isGoing)
			{
				_isGoing = false;
				TurnChanged?.Invoke();
			}

			var isTurn = false;
			var horizontal = Input.GetAxisRaw("Horizontal");
			if (Mathf.Abs(horizontal) == 1f)
			{
                isTurn = MoveOnAxis(new Vector3(horizontal, 0f, 0f));
            }

			var vertical = Input.GetAxisRaw("Vertical");
			if (Mathf.Abs(vertical) == 1f && !isTurn)
			{
                MoveOnAxis(new Vector3(0f, vertical, 0f));
			}
		}
	}

	/// <summary>
	/// Move on one axis.
	/// </summary>
	/// <param name="vector">Next trajectory</param>
	/// <returns>True if moving.</returns>
    private bool MoveOnAxis(Vector3 vector)
    {
        if (Physics2D.OverlapCircle(NextPoint.position + vector, 0.2f, BarrierLayerMask))
        {
            return false;
        }

        _isGoing = true;
        NextPoint.position += vector;
        return true;
    }
}
