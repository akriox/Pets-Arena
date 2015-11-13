using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Rigidbody rb;

	public float bouncePower;

	public float maxSpeed;

	public float kickAngle;
	public float kickPower;

	public bool kicked = false;
	public float stunDelay;
	public enum Zone {R, G, B, Y, N};
	public Zone currentZone;

	private Renderer renderer;
	private Color defaultColor;

	void Awake(){
		renderer = GetComponent<Renderer> ();
		defaultColor = this.renderer.material.color;
	}

	// Use this for initialization
	void Start () {
		currentZone = Zone.N;
		rb = gameObject.GetComponent<Rigidbody>();
		kickAngle = Mathf.Deg2Rad * kickAngle;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if(rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}

		if(kicked)
			renderer.material.SetColor("_Color", Color.cyan);

	}

	public IEnumerator switchKickOn() {
		yield return new WaitForSeconds(stunDelay);
		this.kicked = true;  
		print ("test");
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Wall")
		{
			Vector3 myCollisionNormal = col.contacts[0].normal;
			myCollisionNormal = myCollisionNormal*bouncePower;
			myCollisionNormal.y = 0;
			rb.AddForce(myCollisionNormal, ForceMode.Impulse);
		}



		if (col.gameObject.name == "Player") {
			Player p = col.gameObject.GetComponent<Player>();

			if(p.dashing){
				Vector3 kickVector = new Vector3(p.transform.forward.x * kickPower, Mathf.Cos(kickAngle) * 100, p.transform.forward.z * kickPower);
				print ("Kicked with : "+ kickVector);
				rb.AddForce(kickVector, ForceMode.Impulse);
				StartCoroutine("switchKickOn");
			}
			else if(this.kicked)
				p.paralyzed = true;
		}

		if(col.gameObject.name == "Field" && kicked)
		{
			renderer.material.SetColor("_Color", defaultColor);
			kicked = false;
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "Red Zone")
			currentZone = Zone.R;
		else if (col.gameObject.name == "Green Zone")
			currentZone = Zone.G;
		else if (col.gameObject.name == "Blue Zone")
			currentZone = Zone.B;
		else if (col.gameObject.name == "Yellow Zone")
			currentZone = Zone.Y;
	}
	
}
