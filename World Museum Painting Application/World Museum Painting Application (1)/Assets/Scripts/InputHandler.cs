using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour 
{
	private Ray m_Ray;
	private RaycastHit m_RayCastHit;
	private MoveableObject m_CurrentMovableObject;
	private MoveableObject m_PreviousMovableObject;

	void Update () 
	{

		if (Input.touches.Length == 1) {
			Touch touchedFinger = Input.touches [0];

			switch (touchedFinger.phase) 
			{
			case TouchPhase.Began: 
			m_Ray = Camera.main.ScreenPointToRay (touchedFinger.position);
				if (Physics.Raycast (m_Ray.origin, m_Ray.direction, out m_RayCastHit, Mathf.Infinity)) {
					if (m_RayCastHit.collider.gameObject.GetComponent<MoveableObject> () != null) {
						MoveableObject movableObj = m_RayCastHit.collider.gameObject.GetComponent<MoveableObject> ();
						Debug.Log ("Moveable Object: " + movableObj.name);
						if (movableObj) {
							m_CurrentMovableObject = movableObj;

							//Controls Y-Axis movement for each cup. When first clicked on, each cup will move upwards, indicating they've been selected. When a different cup is selected, the previous cup will return to the base Y-Axis location.  
							if (m_CurrentMovableObject.CompareTag ("MoveableCup")) {

								if (m_CurrentMovableObject != m_PreviousMovableObject) {

									if (m_PreviousMovableObject != null) {
										
										float prevObjX = m_PreviousMovableObject.gameObject.transform.position.x;
										float prevObjY = m_PreviousMovableObject.gameObject.transform.position.y; 
										float prevObjZ = m_PreviousMovableObject.gameObject.transform.position.z;

										m_PreviousMovableObject .gameObject.transform.position = new Vector3 (prevObjX, prevObjY - 30, prevObjZ);
									}

									float objX = m_CurrentMovableObject.gameObject.transform.position.x;
									float objY = m_CurrentMovableObject.gameObject.transform.position.y; 
									float objZ = m_CurrentMovableObject.gameObject.transform.position.z;

									m_CurrentMovableObject.gameObject.transform.position = new Vector3 (objX, objY + 30, objZ);
								}
							}
						}
					}

					/*
					 * Obsolete Button code that controls the Z Transfer Button fucntionality.
					 * 
					if (m_RayCastHit.collider.gameObject.GetComponent<ZTransferButton> () != null) {
						GameObject button = m_RayCastHit.collider.gameObject.GetComponent<ZTransferButton> ().gameObject;
						float btnPositionZ = button.transform.position.z;

						float prevObjX = m_PreviousMovableObject.gameObject.transform.position.x;
						float prevObjY = m_PreviousMovableObject.gameObject.transform.position.y;

						Vector3 moveLocation = new Vector3 (prevObjX, prevObjY, btnPositionZ);

						if (m_PreviousMovableObject != null) {
							m_PreviousMovableObject.gameObject.transform.position = moveLocation;
						}
					}
					*/
				}
			break;
			case TouchPhase.Moved:
				if (m_CurrentMovableObject && m_CurrentMovableObject.CompareTag ("MoveableCup")) {
					m_CurrentMovableObject.gameObject.transform.Translate (Vector3.left * touchedFinger.deltaPosition.x / 3);
					m_CurrentMovableObject.gameObject.transform.Translate (Vector3.back * touchedFinger.deltaPosition.y / 3);

					if (Input.touches.Length == 2) {
						if (m_CurrentMovableObject.gameObject.GetComponent<Animator> () != null) {
							m_CurrentMovableObject.gameObject.GetComponent<Animator> ().Play ("CupTip");
							Debug.Log ("Double Touch Cup Tip Hit Inside");
						}
					}

				}
				/*
				 * Obsolete code that controls the tilt slider fucntionality.
				 * 
				if (m_CurrentMovableObject && m_CurrentMovableObject.CompareTag ("SliderBtn")) {
					Debug.Log ("Slider Button Z: " + m_CurrentMovableObject.gameObject.transform.position.z);

					float staticX = m_CurrentMovableObject.gameObject.transform.position.x;
					float staticY = m_CurrentMovableObject.gameObject.transform.position.y; 
					float dynamicZ = m_CurrentMovableObject.gameObject.transform.position.z;

					//Prevents slider from moving too far along z axis in world position.
					if (dynamicZ >= 205 && dynamicZ <= 255) {
						m_CurrentMovableObject.gameObject.transform.Translate (Vector3.back * touchedFinger.deltaPosition.y / 2);

						//Call the Rotation Slider Handler which rotates the cup based on the slider position. Sending the slider and the previously touched cup
						m_CurrentMovableObject.GetComponent<RotationSliderHandler> ().RotationSlider (m_CurrentMovableObject, m_PreviousMovableObject);

					}else if (dynamicZ < 205){
						m_CurrentMovableObject.gameObject.transform.position = new Vector3 (staticX, staticY, dynamicZ + 1);
					} else if (dynamicZ > 255){
						m_CurrentMovableObject.gameObject.transform.position = new Vector3 (staticX, staticY, dynamicZ - 1);
					}

				}
				*/

			break;
			case TouchPhase.Ended:
				if (m_CurrentMovableObject != null && m_CurrentMovableObject != m_PreviousMovableObject) {
					m_PreviousMovableObject = m_CurrentMovableObject;
				}
				m_CurrentMovableObject = null;
				break;
			default:
				break;
			}
		} 

		if (Input.touches.Length == 2 && m_PreviousMovableObject != null) {
			if (m_PreviousMovableObject.gameObject.GetComponent<Animator> () != null) {
				m_PreviousMovableObject.gameObject.GetComponent<Animator> ().Play ("CupTip");
				Debug.Log ("Previous MO @ 2 Touches: " + m_PreviousMovableObject);
				Debug.Log ("Double Touch Cup Tip Hit Outside");
			}
		}
	}



	public void SplliageTrigger (){
		//Hit when you want to see a new particle effect 'spill' contents of cup.
		Debug.Log ("Cup Should be Spilling");
	}
}