using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	private Rigidbody rb;

	public float bouncePower;

	public float maxSpeed;

	public float kickAngle;
	public float kickPower;

	public bool kicked = false;
	public bool trapped = false;
	public bool bouncy = false;
	public float stunDelay;
	public enum Zone {R, G, B, Y, N};
	public Zone currentZone;

	private Material _mat;
	private Color defaultColor;

	void Awake(){
		_mat = GetComponent<Renderer>().material;
		defaultColor = _mat.color;
	}

	void Start () {
		currentZone = Zone.N;
		rb = gameObject.GetComponent<Rigidbody>();
		kickAngle = Mathf.Deg2Rad * kickAngle;
	}

	void FixedUpdate(){
		if(rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}

		if(kicked)
			_mat.SetColor("_Color", Color.cyan);
		if (trapped)
			_mat.SetFloat ("_Metallic", 1f);
		else
			_mat.SetFloat ("_Metallic", 0);
		if (bouncy)
			_mat.SetFloat ("_Glossiness", 0);
		else
			_mat.SetFloat ("_Glossiness", 0.5f);
	}

	public IEnumerator switchKickOn() {
		yield return new WaitForSeconds(stunDelay);
		this.kicked = true;
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Wall" && col.gameObject.name == "Ramp")
		{
			Vector3 myCollisionNormal = col.contacts[0].normal;
			myCollisionNormal = myCollisionNormal*bouncePower;
			myCollisionNormal.y = 0;
			rb.AddForce(myCollisionNormal, ForceMode.Impulse);
		}
		
		if (col.gameObject.tag == "Player") {
			Player p = col.gameObject.GetComponent<Player>();

			if(p.dashing){
				Vector3 kickVector = new Vector3(p.transform.forward.x * kickPower, Mathf.Cos(kickAngle) * 50, p.transform.forward.z * kickPower);
				rb.AddForce(kickVector, ForceMode.VelocityChange);
				StartCoroutine("switchKickOn");
			}
			else if(this.kicked && !p.dashing)
				p.paralyzed = true;
		}

		if(col.gameObject.name == "Field" && kicked)
		{
			_mat.SetColor("_Color", defaultColor);
			kicked = false;
		}
	}

	void OnTriggerEnter(Collider col){
		switch(col.gameObject.name){
			case "Red Zone": currentZone = Zone.R; break;
			case "Green Zone": currentZone = Zone.G; break;
			case "Blue Zone": currentZone = Zone.B; break;
			case "Yellow Zone": currentZone = Zone.Y; break;
		}
	}

	void OnTriggerExit(Collider col){
		currentZone = Zone.N;
	}
	
}
