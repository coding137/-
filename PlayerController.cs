using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	private float h=0.0f;
	private float v = 0.0f;
	public float movespeed = 10.0f;
	private Transform trans;
	private Rigidbody prgbody;
	int f_mask;
	private Vector3 move;
	public float rotating_Speed = 70.0f;

	//jump
	private bool onGround;
	private float jump;
	private float maxjump;
	private float minjump;
	
	
	void Start()
	{
		f_mask = LayerMask.GetMask("Floor");
		trans = GetComponent<Transform>();
		prgbody = GetComponent<Rigidbody>();

		//jump
		onGround = true;
		jump = 0f;
		minjump = 8f;
		maxjump = 20f;
		
	}
	
	
	void Update()
	{
		
		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");
		Moving (h, v);
		rotating ();

		if (onGround) {
			if (Input.GetButton ("Jump")) {
				if (jump < maxjump) {
					jump += Time.deltaTime * 8f;
				} else {
					jump = maxjump;
				}
			}
			//not jump
			else {
				if (jump > 0f) {
					jump = jump + minjump;
					prgbody.velocity = new Vector3(0f,jump, 0f);
					jump = 0f;
					onGround = false;
				}
			}
		}
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag ("ground")) 
		{
			onGround = true;
		}
	}

	void Moving(float h, float v)
	{
		
		Vector3 way = (Vector3.forward * v) + (Vector3.right) * h;
		
		trans.Translate(way*Time.deltaTime*movespeed,Space.Self);
		
		
		
	}
	
	void rotating()
	{
		trans.Rotate(Vector3.up * Time.deltaTime * rotating_Speed * Input.GetAxisRaw("Mouse X"));
		
	}
}