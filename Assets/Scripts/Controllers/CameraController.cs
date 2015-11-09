using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public static CameraController Instance {get; private set;}
	
	private Camera _camera;

	public void Awake(){
		Instance = this;
	}
	
	public void Start(){
		_camera = Camera.main;
	}

	public IEnumerator Shake(float duration, float magnitude, float speed) {
		
		float elapsed = 0.0f;
		Quaternion initRot = _camera.transform.localRotation;
		
		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;          
			
			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
			
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			float z = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;
			z *= magnitude * damper;
			
			_camera.transform.localRotation = Quaternion.Slerp(_camera.transform.localRotation, new Quaternion(x, y, z, initRot.w), speed * Time.deltaTime);
			
			yield return null;
		}
		
		_camera.transform.localRotation = initRot;
	}
}