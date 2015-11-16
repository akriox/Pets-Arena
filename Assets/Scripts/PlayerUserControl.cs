using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerUserControl : MonoBehaviour
{
	public Player player;
	public int playerNumber;

	private Vector3 previousMove = Vector3.zero;
	public Vector3 move;

	public Transform cam;
	public Vector3 camForward;
	private bool dash;
	private bool dashLeft;
	private bool dashRight;
	private bool triggerDown = false;
	private Player.DashDir dir;
	
	private void Awake()
	{
		player = GetComponent<Player>();
	
		if (Camera.main != null)
		{
			cam = Camera.main.transform;
		}
		else
		{
			Debug.LogWarning(
				"Warning: no main camera found. Player needs a Camera tagged \"MainCamera\", for camera-relative controls.");
		}
	}
	
	
	private void Update()
	{

		float h = GamepadInput.Instance.gamepads [playerNumber - 1].GetAxis (GamepadAxis.LeftStickX);
		float v = GamepadInput.Instance.gamepads [playerNumber - 1].GetAxis (GamepadAxis.LeftStickY);

		if (cam != null)
		{
			camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
			move = (v*camForward + h*cam.right).normalized;
		}
		else
		{
			move = (v*Vector3.forward + h*Vector3.right).normalized;
		}
	}


	private void FixedUpdate()
	{

		if (!player.paralyzed) {
			dash = GamepadInput.Instance.gamepads[playerNumber-1].GetButtonDown(GamepadButton.Action1);
			
			
			if (move != previousMove && move != Vector3.zero)
				player.Rotate (move);
			
			if (dash && player.dashAllowed)
			{
				player.currentDashTime = 0.0f;
				player.currentDashCooldown = 0.0f;
				player.dashAllowed = false;
				player.dashing = true;
			}
			
			if (player.currentDashTime < player.dashTime) {
				move = new Vector3 (0, 0, player.dashSpeed);
				move = player.transform.TransformDirection (move);
				player.currentDashTime += player.dashStoppingSpeed;
			} else {
				player.dashing = false;
			}
			
			player.Move(move);
			dash = false;
			
			//////////////

			dashLeft = GamepadInput.Instance.gamepads[playerNumber-1].GetAxis(GamepadAxis.LeftTrigger) > 0.5;
			dashRight = GamepadInput.Instance.gamepads[playerNumber-1].GetAxis(GamepadAxis.RightTrigger) > 0.5;;

			if(!dashLeft && !dashRight){
				triggerDown = false;
			}
			
			if(dashLeft)
				dir = Player.DashDir.Left;
			else if(dashRight)
				dir = Player.DashDir.Right;
			
			if ((dashRight || dashLeft) && !triggerDown)
			{
				player.currentDashAttackTime = 0.0f;
				player.dashAttackAllowed = false;
				player.attacking = true;
				triggerDown = true;
			}
			
			if (player.currentDashAttackTime < player.dashAttackTime) {
				player.DashAttack(dir);
				player.currentDashAttackTime += player.dashAttackStoppingSpeed;
			} else {
				player.attacking = false;
				dashLeft = false;
				dashRight = false;
				dir = Player.DashDir.NA; 
			}

		}
	}
}
