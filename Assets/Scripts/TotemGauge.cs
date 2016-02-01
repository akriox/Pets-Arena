using UnityEngine;
using System.Collections;

public class TotemGauge : MonoBehaviour {

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 newPos;
    private Color color;
    private float unit;

	void Start () {
        newPos = Vector3.zero;
        startPosition = transform.localPosition;
        endPosition = Vector3.zero;
        unit = startPosition.y / GameController.victoryScore * GameController.scoringRate;
        if (unit < 0) unit *= -1f;
    }
	
    public void fill()
    {
        if (transform.localPosition != endPosition)
        {
            newPos = transform.localPosition;
            newPos.y += unit * GameController.scoringDirection;
            transform.localPosition = newPos;
        }
    }

    public void setColor(Color c)
    {
        this.color = c;
    }
}
