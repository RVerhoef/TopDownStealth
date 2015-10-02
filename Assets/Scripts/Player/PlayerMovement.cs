//Written by Rob Verhoef
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	private float _speed = 5;
	private Rigidbody2D _rigidBody;
	private Vector3 _mousePos;

	void Awake () 
	{
		_rigidBody = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate () 
	{
		//Player can move in 8 directions by pressing a combinations of the movement keys
		if(Input.GetAxis ("Horizontal") < 0 || Input.GetAxis ("Horizontal") > 0)
		{
			_rigidBody.velocity = new Vector2 ((Input.GetAxis ("Horizontal") * (_speed)), _rigidBody.velocity.y);
		}
		else
		{
			_rigidBody.velocity = new Vector2 (0, _rigidBody.velocity.y);
		}

		if(Input.GetAxis ("Vertical") < 0 || Input.GetAxis ("Vertical") > 0)
		{
			_rigidBody.velocity = new Vector2 (_rigidBody.velocity.x, (Input.GetAxis ("Vertical") * (_speed)));
		}
		else
		{
			_rigidBody.velocity = new Vector2 (_rigidBody.velocity.x, 0);
		}
		//Player rotates towards the mouse pointer / recticle 
		_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation = Quaternion.LookRotation(Vector3.forward, _mousePos - transform.position);
    }
}
