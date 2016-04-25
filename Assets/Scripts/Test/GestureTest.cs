using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using PDollarGestureRecognizer;

public class GestureTest : InteractiveInput {

	#region Private Attributes

	private List<Gesture> trainingSet = new List<Gesture>();
	private List<Point> points = new List<Point>();

	private int strokeId = -1;

	#endregion

	protected override void Awake ()
	{
		base.Awake();
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
		}

		// If there is an input progression, check gesture
		if (differences > 0)
		{
			Gesture candidate = new Gesture(points.ToArray());
			Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

			Debug.Log(gestureResult.GestureClass + " " + gestureResult.Score);
		}

		// Cleanup
		strokeId = -1;
		points.Clear();
	}

	protected override void BeginInteraction (Vector3 position)
	{
		Debug.Log("begin");

		points.Add(new Point(position.x, -position.y, strokeId));
	}

	protected override void MoveInteraction (Vector3 position)
	{
		Debug.Log("move");

		points.Add(new Point(position.x, -position.y, strokeId));
	}
}
