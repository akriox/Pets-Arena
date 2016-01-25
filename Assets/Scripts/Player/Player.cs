using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;

	public float paralyzedTime;
	private float currentParalyzedTime;

	public float moveSpeed;
	private float currentPosition;

	public float dashCooldown;
	public float currentDashCooldown;
	private float dashCooldownSpeed = 0.0166f;
    public bool dashAllowed = true;
	public Text dashUI;

    public bool dashing = false;
    public float dashTime = 1.0f;
	public float dashSpeed = 5.0f;
	public float dashStoppingSpeed = 0.1f;
	public float currentDashTime = 1.0f;

    public bool sideDashing;
    private float sideDashCooldown = 3.0f;
    private float sideDashRefresh = 0.0f;

    public int sideDashCount { get; private set; }
    private int maxSideDashStack = 4;

	public bool attacking = false;
	public bool paralyzed = false;
	public bool immune = false;

    public bool repulsed = false;
    private float repulsedDuration = 0.8f;
    private float repulsedTimer = 0.0f;

	private Material _mat;
	private Color defaultColor;

    public Animator anim { get; private set; }

	void Awake() {
        _rb = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();

        _mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        defaultColor = _mat.color;

        sideDashCount = maxSideDashStack;
    }

    void Update() {

        sideDashRefresh += Time.deltaTime;
        if (sideDashRefresh >= sideDashCooldown){
            addSideDashStack(1);
            sideDashRefresh = 0.0f;
        }

        if (repulsed){
            repulsedTimer += Time.deltaTime;
            if (repulsedTimer > repulsedDuration){
                repulsed = false;
                repulsedTimer = 0.0f;
            }
        }

        if (paralyzed && currentParalyzedTime < paralyzedTime && !immune)
        {
			_mat.SetColor("_Color", Color.gray);
            RotateParalyzed();
            currentParalyzedTime += Time.deltaTime;
        }
        else {
            paralyzed = false;
            currentParalyzedTime = 0;
			_mat.SetColor("_Color", defaultColor);
        }

        if (dashing == false && currentDashCooldown < dashCooldown)
        {
            currentDashCooldown += dashCooldownSpeed;
            dashUI.text = currentDashCooldown.ToString("0.00") + " / " + dashCooldown;
        }
        else if (currentDashCooldown >= dashCooldown)
        {
            dashAllowed = true;
            dashUI.text = "DASH";
        }
    }

	void RotateParalyzed(){
		transform.RotateAround(transform.position, transform.up, 3.0f);
	}

	public void Rotate(Vector3 rotateDirection){
		transform.rotation = Quaternion.LookRotation (rotateDirection, Vector3.up);
	}

    public void Move(Vector3 moveDirection){
        _rb.velocity = new Vector3(moveDirection.x * moveSpeed, _rb.velocity.y, moveDirection.z * moveSpeed);
    }

    public void Dash(Vector3 moveDirection){
        _rb.velocity = new Vector3(moveDirection.x * dashSpeed, _rb.velocity.y, moveDirection.z * dashSpeed);
    }

	public void DashAttack(Vector3 dir){
        _rb.velocity = dir * dashSpeed;
	}

    public void removeSideDashStack(int i)
    {
        sideDashCount -= i;
        if (sideDashCount <= 0)
            sideDashCount = 0;
    }

    public void addSideDashStack(int i)
    {
        sideDashCount += i;
        if (sideDashCount > maxSideDashStack)
            sideDashCount = maxSideDashStack;
    }

    public IEnumerator ignoreObstacles(Collision col)
    {
        Physics.IgnoreCollision(this.GetComponent<Collider>(), col.collider, true);
        yield return new WaitForSeconds(0.5f);
        Physics.IgnoreCollision(this.GetComponent<Collider>(), col.collider, false);
    }

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player") {
			Player p = col.gameObject.GetComponent<Player>();
			
			if(p.attacking)
				paralyzed = true;
		}

        if(col.gameObject.tag == "Obstacle" && dashing)
        {
            StartCoroutine(ignoreObstacles(col));
        } 

        if (col.gameObject.tag == "Ball" && dashing)
			currentDashTime = dashTime;
	}
}
