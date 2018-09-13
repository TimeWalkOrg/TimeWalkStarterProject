using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WebViewTriggerComponent : MonoBehaviour
{
	// TODO needs #define
	// public ZenFulcrum.EmbeddedBrowser.Browser browser;

	private void Start()
	{
	}

	private void OnEnable()
	{
		Missive.AddListener<CreditsMissive>(OnToggleCredits);
		SceneManager.activeSceneChanged += ChangedActiveScene;
	}

	private void OnDisable()
	{
		Missive.RemoveListener<CreditsMissive>(OnToggleCredits);
		SceneManager.activeSceneChanged -= ChangedActiveScene;
	}

	private void ChangedActiveScene(Scene current, Scene next)
	{
	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.tag == "VRPlayer")
		{
			this.transform.LookAt(other.transform.position);
			this.transform.rotation = Quaternion.Euler(new Vector3(0, this.transform.rotation.eulerAngles.y, 0));

		}
	}

	private void OnTriggerExit(Collider other)
	{

	}



	private void OnToggleCredits(CreditsMissive missive)
	{
	}
}
