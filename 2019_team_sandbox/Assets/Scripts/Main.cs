// -------------------------------------------------------------------------------------------------
// Main.cs
// Project: 3 Digits Redux
// Created: 2017/01/25
// Last Updated: 2018/08/21
// Copyright 2018 Dig-It! Games, LLC. All rights reserved.
// This code is licensed under the MIT License. (See LICENSE.txt for details)
//
// Note: Need to have RingBuffer.cs and TinCan included in the project.
// -------------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Text; // stringbuilder

// required for GBLXAPI
using DIG.GBLXAPI;
using TinCan;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	public Button clearButton;
	public Button submitButton;
	public Text statementText;
	public InputField hashField;
	public Button hashButton;

	// gblxapi config
	private GBLXAPI gblxapi; // could use GBLXAPI.Instance instead.

	// ------------------------------------------------------------------------
	// ------------------------------------------------------------------------
	public void Start(){

		// add GBLXAPI
		this.gblxapi = GBLXAPI.Instance; // GBLXAPI.IsActive() -- now true

		/* 
		calling init() sets up the DoNotDestroyOnLoad Singleton object for GBLxAPI.
		Use IsInit() to check for pre-existing instances before any calls to init(),
		because re-initializing will destroy any statements in the pending-to-send queue. 
		*/
		if (!this.gblxapi.IsInit()) {
			this.gblxapi.init(GBL_Interface.lrsURL, GBL_Interface.lrsUser, GBL_Interface.lrsPassword, GBL_Interface.standardsConfigDefault, GBL_Interface.standardsConfigUser);
		}

		//this.gblxapi.useDefaultCallback = true;
		this.gblxapi.debugStatement = true;
		this.gblxapi.ResetDurationSlot((int)GBL_Interface.durationSlots.Application); // using slot 0 to track time

		// add listeners
		this.clearButton.onClick.AddListener(delegate { clearButtonClicked(); });
		this.submitButton.onClick.AddListener(delegate { submitButtonClicked(); });
		this.hashButton.onClick.AddListener(delegate { hashButtonClicked(); });

		// text box
		this.statementText.text = "";
	}

	// ------------------------------------------------------------------------
	// ------------------------------------------------------------------------
	private void hashButtonClicked(){

		GBL_Interface.userUUID = this.gblxapi.GenerateActorUUID(this.hashField.text);
		this.statementText.text = GBL_Interface.userUUID;
	}

	// ------------------------------------------------------------------------
	// ------------------------------------------------------------------------
	private void clearButtonClicked(){

		this.statementText.text = "";
	}

	// ------------------------------------------------------------------------
	// ------------------------------------------------------------------------
	private void submitButtonClicked(){

		GBL_Interface.SendTestStatementStarted();
		GBL_Interface.SendTestStatementCompleted();

		this.statementText.text = "See the logs for your statement!";
	}
}
