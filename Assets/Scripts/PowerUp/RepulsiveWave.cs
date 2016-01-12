using UnityEngine;
using System.Collections;

public class RepulsiveWave {

	public static string effectTag = "RepulsiveWave";
	private LayerMask waveMask = 1 << 11;
	private float waveRadius = 15.0f;
	private float waveForce = 20.0f;

	public bool runEffect(Vector3 origin, Vector3 direction){
		RaycastHit[] hits;
		hits = Physics.SphereCastAll(origin, waveRadius, direction, 0.0f, waveMask);
		if(hits != null){
			foreach(RaycastHit hit in hits){
				Vector3 dir = hit.transform.position - origin;
				hit.rigidbody.AddForce(dir * waveForce, ForceMode.Impulse);
			}
		}
		return false;
	}
}
