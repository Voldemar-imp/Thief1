using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private bool _isInside = false;

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed*Time.deltaTime);        
    }

    public bool IsInside()
    {
        _isInside = !_isInside;
        _renderer.enabled = !_isInside;
        return _isInside;
    }
}
