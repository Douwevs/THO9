using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Calculations : MonoBehaviour {

	public int diameter150 = 150;
	public float wrijvingfactor150 = 0.012f;
	public int diameter75 = 75;
	public float wrijvingfactor75 = 0.021f;
	public double drukWater = 0;
	public float waterLevering = 0;
	public int dompelDruk = 0;
	public float varkenDruk = 0;
	public float autoDrukIn = 0;
	public int diameter = 0;
	public int autoDruk = 0;
	public int DompelVarkenLengte = 0;
	public int VarkenAutoLengte = 0;
	public int AutoSpuitLengte = 0;

	bool recalculate = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Return)) {
			recalculate = true;
		}
	}

	void FixedUpdate() {
		if (recalculate) {
			calcDrukWater();
			calcWaterLevering();
			calcDompel();
			calcVarken();
			calcAuto ();
			recalculate = false;
		}
	}

	void calcWaterLevering() {
		waterLevering = (float)(2f / 3f * Math.Pow (diameter, 2) * Math.Sqrt (drukWater));
		GameObject.Find ("waterLevering").GetComponent<Text>().text = waterLevering + " L/m waterlevering";

	}

	void calcAuto() {
		GameObject.Find ("AutoDrukUit").GetComponent<Text>().text = autoDrukIn + autoDruk + " bar uitgaande druk";
	}

	void calcVarken() {
		float druk = (float)(2250 * wrijvingfactor75 * VarkenAutoLengte * (Math.Pow(waterLevering, 2) / (4 * Math.Pow(diameter75, 5))));
		GameObject.Find ("VarkenDrukVerlies").GetComponent<Text>().text = druk + " bar verlies";
		GameObject.Find ("AutoDrukIn").GetComponent<Text>().text = (autoDrukIn = varkenDruk - druk) + " bar binnenkomende druk";
	}

	void calcDompel() {
		float druk = (float)(2250 * wrijvingfactor150 * DompelVarkenLengte * (Math.Pow(waterLevering, 2) / (Math.Pow(diameter150, 5))));
		GameObject.Find ("DompelDrukVerlies").GetComponent<Text>().text = druk + " bar drukverlies";
		GameObject.Find ("VarkenDruk").GetComponent<Text>().text = (varkenDruk = dompelDruk - druk) + " bar druk";
	}

	void calcDrukWater() {
		dompelDruk = int.Parse((GameObject.Find("DompelDruk").GetComponent<InputField>()).text);
		diameter = int.Parse((GameObject.Find("Diameter").GetComponent<InputField>()).text);
		autoDruk = int.Parse((GameObject.Find("AutoDruk").GetComponent<InputField>()).text);
		DompelVarkenLengte = int.Parse((GameObject.Find("DompelVarkenLengte").GetComponent<InputField>()).text);
		VarkenAutoLengte = int.Parse((GameObject.Find("VarkenAutoLengte").GetComponent<InputField>()).text);
		AutoSpuitLengte = int.Parse((GameObject.Find("AutoSpuitLengte").GetComponent<InputField>()).text);
		drukWater = (dompelDruk + autoDruk) / ( (1000 * wrijvingfactor150 * DompelVarkenLengte * Math.Pow(diameter , 4)) / Math.Pow(diameter150,5)
		  	        + ( ( ( 1000 * wrijvingfactor75 * VarkenAutoLengte * Math.Pow(diameter, 4) ) / (4 * Math.Pow(diameter75, 5)) )
		   			+ ( ( 1000 * wrijvingfactor75 * AutoSpuitLengte * Math.Pow(diameter, 4) ) / ( 4 * Math.Pow(diameter75, 5)))  + 1 ) );
		GameObject.Find ("drukWater").GetComponent<Text>().text = drukWater + " bar waterdruk";
	}
}