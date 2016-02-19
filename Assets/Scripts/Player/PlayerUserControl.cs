using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerUserControl : MonoBehaviour
{
	private Player player;
	public int playerNumber;

	private Vector3 previousMove = Vector3.zero;
	private Vector3 move;

	private bool dash;
	private bool dashLeft;
	private bool dashRight;

    private float sideDashTimer = 0.0f;
    private float sideDashTimeOut = 0.1f;
    private Vector3 sideDashDirection;

	private bool gamepadAvailable;
	private PowerUp _powerUp;
	
	private void Awake()
	{
		player = GetComponent<Player>();
		_powerUp = GetComponent<PowerUp>();
        move = Vector3.zero;
	}
	
	private void Update()
	{
		gamepadAvailable = GamepadInput.Instance.gamepads.Count >= playerNumber ? true : false;

		if(gamepadAvailable && GameController.matchHasStarted){
			float h = GamepadInput.Instance.gamepads [playerNumber - 1].GetAxis (GamepadAxis.LeftStickX);
			float v = GamepadInput.Instance.gamepads [playerNumber - 1].GetAxis (GamepadAxis.LeftStickY);
            move = (v * Vector3.forward + h * Vector3.right).normalized;
            player.anim.SetFloat("Speed", move.magnitude);

            dash = GamepadInput.Instance.gamepads[playerNumber - 1].GetButtonDown(GamepadButton.Action1);
            dashLeft = GamepadInput.Instance.gamepads[playerNumber - 1].GetAxis(GamepadAxis.LeftTrigger) > 0.5;
            dashRight = GamepadInput.Instance.gamepads[playerNumber - 1].GetAxis(GamepadAxis.RightTrigger) > 0.5;

			player.anim.SetBool("DashForward", dash && player.dashAllowed);
			player.anim.SetBool("DashRight", dashRight && !dashLeft && player.attacking);
			player.anim.SetBool("DashLeft", dashLeft && !dashRight && player.attacking);

			if(GamepadInput.Instance.gamepads[playerNumber-1].GetButtonDown(GamepadButton.Action2) && _powerUp.available){
                player.PlaySound(player.audioClips[3], false);
				_powerUp.timestamp = Time.time;
				_powerUp.activated = true;
				_powerUp.available = false;
				_powerUp.powerUI.color = Color.clear;
			}
		}
	}
		
	private void FixedUpdate(){

		if (!player.paralyzed && !player.repulsed) {

			if (move != previousMove && move != Vector3.zero)
				player.Rotate (move);
          
			if (dash && player.dashAllowed){
				player.currentDashTime = 0.0f;
				player.currentDashCooldown = 0.0f;
				player.dashAllowed = false;
				player.dashing = true;
			}
           
			if (player.currentDashTime < player.dashTime) {
                player.Dash(move);
                player.currentDashTime += player.dashStoppingSpeed;
			} 
			else{
                player.Move(move);
                player.dashing = false;
            }
            dash = false;


            if (dashLeft)
                sideDashDirection = -player.transform.right;
            else if (dashRight)
                sideDashDirection = player.transform.right;
            else
                sideDashDirection = Vector3.zero;

            if (dashLeft || dashRight) {
                if (player.sideDashingAllowed && player.sideDashCount > 0) {
                    sideDashTimer += Time.deltaTime;
                    if (sideDashTimer < sideDashTimeOut) {
                        player.DashAttack(sideDashDirection);
                        player.attacking = true;
                    }
                    else {
                        sideDashTimer = 0.0f;
                        player.sideDashingAllowed = false;
                        player.removeSideDashStack(1);
                    }
                }
            }
            else {
                player.sideDashingAllowed = true;
                player.attacking = false;
            }
        }
	}
}