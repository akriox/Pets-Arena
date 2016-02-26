using UnityEngine;
using System.Collections;

public class AttractBall {

	public static string effectTag = "AttractBall";
	private float duration = 3.0f;
	private float gravityPull = 20.0f;

	public bool runEffect(GameObject ball, Vector3 origin, float timestamp, LineRenderer line){
		if(Time.time - timestamp <= duration){
			ball.transform.LookAt (origin);
			ball.GetComponent<Rigidbody>().AddForce(ball.transform.TransformDirection(Vector3.forward).normalized * gravityPull, ForceMode.Acceleration);
			updateLineRenderer(line, true, ball.transform.position, origin);
			return true;
		}
		else{
			updateLineRenderer(line, false, ball.transform.position, origin);
			return false;
		}
	}

	public void initLineRenderer(LineRenderer line){
		line.SetVertexCount(2);
		line.SetWidth(2.5f, 2.5f);
		line.enabled = false;
	}
	
	private void updateLineRenderer(LineRenderer line, bool value, Vector3 startPoint, Vector3 endPoint){
		line.enabled = value;
		line.SetPosition(0, startPoint);
		line.SetPosition(1, endPoint);
	}
}
