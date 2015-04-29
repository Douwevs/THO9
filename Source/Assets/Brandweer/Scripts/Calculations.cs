using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Calculations : MonoBehaviour {

	public int diameter150 = 150;
	public float wrijvingfactor150 = 0.012f;
	public int diameter75 = 75;
	public float wrijvingfactor75 = 0.021f;
	public double drukWater1 = 0;
	public double drukWater2 = 0;
	public float waterLevering1 = 0;
	public float waterLevering2 = 0;
	public int dompelDruk = 0;
	public float varkenDruk = 0;
	public float autoDrukIn = 0;
	public int diameter1 = 0;
	public int diameter2 = 0;
	public int autoDruk = 0;
	public int DompelVarkenLengte = 0;
	public int VarkenAutoLengte = 0;
	public int AutoSpuitLengte = 0;
	public double gok1 = 8;
	public double gok2 = 8;
	public Boolean [] Spuitkloppend = new Boolean [2];

	bool recalculate = false;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < Spuitkloppend.Length; i++) {
			Spuitkloppend[i] = false;
		}
		drukWater1 = gok1;
		drukWater2 = gok2;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Return)) {
			recalculate = true;
		}
		if (Input.GetKey(KeyCode.Escape)) { 
			Application.Quit(); 
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
		waterLevering1 = (float)(2f / 3f * Math.Pow (diameter1, 2) * Math.Sqrt (drukWater1));
		waterLevering2 = (float)(2f / 3f * Math.Pow (diameter2, 2) * Math.Sqrt (drukWater2));
		GameObject.Find ("waterLevering1").GetComponent<Text>().text = waterLevering1 + " L/m waterlevering";
		GameObject.Find ("waterLevering2").GetComponent<Text>().text = waterLevering2 + " L/m waterlevering";

	}

	void calcAuto() {
		GameObject.Find ("AutoDrukUit").GetComponent<Text>().text = autoDrukIn + autoDruk + " bar uitgaande druk";
	}

	void calcVarken() {
		float druk = (float)(2250 * wrijvingfactor75 * VarkenAutoLengte * (Math.Pow(waterLevering1 + waterLevering2, 2) / (4 * Math.Pow(diameter75, 5))));
		GameObject.Find ("VarkenDrukVerlies").GetComponent<Text>().text = druk + " bar verlies";
		GameObject.Find ("AutoDrukIn").GetComponent<Text>().text = (autoDrukIn = varkenDruk - druk) + " bar binnenkomende druk";
	}

	void calcDompel() {
		float druk = (float)(2250 * wrijvingfactor150 * DompelVarkenLengte * (Math.Pow(waterLevering1 + waterLevering2, 2) / (Math.Pow(diameter150, 5))));
		GameObject.Find ("DompelDrukVerlies").GetComponent<Text>().text = druk + " bar drukverlies";
		GameObject.Find ("VarkenDruk").GetComponent<Text>().text = (varkenDruk = dompelDruk - druk) + " bar druk";
	}

	void calcDrukWater() {
		dompelDruk = int.Parse((GameObject.Find("DompelDruk").GetComponent<InputField>()).text);
		diameter1 = int.Parse((GameObject.Find("Diameter1").GetComponent<InputField>()).text);
		diameter2 = int.Parse((GameObject.Find("Diameter2").GetComponent<InputField>()).text);
		autoDruk = int.Parse((GameObject.Find("AutoDruk").GetComponent<InputField>()).text);
		DompelVarkenLengte = int.Parse((GameObject.Find("DompelVarkenLengte").GetComponent<InputField>()).text);
		VarkenAutoLengte = int.Parse((GameObject.Find("VarkenAutoLengte").GetComponent<InputField>()).text);
		AutoSpuitLengte = int.Parse((GameObject.Find("AutoSpuitLengte").GetComponent<InputField>()).text);
		/*
		drukWater = (dompelDruk + autoDruk) / ( (1000 * wrijvingfactor150 * DompelVarkenLengte * Math.Pow(diameter1 , 4)) / Math.Pow(diameter150,5)
		  	        + ( ( ( 1000 * wrijvingfactor75 * VarkenAutoLengte * Math.Pow(diameter1, 4) ) / (4 * Math.Pow(diameter75, 5)) )
		   			+ ( ( 1000 * wrijvingfactor75 * AutoSpuitLengte * Math.Pow(diameter1, 4) ) / ( 4 * Math.Pow(diameter75, 5)))  + 1 ) );
		*/
		gok1 = drukWater1 = 8;
		gok2 = drukWater2 = 8;
		Spuitkloppend [0] = false;
		Spuitkloppend [1] = false;
		while ((!Spuitkloppend[0] || !Spuitkloppend[1]) && !double.IsNaN(drukWater1)) {
			if (!Spuitkloppend [0]) {
				drukWater1 = calcDrukWaterGok1 (gok1);
				if (gok1 < drukWater1 - 0.001f || gok1 > drukWater1 + 0.001f) {
					gok1 += (drukWater1 - gok1) / 4;
				} else
					Spuitkloppend [0] = true;
			} else if (!Spuitkloppend [1]) {
				Spuitkloppend [0] = false;
				drukWater2 = calcDrukWaterGok2 (gok2);
				if (gok2 < drukWater2 - 0.001f || gok2 > drukWater2 + 0.001f) {
					gok2 += (drukWater2 - gok2) / 4;
				} else 
					Spuitkloppend [1] = true;
			}
		}

		GameObject.Find ("drukWater1").GetComponent<Text>().text = drukWater1 + " bar waterdruk";
		GameObject.Find ("drukWater2").GetComponent<Text>().text = drukWater2 + " bar waterdruk";
	}
	double calcDrukWaterGok1(double gok) {
		double gokDrukWater = dompelDruk + autoDruk 
			- 2250 * wrijvingfactor150 * DompelVarkenLengte * 
				Math.Pow ((2f/3f * diameter1 * diameter1 * Math.Sqrt(gok) + 
				           2f/3f * diameter2 * diameter2 * Math.Sqrt(drukWater2)), 2) / Math.Pow (diameter150, 5)
			- 2250 * wrijvingfactor75 * VarkenAutoLengte * 
				Math.Pow ((2f/3f * diameter1 * diameter1 * Math.Sqrt(gok) + 
				           2f/3f * diameter2 * diameter2 * Math.Sqrt(drukWater2)), 2) / (4 * Math.Pow (diameter75, 5))
			- 2250 * wrijvingfactor75 * AutoSpuitLengte * 
				Math.Pow ((2f/3f * diameter1 * diameter1 * Math.Sqrt(gok) + 
				           2f/3f * diameter2 * diameter2 * Math.Sqrt(drukWater2)), 2) / (4 * Math.Pow (diameter75, 5))
			- 2250 * wrijvingfactor75 * AutoSpuitLengte * 
				Math.Pow ((2f/3f * diameter1 * diameter1 * Math.Sqrt(gok)), 2) / Math.Pow (diameter75, 5);
		return gokDrukWater;
	}
	double calcDrukWaterGok2(double gok) {
		double gokDrukWater = dompelDruk + autoDruk 
			- 2250 * wrijvingfactor150 * DompelVarkenLengte * 
				Math.Pow ((2f/3f * diameter1 * diameter1 * Math.Sqrt(gok) + 
				           2f/3f * diameter2 * diameter2 * Math.Sqrt(drukWater1)), 2) / Math.Pow (diameter150, 5)
				- 2250 * wrijvingfactor75 * VarkenAutoLengte * 
				Math.Pow ((2f/3f * diameter1 * diameter1 * Math.Sqrt(gok) + 
				           2f/3f * diameter2 * diameter2 * Math.Sqrt(drukWater1)), 2) / (4 * Math.Pow (diameter75, 5))
				- 2250 * wrijvingfactor75 * AutoSpuitLengte * 
				Math.Pow ((2f/3f * diameter1 * diameter1 * Math.Sqrt(gok) + 
				           2f/3f * diameter2 * diameter2 * Math.Sqrt(drukWater1)), 2) / (4 * Math.Pow (diameter75, 5))
				- 2250 * wrijvingfactor75 * AutoSpuitLengte * 
				Math.Pow ((2f/3f * diameter1 * diameter2 * Math.Sqrt(gok)), 2) / Math.Pow (diameter75, 5);
		return gokDrukWater;
	}
}