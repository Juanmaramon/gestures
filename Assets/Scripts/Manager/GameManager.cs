using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	#region Private Attributes

	// Is touch device or desktop?
	private bool isTouchDevice;

	private bool debug;

	#endregion

	#region Public Attributes

	public bool IsTouchDevice
	{
		get { return isTouchDevice; }
	}

	public bool _Debug
	{
		get { debug = true; return debug; }
	}

	#endregion


	void Awake ()
	{
		CheckDeviceType();
	}

	/// <summary>
	/// Checks device type and assign IsTouchDevice.
	/// </summary>
	private void CheckDeviceType () 
	{
		if(SystemInfo.deviceType == DeviceType.Desktop)
		{
			isTouchDevice = false;
		}
		else if(SystemInfo.deviceType == DeviceType.Handheld)
		{
			isTouchDevice = true;
		}

		if (debug)
			Debug.Log ("********** checkDeviceType IsTouchDevice: " + isTouchDevice);
	}
}
