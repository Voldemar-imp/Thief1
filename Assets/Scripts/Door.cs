using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private AlarmLamp _alarmLamp;

    private Animator _animator;
    private const string _open = "Open";

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
            bool isInside = thief.IsInside();
            _alarmLamp.SwithAlarm(isInside);           
        }
    }    
}
