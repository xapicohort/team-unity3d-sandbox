// -------------------------------------------------------------------------------------------------
// GBL_Interface.cs
// Project: GBLXAPI-Unity
// Created: 2018/07/06
// Copyright 2018 Dig-It! Games, LLC. All rights reserved.
// This code is licensed under the MIT License (see LICENSE.txt for details)
// -------------------------------------------------------------------------------------------------
using UnityEngine;

// required for GBLXAPI
using DIG.GBLXAPI;
using TinCan;
using System;
using System.Collections.Generic;
using System.Text; // required for StringBuilder()

using Newtonsoft.Json.Linq; // for extensions

// --------------------------------------------------------------------------------------
// --------------------------------------------------------------------------------------
public static class GBL_Interface {

		public enum durationSlots
	{
		Application,
		Game,
		Tutorial,
		Level
	};

    // Fill in these fields for GBLxAPI setup.//LL OPEN SOURCE
	public static string lrsURL = ""; 				// endpoint
	public static string lrsUser = "";  		// key
	public static string lrsPassword = "";
	public static string standardsConfigDefault = "data/GBLxAPI_Vocab_Default";
	public static string standardsConfigUser = "data/GBLxAPI_Vocab_User";
	public static string gameURI = "https://github.com/xapicohort/apps/team-unity3d-sandbox";
	public static string gameName = "2019 xAPI Cohort Sandbox";
	public static string companyURI = "https://github.com/xapicohort/team-unity3d-sandbox";
	public static string userUUID = "f1cd58aa-ad22-49e5-8567-d59d97d3b209";

    // ------------------------------------------------------------------------
	// Sample Gameplay GBLxAPI Triggers
	// ------------------------------------------------------------------------
	/*
	Here is where you will put functions to be called whenever you want to send a GBLxAPI statement.
	 */
	
	public static void SendTestStatementStarted(){

		Agent statementActor = GBLXAPI.Instance.CreateActorStatement(GBL_Interface.userUUID, "https://github.com/xapicohort/team-unity3d-sandbox", "Test User");
		Verb statementVerb = GBLXAPI.Instance.CreateVerbStatement("started");
		Activity statementObject = GBLXAPI.Instance.CreateObjectActivityStatement("https://github.com/xapicohort/apps/team-unity3d-sandbox", "serious-game", "Sandbox TEST");
		Result statementResult = null;

		// context can be created right in the statement functions
		List<Activity> parentList = new List<Activity>();
		parentList.Add(GBLXAPI.Instance.CreateObjectActivityStatement("https://github.com/xapicohort/apps/team-unity3d-sandbox", "serious-game", "Sandbox TEST"));

		List<Activity> groupingList = new List<Activity>();
		groupingList.Add(GBLXAPI.Instance.CreateObjectActivityStatement("https://github.com/xapicohort/team-unity3d-sandbox"));

		Context statementContext = GBLXAPI.Instance.CreateContextActivityStatement(parentList, groupingList);

		// QueueStatement(Agent statementActor, Verb statementVerb, Activity statementObject, Result statementResult, Context statementContext, StatementCallbackHandler sendCallback = null)
		GBLXAPI.Instance.QueueStatement(statementActor, statementVerb, statementObject, statementResult, statementContext);
	}

	public static void SendTestStatementCompleted(){

		Agent statementActor = GBLXAPI.Instance.CreateActorStatement(GBL_Interface.userUUID, "https://github.com/xapicohort/team-unity3d-sandbox", "Test User");
		Verb statementVerb = GBLXAPI.Instance.CreateVerbStatement("completed");
		Activity statementObject = GBLXAPI.Instance.CreateObjectActivityStatement("https://github.com/xapicohort/apps/team-unity3d-sandbox", "serious-game", "GBLXAPI TEST");

		float durationSeconds = GBLXAPI.Instance.GetDurationSlot((int)durationSlots.Application); // get delta time since start of application
		Result statementResult = GBLXAPI.Instance.CreateResultStatement(false, false, durationSeconds);

		// this time using a helper function to create context
		Context statementContext = CreateTestContext();

		GBLXAPI.Instance.QueueStatement(statementActor, statementVerb, statementObject, statementResult, statementContext);
	}

	// // ------------------------------------------------------------------------
	// // Sample Context Generators
	// // ------------------------------------------------------------------------
    /*
    Since context generation can be many lines of code, it is often helpful to separate it out into helper functions. 
    These functions will be responsible for creating Context Activities, Context Extensions, and assigning them to a singular Context object.
     */

