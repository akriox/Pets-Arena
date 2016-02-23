using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBlink : MonoBehaviour{

    private Text text;
    private float blinkTimer = 0.0f;

    public void Start(){
        text = GetComponent<Text>();
        blinkTimer = Time.time;
    }

    public void Update(){
        if (Time.time - blinkTimer > 0.5f){
            var color = text.color;
            color.a = color.a == 0 ? 1 : 0;
            text.color = color;
            blinkTimer = Time.time;
        }
    }
}
