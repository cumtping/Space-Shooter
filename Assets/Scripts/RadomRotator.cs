using UnityEngine;
using System.Collections;

public class RadomRotator : MonoBehaviour 
{
	public float tumble;
	// Use this for initialization
	void Start ()
	{
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
	}
}
