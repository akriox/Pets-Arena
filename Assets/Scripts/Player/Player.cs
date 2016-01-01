using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
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
	public bool dashAttackAllowed = true;
	
	public float moveSpeed;
	private Vector3 forwardVector;
	private float currentPosition;

	public float dashCooldown;
	public float currentDashCooldown;
	private float dashCooldownSpeed = 0.0166f;
	public bool dashAllowed = true;
	public Text dashUI;

	public float dashTime = 1.0f;
	public float dashSpeed = 5.0f;
	public float dashStoppingSpeed = 0.1f;
	public float currentDashTime = 1.0f;

	public bool dashing = false;
	public bool attacking = false;
	public bool paralyzed = false;
	public bool immune = false;

	private Material _mat;
	private Color defaultColor;

	void Awake(){
		_mat = GetComponent<Renderer>().material;
		defaultColor = this.GetComponent<Renderer>().material.color;
		dashDirections = new Vector3[2];
		dashDirections [(int) DashDir.Left] = new Vector3 (-1, 0, 0);
		dashDirections [(int) DashDir.Right] = new Vector3 (1, 0, 0);
	}

	void FixedUpdate(){

		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -30.0F, 29.3F), transform.position.y, Mathf.Clamp(transform.position.z, -22.95F, 20.86F));
		
		if (paralyzed && currentParalyzedTime < paralyzedTime && !immune) {
			_mat.SetColor("_Color", Color.gray);
			RotateParalyzed ();
			currentParalyzedTime += paralyzedTimeSpeed;
		} else {
			paralyzed = false;
			currentParalyzedTime = 0;
			_mat.SetColor("_Color", defaultColor);
		}

		if (dashing == false && currentDashCooldown < dashCooldown) {
			currentDashCooldown += dashCooldownSpeed;
			dashUI.text = currentDashCooldown.ToString("0.00") + " / "+ dashCooldown;
		}
		else if (currentDashCooldown >= dashCooldown) {
			dashAllowed = true;
			dashUI.text = "DASH";
		}
	}

	void RotateParalyzed(){
		transform.RotateAround(transform.position, transform.up, 3.0f);
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
		if (col.gameObject.tag == "Player") {
			Player p = col.gameObject.GetComponent<Player>();
			
			if(p.attacking)
				paralyzed = true;
		}

		if (col.gameObject.tag == "Ball" && dashing)
			currentDashTime = dashTime;
	}
}
