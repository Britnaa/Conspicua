using UnityEngine;
using System.Collections;

public class PlayChimes : MonoBehaviour {

	public AudioClip windchimes;

	void OnTriggerEnter(Collider other)
	{
		if(gameObject.name == "Trish")
		{
			GetComponent<AudioSource>().PlayOneShot(windchimes);
		}
	}
}