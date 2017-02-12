﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Handles the player camera. To be changed, A LOT
/// </summary>
public class CameraHandler : MonoBehaviour 
{

	/// Do not change these
	/// They handle camera offsets, and they are fine, for the moment.
	/// </summary>
	const int yOffsetMagicNumber = 90;
	const int zOffsetMagicNumber = 120;
	public string AttachedPlayer;
	const int yOffsetClose = 30;
	const int zOffsetClose = 40;

	private float yOffset = yOffsetMagicNumber;
	private float zOffset = zOffsetMagicNumber;

	private System.Diagnostics.Stopwatch Watch;

	// Do not use this for initialization
	private void Start () 
	{ }
	
	// Update is *ignored* once per frame
	private void Update ()  
	{
		// Get the player, is there any other way? This seems dumb and risky.
		var player = GameObject.Find("Player");

		transform.position = new Vector3 (player.GetComponentInChildren<Rigidbody>().position.x, 
		                                  player.GetComponentInChildren<Rigidbody>().position.y + yOffset, 
		                                  player.GetComponentInChildren<Rigidbody>().position.z + zOffset);
		transform.LookAt (player.GetComponentInChildren<Rigidbody>().position);

		var heading = player.GetComponentInChildren<Rigidbody>().position - transform.position;
		var distance = heading.magnitude;
		var direction = heading / distance;

		//Debug.DrawRay (transform.position, heading, Color.black);
		/*
		var ray = new Ray (transform.position, heading);
		var hit = new RaycastHit ();

		if (Physics.Raycast (ray, out hit)) 
		{
			if (hit.collider.GetType () == typeof(UnityEngine.TerrainCollider)) 
			{
				SlideIn (ref this.yOffset, ref this.zOffset);
			} 
			else 
			{
				if (!IsFar () && !false /* hasfinishedrace *//*)
					SlideOut (ref this.yOffset, ref this.zOffset);
			}
		}

*/


		//Debug.DrawLine (transform.position, player.transform.position - transform.position, Color.black);
		//Debug.DrawRay (transform.position, (player.transform.position - transform.position).normalized, Color.red);
	}

	private void SlideIn(ref float yPos, ref float zPos)
	{
		if (yOffset >= yOffsetClose + 2.5f)
			yOffset -= 2.5f;

		if (zOffset >= zOffsetClose + 2.5f)
			zOffset -= 2.5f;

		Watch = new System.Diagnostics.Stopwatch ();
		Watch.Start ();
	}

	private void SlideOut(ref float yPos, ref float zPos)
	{
		if (Watch.ElapsedMilliseconds < 500 && Watch.IsRunning)
			return;
		else if (Watch.ElapsedMilliseconds >= 500 && Watch.IsRunning)
			Watch.Stop ();

		if (yOffset < yOffsetMagicNumber)
			yOffset += 2.5f;
		
		if (zOffset < zOffsetMagicNumber)
			zOffset += 2.5f;
	}

    private bool IsFar()
    {
		return (yOffset >= yOffsetMagicNumber && zOffset >= zOffsetMagicNumber);
	}
}
