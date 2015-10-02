//Written by Rob Verhoef
using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    private Transform _player;
    private float _followSpeed = 5f;

	void Awake ()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	void FixedUpdate ()
    {
        float step = _followSpeed * Time.smoothDeltaTime;

        //Camera follows player with a slight delay
        if (_player != null)
        {
            transform.position = Vector3.Lerp (transform.position, new Vector3(_player.transform.position.x, _player.transform.position.y, -10), step);
        }
    }
}
