﻿using UnityEngine;
using System.Collections;

public class Totem : MonoBehaviour {

	public GameObject heads;
    public GameObject gauge;
    public GameObject[] notches;

	public static string headAxolotl = "Totem_Head_Axolotl";
	public static string headFennec = "Totem_Head_Fennec";
	public static string headLeopard = "Totem_Head_Leopard";
	public static string headLoutre = "Totem_Head_Loutre";

    private Vector3 gaugeEndPos;
    private Vector3 newPos;
    private Vector3 rotationVector;
    private float rotationSpeed;

    private float unit;
    private float gaugeLevel;
    private float gaugeThreshold;

	void Start () {
        rotationSpeed = 2.5f;
        rotationVector = new Vector3(0.0f, 0.0f, rotationSpeed);

        gaugeLevel = 0.0f;
        newPos = Vector3.zero;
        gaugeEndPos = Vector3.zero;

        gaugeThreshold = gauge.transform.localPosition.y / notches.Length;
        if (gaugeThreshold < 0) gaugeThreshold *= -1f;

        unit = gauge.transform.localPosition.y / GameController.victoryScore * GameController.scoringRate;
        if (unit < 0) unit *= -1f;
    }

    public void fill()
    {
        if (gauge.transform.localPosition != gaugeEndPos)
        {
            newPos = gauge.transform.localPosition;
            newPos.y += unit * GameController.scoringDirection;
            gaugeLevel += unit * GameController.scoringDirection;
            gauge.transform.localPosition = newPos;
        }

        if (gaugeLevel > 0) RotateNotch(0);
        if (gaugeLevel > gaugeThreshold) RotateNotch(1);
        if (gaugeLevel > gaugeThreshold * 2) RotateNotch(2);
        if (gaugeLevel > gaugeThreshold * 3) RotateNotch(3);
        if (gaugeLevel > gaugeThreshold * 4) RotateNotch(4);
    }

    private void RotateNotch(int index)
    {
        notches[index].transform.Rotate(rotationVector);
    }

	public void setHead(string headName)
	{
		GameObject[] head = heads.GetComponentsInChildren<GameObject>();
		for(int i = 0; i < head.Length; i++)
		{
			if(head[i].name == headName) head[i].SetActive(true);
			else head[i].SetActive(false);
		}
	}
}
