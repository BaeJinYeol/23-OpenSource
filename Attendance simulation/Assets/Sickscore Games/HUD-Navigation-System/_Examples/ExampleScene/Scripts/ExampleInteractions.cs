using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SickscoreGames.HUDNavigationSystem;
using SickscoreGames.ExampleScene;

public class ExampleInteractions : MonoBehaviour
{
	#region Variables
	public LayerMask layerMask = 1 << 0;
	public float interactionDistance = 4f;

	private RaycastHit hit;
	private Transform pickupText;
	private Transform rideText;
	private Transform interactionText;
	private HUDNavigationSystem _HUDNavigationSystem;
	#endregion


	#region Main Methods
	void Start ()
	{
		_HUDNavigationSystem = HUDNavigationSystem.Instance;
	}


	void Update ()
	{
		HandleKeyInput ();
		HandleItemPickUp ();
		HandleItemRide ();
		HandlePrismSceneChange();
	}
	#endregion


	#region Utility Methods
	void HandleKeyInput ()
	{
		// update radar zoom / indicator border input
		if (Input.GetKey (KeyCode.X) && _HUDNavigationSystem.radarZoom < 5f)
			_HUDNavigationSystem.radarZoom += .0175f;
		else if (Input.GetKey (KeyCode.C) && _HUDNavigationSystem.radarZoom > .25f)
			_HUDNavigationSystem.radarZoom -= .0175f;
		else if (Input.GetKey (KeyCode.V) && _HUDNavigationSystem.indicatorOffscreenBorder < .7f)
			_HUDNavigationSystem.indicatorOffscreenBorder += .01f;
		else if (Input.GetKey (KeyCode.B) && _HUDNavigationSystem.indicatorOffscreenBorder > .07f)
			_HUDNavigationSystem.indicatorOffscreenBorder -= .01f;
		else if (Input.GetKey (KeyCode.N) && _HUDNavigationSystem.minimapScale > .06f)
			_HUDNavigationSystem.minimapScale -= .0075f;
		else if (Input.GetKey (KeyCode.M) && _HUDNavigationSystem.minimapScale < .35f)
			_HUDNavigationSystem.minimapScale += .0075f;

		// update feature enable / disable input
		if (Input.GetKeyDown (KeyCode.H))
			_HUDNavigationSystem.EnableSystem (!_HUDNavigationSystem.isEnabled);
		if (Input.GetKeyDown (KeyCode.Alpha1))
			_HUDNavigationSystem.EnableRadar (!_HUDNavigationSystem.useRadar);
		if (Input.GetKeyDown (KeyCode.Alpha2))
			_HUDNavigationSystem.EnableCompassBar (!_HUDNavigationSystem.useCompassBar);
		if (Input.GetKeyDown (KeyCode.Alpha3))
			_HUDNavigationSystem.EnableIndicators (!_HUDNavigationSystem.useIndicators);
		if (Input.GetKeyDown (KeyCode.Alpha4))
			_HUDNavigationSystem.EnableMinimap (!_HUDNavigationSystem.useMinimap);

		// toggle radar / minimap mode
		if (Input.GetKeyDown (KeyCode.Alpha5))
			_HUDNavigationSystem.radarMode = (_HUDNavigationSystem.radarMode == RadarModes.RotateRadar) ? RadarModes.RotatePlayer : RadarModes.RotateRadar;
		if (Input.GetKeyDown (KeyCode.Alpha6))
			_HUDNavigationSystem.minimapMode = (_HUDNavigationSystem.minimapMode == MinimapModes.RotateMinimap) ? MinimapModes.RotatePlayer : MinimapModes.RotateMinimap;

		// toggle minimap custom layers
		if (Input.GetKeyDown (KeyCode.Alpha7) && _HUDNavigationSystem.currentMinimapProfile != null) {
			GameObject blackWhiteLayer = _HUDNavigationSystem.currentMinimapProfile.GetCustomLayer ("exampleLayer");
			if (blackWhiteLayer != null)
				blackWhiteLayer.SetActive (!blackWhiteLayer.activeSelf);
		}
	}

