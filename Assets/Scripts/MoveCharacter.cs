using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
	/// <summary>
	/// Character move speed.
	/// </summary>
	public float MoveSpeed = 5f;

	/// <summary>
	/// Character next point.
	/// </summary>
	public Transform NextPoint;

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

		if (Vector3.Distance(transform.position, NextPoint.position) <= 0.05f)
		{
			var horizontal = Input.GetAxisRaw("Horizontal");
			if (Mathf.Abs(horizontal) == 1f)
			{
				NextPoint.position += new Vector3(horizontal, 0f, 0f);
			}

			var vertical = Input.GetAxisRaw("Vertical");
			if (Mathf.Abs(vertical) == 1f)
			{
				NextPoint.position += new Vector3(0f, vertical, 0f);
			}
		}
	}
}
