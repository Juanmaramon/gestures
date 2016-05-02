using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using PDollarGestureRecognizer;

public class Utils : MonoBehaviour {

	private static Texture2D _staticRectTexture;
    private static GUIStyle _staticRectStyle;

	private static Texture2D _staticRectTextureCenter;
    private static GUIStyle _staticRectStyleCenter;

    private static Rect rect;
 
    // Note that this function is only meant to be called from OnGUI() functions.
    public static void GUIDrawRect( Rect position, Color color)
    {
        if( _staticRectTexture == null )
        {
            _staticRectTexture = new Texture2D( 1, 1 );
        }
 
        if( _staticRectStyle == null )
       	{
            _staticRectStyle = new GUIStyle();
        }

		if( _staticRectTextureCenter == null )
        {
            _staticRectTextureCenter = new Texture2D( 1, 1 );
        }
 
        if( _staticRectStyleCenter == null )
       	{
            _staticRectStyleCenter = new GUIStyle();
        }
 
        _staticRectTexture.SetPixel( 0, 0, color );
       	_staticRectTexture.Apply();
 
        _staticRectStyle.normal.background = _staticRectTexture;

		_staticRectTextureCenter.SetPixel( 0, 0, Color.red );
       	_staticRectTextureCenter.Apply();
 
		_staticRectStyleCenter.normal.background = _staticRectTextureCenter;
 
        GUI.Box( position, GUIContent.none, _staticRectStyle );

		GUI.Box(new Rect(position.center.x, position.center.y, 4, 4), GUIContent.none, _staticRectStyleCenter);
    }

	public static Vector2 CalculateRectCenter (List<Point> points)
	{
		float maxX = float.MinValue, maxY = float.MinValue; 
		float minX = float.MaxValue, minY = float.MaxValue;

		foreach (Point point in points)
		{
			if (point.X > maxX)
				maxX = point.X;
			if (point.X < minX)
				minX = point.X;

			if (point.Y > maxY)
				maxY = point.Y;
			if (point.Y < minY)
				minY = point.Y;
		}
		//Debug.Log(minX + " " + (Screen.height - Mathf.Abs(minY)) + " " + (maxX - minX) + " " + (Mathf.Abs(maxY) - Mathf.Abs(minY)));
		rect = new Rect(minX, (Screen.height - Mathf.Abs(minY)), maxX - minX, (Mathf.Abs(minY) - Mathf.Abs(maxY)));

		return rect.center;
	}

	void OnGUI ()
	{
		if (GameManager.Instance._Debug)
			GUIDrawRect(rect, Color.cyan);
	}

	/// <summary>
	/// Screen point to World reseting z.
	/// </summary>
	/// <returns>World point without z val</returns>
	/// <param name="point">Screen point.</param>
	public static Vector3 ScreenPointResetZ (Vector3 point)
	{
		// Screen point to world
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(point);

		// without deep
		worldPoint.z = 0;

		return worldPoint;
	}

	public static float ConvertInputToScreenSpace (float point)
	{
		return (Screen.height - point);
	} 
}
