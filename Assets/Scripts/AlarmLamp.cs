using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLamp : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();      
    }

    public void OnAlarm(bool _isOutside)
    {
        if (_isOutside == false) _animator.SetTrigger("Alarm"); 
        else  _animator.SetTrigger("EndAlarm"); 
    }
}
