using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour {

	#region Private Attributes

	private GameManager manager;

	private GameObject refFlare;
	private GameObject resultExplosion;
	private GameObject refExplosion;

	private List<GameObject> sparksRef; 
	// Save GC calls
	private GameObject refSpark;

	#endregion

	#region Public Asstributes

	[SerializeField] GameObject flare;
	[SerializeField] GameObject sparks;
	[SerializeField] GameObject explosionMobile;
	[SerializeField] GameObject explosion;
	[SerializeField] AudioClip explosionSound;

	#endregion

	void Awake ()
	{
		sparksRef = new List<GameObject>();
		manager = GameManager.Instance;

		// Get explosion quality depends of device
		if (manager.IsTouchDevice)
			resultExplosion = explosionMobile;
		else
			resultExplosion = explosion;
	}

	public void CreateFlare (Vector3 position)
	{
		refFlare = (GameObject)Instantiate(flare, Utils.ScreenPointResetZ(position), Quaternion.identity);
	}

	public void MoveFlare (Vector3 position)
	{
		refFlare.transform.position = position;
	}

	public void CreateSpark (Vector3 position)
	{
		refSpark = (GameObject)Instantiate(sparks, Utils.ScreenPointResetZ(position), Quaternion.identity);
		sparksRef.Add(refSpark);
	}

	public void DestroyFlareSparks ()
	{
		// Destroy flare trail
		foreach (GameObject spark in sparksRef)
		{
			Destroy(spark);
		}

		sparksRef.Clear();
		Destroy(refFlare);
	}

	public void CreateBigExplosion (Vector3 center)
	{
		refExplosion = (GameObject)Instantiate(resultExplosion, Utils.ScreenPointResetZ(center), Quaternion.identity);
		refExplosion.SetActive(true);
		AudioSource.PlayClipAtPoint(explosionSound, Utils.ScreenPointResetZ(center));

		StartCoroutine(DestroyExplosion(refExplosion, 2f));
	}

	private IEnumerator DestroyExplosion (GameObject explosion, float delay) 
	{
		yield return new WaitForSeconds(delay);

		Destroy(explosion);
	}
}
