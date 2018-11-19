using UnityEngine;
using System.Collections;

public class MouseControler : MonoBehaviour {

	public float offsetX = 15.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, Input.GetAxis ("Mouse X") * offsetX, 0);
	}
}
