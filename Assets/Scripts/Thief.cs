using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Transform _target;

    private bool _isOutside = true;


    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed*Time.deltaTime);        
    }

    public bool IsOutside()
    {
        _isOutside = !_isOutside;
        _renderer.enabled= _isOutside;
        return _isOutside;
    }
}
