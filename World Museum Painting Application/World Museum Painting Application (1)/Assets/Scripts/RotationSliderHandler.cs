using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSliderHandler : MonoBehaviour {

	MoveableObject m_sliderObj; 
	MoveableObject m_cupObj; 

	// Use this for initialization
	public void Start () {
		
	}

	public void RotationSlider (MoveableObject currentObj, MoveableObject prevObj){
		m_sliderObj = currentObj;
		m_cupObj = prevObj;

	}
	
	// Update is called once per frame
	void Update () {

		//Moveable cups change rotation based on location of slider button.
		//Slider button resets back to 0,0,0 when selecting different moveable cup. 

		/*
		 * So, base location of the slider is 230z and the total difference between maximum location and minimum locations is 50z 
		 * Meaning, we have a 25/-25 deviation from 230.
		 * We want the cups to rotate 180 is either direction. 
		 * If we move the slider in one direction, we want the cup to rotate in one direction to a maximum of 180 degrees. 
		 * Thus, 25z slider deviation = 180 degree cup rotation.
		 * 
		 * The Least Common Multiple of 25 and 180 is 900 
		 * 900/25 = 36	-deviation
		 * 900/180 = 5	-degree
		 * 36/5 = 7.2	
		*/

		if (m_sliderObj != null) {
			float zDifference = m_sliderObj.gameObject.transform.position.z - 205; //The slider moves between 205 and 255, so if we take the current value, subtracted by 205, we know the deviation of the slider. 
			if (zDifference <= 25){ //if the slider's deviation is less than 25, we know the user has slid the slider upwards.

				float deviationLCM = 36; 
				float degreeLCM = 5;
				float holder; 
				float xRotation = 0;
				holder = zDifference * deviationLCM; //This brings the deviation to a point where we can compare the degrees. 
				xRotation = holder / degreeLCM; //We divide by the degreeLCM to find the degrees in reference of the z deviation by using the least common multiple (900);

				float sliderRotationX = m_sliderObj.gameObject.transform.eulerAngles.x; 
				float staticRotationY = m_sliderObj.gameObject.transform.eulerAngles.y;
				float staticRotationZ = m_sliderObj.gameObject.transform.eulerAngles.z;

				sliderRotationX = xRotation;

				m_cupObj.gameObject.transform.eulerAngles = new Vector3 (sliderRotationX, staticRotationY, staticRotationZ);
				Debug.Log ("Slider Rotation: " + sliderRotationX);

			}else if (zDifference > 25){ //if the slider's deviation is greater than 25, we know the user has slid the slider downwards. 
				//We need to flip the calculation ot make the cup rotate the other direction. 
			}
		}
	}
}