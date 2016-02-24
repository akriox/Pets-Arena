using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageBlink : MonoBehaviour{

	private Image img;
    private float blinkTimer = 0.0f;

    public void Start(){
		img = GetComponent<Image>();
        blinkTimer = Time.time;
    }

    public void Update(){
        if (Time.time - blinkTimer > 0.5f){
            var color = img.color;
            color.a = color.a == 0 ? 1 : 0;
            img.color = color;
            blinkTimer = Time.time;
        }
    }
}
