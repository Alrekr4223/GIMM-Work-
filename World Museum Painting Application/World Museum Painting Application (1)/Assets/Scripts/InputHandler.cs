using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour 
{
	private Ray m_Ray;
	private RaycastHit m_RayCastHit;
	private MoveableObject m_CurrentMovableObject;
<<<<<<< HEAD
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
				}
				/*
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
=======

	public GameObject indicator;
	private bool indicatorSwitchisHit = false;
	private float timeToWait = 2f;

	private bool toMoveUp = false;
	private bool toMoveIn = false; 
	private bool toRotateIn = false;
	private bool movingDone = false;

	void Start () 
	{
		
	}

	IEnumerator IndicatorCoroutine()
	{
		if (indicator.activeInHierarchy == true) 
		{
			indicator.SetActive (false);
		};

		yield return new WaitForSeconds(timeToWait);

		if (indicator.activeInHierarchy == false) 
		{
			indicator.SetActive (true);
		};
		indicatorSwitchisHit = false;
	}

	void Update () 
	{
		//Debug.Log ("Input Handler is running");
		if (indicatorSwitchisHit == false) 
		{
			indicatorSwitchisHit = true;
			StartCoroutine (IndicatorCoroutine ());
		}

		if (Input.touches.Length == 1) {
			Touch touchedFinger = Input.touches [0];

			timeToWait = 0.5f;

			switch (touchedFinger.phase) {

			case TouchPhase.Began: 
				m_Ray = Camera.main.ScreenPointToRay (touchedFinger.position);
				if (Physics.Raycast (m_Ray.origin, m_Ray.direction, out m_RayCastHit, Mathf.Infinity)) {
					MoveableObject movableObj = m_RayCastHit.collider.gameObject.GetComponent<MoveableObject> ();
					Debug.Log ("Moveable Object: " + movableObj.name);
					if (movableObj) {
						m_CurrentMovableObject = movableObj;
						m_CurrentMovableObject.gameObject.transform.Translate ( Vector3.up * 5) ;
					}
				}
				break;
			case TouchPhase.Moved:
				if (m_CurrentMovableObject) {


					//if (m_CurrentMovableObject.gameObject.transform.position.y <= 75f && toMoveUp == true) {
					//	m_CurrentMovableObject.gameObject.transform.Translate (Vector3.up * touchedFinger.deltaPosition.y);
					//}

					Debug.Log("Position of Movable Obj: " + m_CurrentMovableObject.gameObject.transform.position.y);


					//Moves up and down, left and right, but not forwards and backwards. 
					//m_CurrentMovableObject.gameObject.transform.Translate (Vector3.left * touchedFinger.deltaPosition.x);
					//m_CurrentMovableObject.gameObject.transform.Translate (Vector3.up * touchedFinger.deltaPosition.y);



				}
				break;
			case TouchPhase.Ended:
				
				m_CurrentMovableObject = null;

				toMoveUp = false;
				toMoveIn = false; 
				toRotateIn = false;
				movingDone = false;

>>>>>>> master
				break;
			default:
				break;
			}
<<<<<<< HEAD
		} 

		if (Input.touches.Length == 2 && m_PreviousMovableObject != null) {
			if (m_PreviousMovableObject.gameObject.GetComponent<Animator> () != null) {
				m_PreviousMovableObject.gameObject.GetComponent<Animator> ().Play ("CupTip");
				Debug.Log ("Double Touch Cup Tip Hit");
			}
=======
		} else {
			timeToWait = 2f;
>>>>>>> master
		}
	}
}