using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour 
{
	private Ray m_Ray;
	private RaycastHit m_RayCastHit;
	private MoveableObject m_CurrentMovableObject;

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

				break;
			default:
				break;
			}
		} else {
			timeToWait = 2f;
		}
	}
}