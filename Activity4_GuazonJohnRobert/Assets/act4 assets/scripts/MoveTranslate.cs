using UnityEngine;
using System.Collections;

public class MoveTranslate : MonoBehaviour {

    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _maxDistance = 5f;

    private float _startPosition;
    private bool _moveRight = true;

	// Use this for initialization
	void Start () {
        _startPosition = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        float x = transform.position.x;

        if (x < _maxDistance && _moveRight)
        {
            x += _speed * Time.deltaTime;
        } else
        {
            _moveRight = false;
            x += -_speed * Time.deltaTime;
           if (x <= _startPosition - _maxDistance)
            {
                _moveRight = true;
            }
        }
        transform.position = new Vector3(x,transform.position.y,transform.position.z);
	}
}
