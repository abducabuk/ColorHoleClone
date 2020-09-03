using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{


	public void ShakeCamera(UnityAction endAction = null)
	{

		StartCoroutine(Shake(duration, magnitude,endAction));
	}

	public float duration;
	public float magnitude;

	public IEnumerator Shake(float duration, float magnitude, UnityAction endAction = null)
	{
		Vector3 orignalPosition = transform.position;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			float x = Random.Range(-1f, 1f) * magnitude;

			transform.position = new Vector3(x, transform.position.y, transform.position.z);
			elapsed += Time.deltaTime;
			yield return 0;
		}
		transform.position = orignalPosition;
		endAction?.Invoke();
	}

}
