using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public static CameraController Instance {get; private set;}
	
	//private Camera _camera;
	private Vector3 FinalPosition = new Vector3(0f, 40.2f, -23.4f);
	private Quaternion FinalRotation = Quaternion.Euler(62.9028f, 0f, 0f);

	public void Awake(){
		Instance = this;
	}
	
	public void Start(){
		//_camera = Camera.main;
	}

	public void PlaceCamera(){
		transform.position = Vector3.Lerp (transform.position, FinalPosition, Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, FinalRotation, Time.deltaTime);
	}

	public void Update(){
		PlaceCamera ();
	}


//	public IEnumerator Shake(float duration, float magnitude, float speed) {
//		
//		float elapsed = 0.0f;
//		Quaternion initRot = _camera.transform.localRotation;
//		
//		while (elapsed < duration) {
//			
//			elapsed += Time.deltaTime;          
//			
//			float percentComplete = elapsed / duration;         
//			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
//			
//			float x = Random.value * 2.0f - 1.0f;
//			float y = Random.value * 2.0f - 1.0f;
//			float z = Random.value * 2.0f - 1.0f;
//			x *= magnitude * damper;
//			y *= magnitude * damper;
//			z *= magnitude * damper;
//			
//			_camera.transform.localRotation = Quaternion.Slerp(_camera.transform.localRotation, new Quaternion(x, y, z, initRot.w), speed * Time.deltaTime);
//			
//			yield return null;
//		}
//		
//		_camera.transform.localRotation = initRot;
//	}


}