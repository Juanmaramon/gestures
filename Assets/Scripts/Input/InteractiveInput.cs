using UnityEngine;
using System.Collections;

public abstract class InteractiveInput : MonoBehaviour {

	#region Private Attributes

	protected GameManager manager;
	protected bool updateEnabled;
	protected bool inputExtended;

	#endregion 

	protected virtual void Awake ()
	{
		Debug.Log("Awake InteractiveInput");
		manager = GameManager.Instance;
		updateEnabled = true;
		inputExtended = true;
	}

	protected virtual void Update ()
	{
		if (updateEnabled) 
		{
			if (manager.IsTouchDevice && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
			{
				InteractiveControl(Input.touches[0].position);	
			}	
			else if (!manager.IsTouchDevice && Input.GetMouseButtonUp(0))
			{
				InteractiveControl(Input.mousePosition);
			}

			if (inputExtended)
			{

				// Get first input touch/button pressed
				if (manager.IsTouchDevice && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
				{
					BeginInteraction(Input.touches[0].position);
				}
				else if (!manager.IsTouchDevice && Input.GetMouseButtonDown(0))
				{
					BeginInteraction(Input.mousePosition);
				}

				// Get first input touch/button pressed
				if (manager.IsTouchDevice && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
				{
					MoveInteraction(Input.touches[0].position);
				}
				else if (!manager.IsTouchDevice && Input.GetMouseButton(0))
				{
					MoveInteraction(Input.mousePosition);
				}
			}
		}
	}

	protected abstract void InteractiveControl (Vector3 position);

	protected abstract void BeginInteraction (Vector3 position);

	protected abstract void MoveInteraction (Vector3 position);
}
