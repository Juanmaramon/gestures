using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using PDollarGestureRecognizer;

public class GestureTest : InteractiveInput {

	#region Private Attributes

	// Gesture reconizer list
	private List<Gesture> trainingSet = new List<Gesture>();
	// Gesture points list 
	private List<Point> points = new List<Point>();

	// Stroke id, one for every gesture
	private int strokeId = -1;

	// Scene logic
	private SceneManager scene;

	#endregion

	#region Public Attributes


	#endregion

	protected override void Awake ()
	{
		base.Awake();

		scene = GameObject.Find("SceneManager").GetComponent<SceneManager>();
	}

	void Start () 
	{
		trainingSet.Add(GestureIO.ReadGestureFromFile(Application.streamingAssetsPath + "/circle-131058885257876810.xml"));
	}
	
	protected override void Update () 
	{
		base.Update();
	}

	protected override void InteractiveControl (Vector3 position)
	{
		Debug.Log("interactive");
		++strokeId;

		// Check for not only one point
		int differences = 0;
		for (int i = 1; i < points.ToArray().Length; i++)
		{
			//Debug.Log(i + " - " + points[i].X + " - " + points[i].Y);
			if ((points[i].X != points[i-1].X) || (points[i].Y != points[i-1].Y))
				differences++;
			else
				// Destroy all sparks in scene
				scene.DestroyFlareSparks();
		}

		// If there is an input progression, check gesture
		if (differences > 0)
		{
			Gesture candidate = new Gesture(points.ToArray());
			Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

			Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);

			// Gesture ok!
			if (gestureResult.Score > 0.8)
			{
				// Correct gesture
				Debug.Log("Circle");

				// Get center of circle
				Vector2 center = Utils.CalculateRectCenter(points);

				// Big explosion!
				scene.CreateBigExplosion(new Vector3(center.x, Utils.ConvertInputToScreenSpace(center.y), 0));
			}

			// Destroy all sparks in scene
			scene.DestroyFlareSparks();
		}

		// Cleanup
		strokeId = -1;
		points.Clear();
	}

	protected override void BeginInteraction (Vector3 position)
	{
		Debug.Log("begin");

		points.Add(new Point(position.x, -position.y, strokeId));

		// Create flare
		scene.CreateFlare(position);
	}

	protected override void MoveInteraction (Vector3 position)
	{
		Debug.Log("move");

		points.Add(new Point(position.x, -position.y, strokeId));

		// Move flare with input position
		scene.MoveFlare(position);

		// Creates sparks on input position
		scene.CreateSpark(position);
	}
}
