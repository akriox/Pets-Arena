using UnityEngine;
using System;
using System.Collections;

public class RepulsiveWave {

	public static string effectTag = "RepulsiveWave";
	private LayerMask waveMask = 1 << 11;
	private float waveRadius = 10.0f;
	private float waveForce = 20.0f;
    private Player playerScript;

	public bool runEffect(GameObject caster, Vector3 origin, Vector3 direction){
		RaycastHit[] hits;
		hits = Physics.SphereCastAll(origin, waveRadius, direction, 0.0f, waveMask);
		if(hits != null){
			foreach(RaycastHit hit in hits){
                if (hit.collider.gameObject.name != caster.name){
                    Vector3 dir = hit.transform.position - origin;
                    hit.rigidbody.AddForce(dir * waveForce, ForceMode.Impulse);
                    playerScript = hit.collider.gameObject.GetComponent<Player>();
                    playerScript.repulsed = true;
                }
            }
		}
		return false;
	}
}