	public static Context CreateTestContext() {

		// CONTEXT ACTIVITIES

		// parent contains the activity just above this one in the hierarchy
		List<Activity> parentList = new List<Activity>();
		parentList.Add (GBLXAPI.Instance.CreateObjectActivityStatement(gameURI, "serious-game", gameName));

		// grouping contains all other related activities to this one
		List<Activity> groupingList = new List<Activity> ();
		groupingList.Add (GBLXAPI.Instance.CreateObjectActivityStatement (companyURI));

		// category is used in GBLxAPI to report on the subject area
		List<Activity> categoryList = new List<Activity> ();;
		categoryList.Add(GBLXAPI.Instance.CreateObjectActivityStatement("https://gblxapi.org/socialstudies"));
		categoryList.Add(GBLXAPI.Instance.CreateObjectActivityStatement("https://gblxapi.org/math"));

		// CONTEXT EXTENSIONS

		// grade
		List<JToken> gradeList = new List<JToken>();
		gradeList.Add(GBLXAPI.Instance.CreateContextExtensionStatement("Grade", "Grade 4 level")); 

		// domain
		List<JToken> domainList = new List<JToken>();
		domainList.Add(GBLXAPI.Instance.CreateContextExtensionStatement("Domain", "History"));
		domainList.Add(GBLXAPI.Instance.CreateContextExtensionStatement("Domain", "Number and Operations in Base Ten"));

		// subdomain
		List<JToken> subdomainList = new List<JToken>();
		subdomainList.Add(GBLXAPI.Instance.CreateContextExtensionStatement("Subdomain", "Problem Solving"));

		// skill
		List<JToken> skillList = new List<JToken>();
		skillList.Add(GBLXAPI.Instance.CreateContextExtensionStatement("Skill","Patterns and Relationships"));
		skillList.Add(GBLXAPI.Instance.CreateContextExtensionStatement("Skill","Calculation and Computation"));
		
		// topic
		List<JToken> topicList = new List<JToken>();
		topicList.Add(GBLXAPI.Instance.CreateContextExtensionStatement("Topic","Arithmetic"));
		
		// focus
		List<JToken> focusList = new List<JToken>();
		focusList.Add(GBLXAPI.Instance.CreateContextExtensionStatement("Focus","Addition/Subtraction"));
		
		// action
		List<JToken> actionList = new List<JToken>();
		actionList.Add(GBLXAPI.Instance.CreateContextExtensionStatement("Action","Solve Problems"));
		
		// c3/ccss
		List<JToken> c3List = new List<JToken>(); 
		c3List.Add(GBLXAPI.Instance.CreateContextExtensionStatement("C3 Framework", "d2.His.13.6-8."));

		List<JToken> ccList = new List<JToken>();
		ccList.Add(GBLXAPI.Instance.CreateContextExtensionStatement("CC-MATH", "CCSS.Math.Content.4.NBT.B.4"));
		ccList.Add(GBLXAPI.Instance.CreateContextExtensionStatement("CC-MATH", "CCSS.Math.Content.5.NBT.A.1"));

		// creating TinCan.Extensions object to pack the lists into
		TinCan.Extensions contextExtensions = new TinCan.Extensions();
		// adding lists to extensions JObject
		GBLXAPI.Instance.PackExtension("domain", domainList, contextExtensions);
		GBLXAPI.Instance.PackExtension("subdomain", subdomainList, contextExtensions);
		GBLXAPI.Instance.PackExtension("topic", topicList, contextExtensions);
		GBLXAPI.Instance.PackExtension("focus", focusList, contextExtensions);
		GBLXAPI.Instance.PackExtension("action", actionList, contextExtensions);
		GBLXAPI.Instance.PackExtension("skill", skillList, contextExtensions);
		GBLXAPI.Instance.PackExtension("grade", gradeList, contextExtensions);
		GBLXAPI.Instance.PackExtension("cc", ccList, contextExtensions);
		GBLXAPI.Instance.PackExtension("c3", c3List, contextExtensions);

		// Folding all of the above into our Context object to be used in the statement
		Context statementContext = GBLXAPI.Instance.CreateContextActivityStatement(parentList, groupingList, categoryList, contextExtensions);
		return statementContext;
	}
}