	void HandleItemPickUp ()
	{
		if (!_HUDNavigationSystem.isEnabled)
			return;
		
		// check for pickup items
		if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, interactionDistance, layerMask) && hit.collider.name.Contains ("PickUp"))
		{
			// get HUD navigation element component
			HUDNavigationElement element = hit.collider.gameObject.GetComponent<HUDNavigationElement> ();
			if (element != null) 
			{
				// show pickup text
				if (element.Indicator != null)
                {
					pickupText = element.Indicator.GetCustomTransform("pickupText");
					if (pickupText != null)
						pickupText.gameObject.SetActive(true);
				}

				// wait for interaction input and destroy gameobject
				if (Input.GetKeyDown(KeyCode.E))
                {
					Destroy (element.gameObject);
					if (element.gameObject.name == "PickUp BackPack")
					{
						GameManager.Instance.TakeBackPack();
					}
					else if (element.gameObject.name == "PickUp helmet")
					{
						GameManager.Instance.TakeHelmet();
					}
					else if (element.gameObject.name == "PickUp Watch")
					{
						GameManager.Instance.TakeWatch();
						TimeManager.Instance.ActiveTime();
					}
				}
			}
		} else {
			// reset pickup text
			if (pickupText != null) {
                pickupText.gameObject.SetActive(false);
                pickupText = null;
            }
		}
	}

	void HandleItemRide()
	{
		if (!_HUDNavigationSystem.isEnabled)
			return;

		// check for pickup items
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactionDistance, layerMask) 
			&& hit.collider.name.Contains("Ride") && GameManager.Instance.isHelmet)
		{
			// get HUD navigation element component
			HUDNavigationElement element = hit.collider.gameObject.GetComponent<HUDNavigationElement>();
			if (element != null)
			{
				// show pickup text
				if (element.Indicator != null)
				{
					rideText = element.Indicator.GetCustomTransform("pickupText");
					if (rideText != null)
						rideText.gameObject.SetActive(true);
				}

				// wait for interaction input and destroy gameobject
				if (Input.GetKeyDown(KeyCode.E))
                {
					GameObject.FindWithTag("vehicle").GetComponent<RideScooter>().RideTheScooter();
                }
			}
		}
		else
		{
			// reset pickup text
			if (rideText != null)
			{
				rideText.gameObject.SetActive(false);
				rideText = null;
			}
		}
	}

    void HandlePrismSceneChange()
    {
        if (!_HUDNavigationSystem.isEnabled)
            return;

        // check for colored prisms
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, interactionDistance, layerMask) 
			&& hit.collider.name.Contains("Prism") && GameManager.Instance.isBackpack)
        {
			// get HUD navigation element component
			HUDNavigationElement element = hit.collider.gameObject.GetComponentInChildren<HUDNavigationElement>();
			if (element != null)
			{
				// show interaction text
				if (element.Indicator != null)
				{
					interactionText = element.Indicator.GetCustomTransform("interactionText");
					if (interactionText != null)
						interactionText.gameObject.SetActive(true);
				}

				// wait for interaction input and change prism color
				if (Input.GetKeyDown(KeyCode.E) && SceneManager.GetActiveScene().buildIndex != 1)
				{
					if (SceneManager.GetActiveScene().buildIndex == 3)
					{
						SceneManager.LoadScene(2);
					}
					else if (SceneManager.GetActiveScene().buildIndex == 2)
					{
						SceneManager.LoadScene(1);
					}
				}
				else if (Input.GetKeyDown(KeyCode.E) && SceneManager.GetActiveScene().buildIndex == 1)
				{	
					if (hit.collider.name.Contains(GameManager.Instance.GetGoalClass()))
					{
						GameManager.Instance.GoalIn();
                    }
				}
            }
        }
        else
        {
            // reset interaction text
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
                interactionText = null;
            }
        }
    }
	#endregion
}
