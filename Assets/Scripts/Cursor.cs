using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cursor : MonoBehaviour {

    private Image cursorSprite;
    private Vector2 position;
    private float speed = 5.0f;

	void Start () {
        cursorSprite = GetComponent<Image>();
	}

	void Update () {

        float x = GamepadInput.Instance.gamepads[0].GetAxis(GamepadAxis.LeftStickX);
        float y = GamepadInput.Instance.gamepads[0].GetAxis(GamepadAxis.LeftStickY);

        position = cursorSprite.rectTransform.anchoredPosition;
        cursorSprite.rectTransform.anchoredPosition = new Vector2(position.x + x * speed, position.y + y * speed);
    }
}
