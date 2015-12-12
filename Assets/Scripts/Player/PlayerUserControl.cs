using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerUserControl : MonoBehaviour
{
	public Player player;
	public int playerNumber;

	private Vector3 previousMove = Vector3.zero;
	public Vector3 move;

	private bool dash;
	private bool dashLeft;
	private bool dashRight;
	private bool triggerDown = false;
	private Player.DashDir dir;

	private bool gamepadAvailable;
	private PowerUp _powerUp;
	
	private void Awake()
	{
		player = GetComponent<Player>();
		_powerUp = GetComponent<PowerUp>();
	}
	
	private void Update()
	{
		gamepadAvailable = GamepadInput.Instance.gamepads.Count >= playerNumber ? true : false;

		if(gamepadAvailable){
			float h = GamepadInput.Instance.gamepads [playerNumber - 1].GetAxis (GamepadAxis.LeftStickX);
			float v = GamepadInput.Instance.gamepads [playerNumber - 1].GetAxis (GamepadAxis.LeftStickY);

			move = (v*Vector3.forward + h*Vector3.right).normalized;

			if(GamepadInput.Instance.gamepads[playerNumber-1].GetButtonDown(GamepadButton.Action2) && _powerUp.available){
				_powerUp.timestamp = Time.time;
				_powerUp.activated = true;
				_powerUp.available = false;
				_powerUp.powerUI.text = "";
			}
		}
	}


	private void FixedUpdate()
	{

		if (!player.paralyzed && gamepadAvailable) {

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
			} 
			else{
				player.dashing = false;
			}
			
			player.Move(move);
			dash = false;

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
			} 
			else {
				player.attacking = false;
				dashLeft = false;
				dashRight = false;
				dir = Player.DashDir.NA; 
			}
		}
	}
}