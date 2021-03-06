﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonUIComponent : MonoBehaviour
{
	public Text locationText;
	public Text timeText; //timeText.text = System.DateTime.Now.ToString("h:mm:ss tt");  //May cause garbage collection problem
	public Text yearText;
	public Text eraText; // TODO remove?
	public GameObject helpHintGO;
	public GameObject helpFullGO;

	public GameObject gameUIGO;

	public CharacterMotor characterMotorRef;

	// TODO needs #define

	public float helpHintStartDelay = 4.0f;
	public float helpHintDuration = 5.0f;

	private void Start()
	{
		helpHintGO.SetActive(false);
		helpFullGO.SetActive(false);
		gameUIGO.SetActive(true);
		Invoke("OnStartHint", helpHintStartDelay);
	}

	private void OnEnable()
	{
		Missive.AddListener<YearDataMissive>(OnYearData);
		Missive.AddListener<HelpMissive>(OnHelp);
	}

	private void OnDisable()
	{
		Missive.RemoveListener<YearDataMissive>(OnYearData);
		Missive.RemoveListener<HelpMissive>(OnHelp);
	}

	public void CloseWebView()
	{
		ControlManager.instance.SendWebViewMissive("");
	}

	private void OnYearData(YearDataMissive missive)
	{
		yearText.text = missive.data.year.ToString();
	}

	private void OnWebView(WebViewMissive missive)
	{
		// TODO needs #define
		if (missive.url != "")
		{
			gameUIGO.SetActive(false);
			ToggleFPSControl(false);
		}
		else
		{
			gameUIGO.SetActive(true);
			ToggleFPSControl(true);
		}
	}

	private void OnStartHint()
	{
		helpHintGO.SetActive(true);
	}

	private void CloseHelpFullGO()
	{
		helpFullGO.SetActive(false);
	}

	private void OnHelp(HelpMissive missive)
	{
		if (helpFullGO.activeInHierarchy)
		{
			helpFullGO.SetActive(false);
			CancelInvoke("CloseHelpFullGO");
		}
		else
		{
			helpFullGO.SetActive(true);
			Invoke("CloseHelpFullGO", helpHintDuration);
		}
	}

	private void ToggleFPSControl(bool state)
	{
		if (characterMotorRef == null) return;

		characterMotorRef.canControl = state;

		if (state)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	private void OnDestroy()
	{
		CancelInvoke();
	}
}
