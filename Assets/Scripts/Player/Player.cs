using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public GameObject ball;

	private Behaviour halo;

	public enum DashDir {Left=0, Right, NA};
	public float dashAttackSpeed;
	public float dashAttackStoppingSpeed;
	public float currentDashAttackTime;
	public float dashAttackTime;
	public Vector3[] dashDirections;

	public float paralyzedTime;
	public float currentParalyzedTime;
	public float paralyzedTimeSpeed;

	public float dashAttackCooldown;
	public float currentDashAttackCooldown;
	//private float dashAttackCooldownSpeed = 0.0166f;
	public bool dashAttackAllowed = true;
	
	public float moveSpeed;
	private Vector3 forwardVector;
	private float currentPosition;

	public float dashCooldown;
	public float currentDashCooldown;
	private float dashCooldownSpeed = 0.0166f;
	public bool dashAllowed = true;
	public Text dashUI;

	public bool hasItem;
	public Text itemUI;
	public float itemDuration;
	public float currentItemDuration;
	private float itemDurationSpeed = 0.0166f;

	public float dashTime = 1.0f;
	public float dashSpeed = 5.0f;
	public float dashStoppingSpeed = 0.1f;
	public float currentDashTime = 1.0f;

	public bool dashing = false;
	public bool attacking = false;
	public bool paralyzed = false;

	private Material _mat;
	//private Renderer renderer;
	private Color defaultColor;

	public float gravityPull;

	void Awake(){
		_mat = GetComponent<Renderer>().material;
		//renderer = GetComponent<Renderer> ();
		defaultColor = this.GetComponent<Renderer>().material.color;
		dashDirections = new Vector3[2];
		dashDirections [(int) DashDir.Left] = new Vector3 (-1, 0, 0);
		dashDirections [(int) DashDir.Right] = new Vector3 (1, 0, 0);
	}

	private void Start()
	{
		itemUI.enabled = false;
		ball = GameObject.Find ("Ball");
		halo = (Behaviour) GetComponent ("Halo");
	}

	void Update(){
	}

	void FixedUpdate(){

		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -27.0F, 22.0F), transform.position.y, Mathf.Clamp(transform.position.z, -20.7F, 13.25F));


		if (paralyzed && currentParalyzedTime < paralyzedTime) {
			_mat.SetColor("_Color", Color.gray);
			//renderer.material.SetColor("_Color", Color.gray);
			RotateParalyzed ();
			currentParalyzedTime += paralyzedTimeSpeed;
		} else {
			paralyzed = false;
			currentParalyzedTime = 0;
			_mat.SetColor("_Color", defaultColor);
			//renderer.material.SetColor("_Color", defaultColor);
		}

		if (dashing == false && currentDashCooldown < dashCooldown) {
			currentDashCooldown += dashCooldownSpeed;
			dashUI.text = currentDashCooldown.ToString("0.00") + " / "+ dashCooldown;
		}
		else if (currentDashCooldown >= dashCooldown) {
			dashAllowed = true;
			dashUI.text = "DASH";
		}

		if (hasItem && itemDuration > currentItemDuration) {
			itemUI.enabled = true;
			halo.enabled = true;
			currentItemDuration += itemDurationSpeed;
			itemUI.text = "ITEM (" + currentItemDuration.ToString("0.00") + " / " + itemDuration + ")";
		} else {
			hasItem = false;
			itemUI.text = "ITEM";
			itemUI.enabled = false;
			halo.enabled = false;
			currentItemDuration = 0;
			itemDuration = 0;
		}

		if (hasItem) {
			AttractBall();
		}
	}

	void RotateParalyzed(){
		transform.RotateAround(transform.position, transform.up, 3.0f);
	}

	void AttractBall(){
		ball.transform.LookAt (transform.position);
		ball.GetComponent<Rigidbody>().AddForce(ball.transform.TransformDirection(Vector3.forward).normalized*gravityPull, ForceMode.Acceleration);
	}

	public void Move(Vector3 moveDirection)
	{
		transform.Translate(moveDirection*moveSpeed, Space.World);
	}

	public void Rotate(Vector3 rotateDirection){
		transform.rotation = Quaternion.LookRotation (rotateDirection, Vector3.up);
	}

	public void DashAttack(DashDir direction){
		transform.Translate(dashDirections[(int) direction]*dashAttackSpeed);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "Player") {
			Player p = col.gameObject.GetComponent<Player>();
			
			if(p.attacking)
				paralyzed = true;
		}

		if (col.gameObject.name == "Ball" && dashing)
			currentDashTime = dashTime;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "PickUp") {
			PickUp p = col.gameObject.GetComponent<PickUp>();
			if(!hasItem){
				hasItem = true;
				itemDuration = p.duration;
				Destroy (p.gameObject);	
			}
		}
	}

}
