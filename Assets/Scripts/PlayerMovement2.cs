using UnityEngine;
using System.Collections;

public class PlayerMovement2 : MonoBehaviour {

	Rigidbody2D rbody;
	Animator anim;
	public float moveSpeed;
	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

	}

	// Update is called once per frame
	void FixedUpdate () {

		Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));


		if (movement_vector != Vector2.zero)
		{
			anim.SetBool("iswalking", true);
			anim.SetFloat("input_x", movement_vector.x);
			anim.SetFloat("input_y", movement_vector.y);


		}
		else {
			anim.SetBool("iswalking", false);
		}

		rbody.MovePosition(rbody.position + movement_vector * moveSpeed);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad0))
		{
			anim.SetBool("attacking", true);
		}
		else
		{
			anim.SetBool("attacking", false);
		}
	}
}
