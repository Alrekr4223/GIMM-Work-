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
							//m_CurrentMovableObject.gameObject.transform.Translate ( Vector3.up * 5) ;
						}
					}

					if (m_RayCastHit.collider.gameObject.GetComponent<ZTransferButton> () != null) {
						GameObject button = m_RayCastHit.collider.gameObject.GetComponent<ZTransferButton> ().gameObject;
						float btnPositionZ = button.transform.position.z;

						float prevObjX = m_PreviousMovableObject.gameObject.transform.position.x;
						float prevObjY = m_PreviousMovableObject.gameObject.transform.position.y;

						Vector3 moveLocation = new Vector3 (prevObjX, prevObjY, btnPositionZ);

						if (m_PreviousMovableObject != null) {
							m_PreviousMovableObject.gameObject.transform.position = moveLocation;
						}

						//if (m_CurrentMovableObject != null) {
						//	m_CurrentMovableObject.gameObject.transform.Translate (Vector3.back * btnPositionZ);
						//}
						/*
						if (button.CompareTag ("ZButton1")) {
							//Move the moveable object to the z of Zbutton1
							m_CurrentMovableObject.gameObject.transform.transform.position.z = button.transform.position.z;
						}else if (button.CompareTag ("ZButton2")) {
							//Move the moveable object to the z of Zbutton2
						}else if (button.CompareTag ("ZButton3")) {
							//Move the moveable object to the z of Zbutton3
						}
						*/
					}
				}
			break;
			case TouchPhase.Moved:
				if (m_CurrentMovableObject) {
					//Debug.Log("Position of Movable Obj: " + m_CurrentMovableObject.gameObject.transform.position.y);

					//Moves up and down, left and right, but not forwards and backwards. 
					m_CurrentMovableObject.gameObject.transform.Translate (Vector3.left * touchedFinger.deltaPosition.x /2);
					m_CurrentMovableObject.gameObject.transform.Translate (Vector3.up * touchedFinger.deltaPosition.y /2);

				}
			break;
			case TouchPhase.Ended:
				if (m_CurrentMovableObject != null) {
					m_PreviousMovableObject = m_CurrentMovableObject;
				}
				m_CurrentMovableObject = null;
				break;
			default:
				break;
			}
		} 
	}
}