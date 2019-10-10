// -------------------------------------------------------------------------------------------------
// ObjectRotation.cs
// Project: 3 Digits Redux
// Created: 2017/02/09
// Copyright 2017 Dig-It! Games, LLC. All rights reserved.
// -------------------------------------------------------------------------------------------------
using UnityEngine;

// ----------------------------------------------------------------------------
// ----------------------------------------------------------------------------
public class ObjectRotation : MonoBehaviour {
	private Vector3 rotation;

	// ------------------------------------------------------------------------
	// ------------------------------------------------------------------------
	private void Start(){
		rotation = Vector3.zero;
		rotation.y = UnityEngine.Random.Range(0,360f);
		rotation.z = UnityEngine.Random.Range(0,360f);
	}

	// ------------------------------------------------------------------------
	// ------------------------------------------------------------------------
	private void Update(){

		this.rotation.y += 1.5f;
		this.rotation.z -= 1.5f;
		this.transform.eulerAngles = this.rotation;
	}
}