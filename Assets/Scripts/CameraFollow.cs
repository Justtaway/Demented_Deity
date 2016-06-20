using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	Camera mycamera;
	public float m_speed = 0.1f;

	// Use this for initialization
	void Start () {
		
		mycamera = GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		mycamera.orthographicSize = (Screen.height / 100f) / 4f;

		if (target) {
			
			transform.position = Vector3.Lerp(transform.position, target.position, m_speed);// + new Vector3(0, 0, -10);

		}
	}
}
