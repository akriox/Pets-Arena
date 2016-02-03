using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	private Rigidbody rb;

	public float bouncePower;

	public float maxSpeed;

	public float kickAngle;
	public float kickPower;

	public bool trapped = false;
	public bool bouncy = false;
	public float stunDelay;
	public enum Zone {R, G, B, Y, N};
	public Zone currentZone;

	private Material defaultMat;
    public Material ballTrappedMat;

    public TrailRenderer trail { get; set; }
    private Behaviour halo;

	void Awake(){
		defaultMat = GetComponent<Renderer>().material;
        trail = GetComponent<TrailRenderer>();
        trail.enabled = true;
        halo = (Behaviour)GetComponent("Halo");
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

        if (trapped){
            GetComponent<Renderer>().material = ballTrappedMat;
            halo.enabled = true;
        }
        else {
            GetComponent<Renderer>().material = defaultMat;
            halo.enabled = false;
        }

        /*
		if (bouncy){

        }
		else{

        }
        */
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
			}
		}
	}

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Player p = col.gameObject.GetComponent<Player>();
            if (p.dashing)
            {
                Vector3 kickVector = new Vector3(p.transform.forward.x * kickPower, Mathf.Cos(kickAngle) * 50, p.transform.forward.z * kickPower);
                rb.AddForce(kickVector, ForceMode.VelocityChange);
            }
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
