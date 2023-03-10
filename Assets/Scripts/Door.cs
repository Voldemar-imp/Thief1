using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private UnityEvent _opened; 

    private Animator _animator;    
    private static int _open = Animator.StringToHash("Open");

    public event UnityAction Opened
    {
        add => _opened?.AddListener(value);
        remove => _opened?.RemoveListener(value);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _animator.SetTrigger(_open);           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {            
            thief.GoInside();
            _opened?.Invoke();
        }
    }    
}
