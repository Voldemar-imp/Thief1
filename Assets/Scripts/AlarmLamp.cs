using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AlarmLamp : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private DoorsStack _doorsStack;

    private Coroutine _AudioFadeInJob;
    private Animator _animator;
    private Door[] _doors;
    private bool _isInside = false;
    private static int _alarm = Animator.StringToHash("Alarm");
    private static int _endAlarm = Animator.StringToHash("EndAlarm");


    private void OnEnable()
    {
        _doors = _doorsStack.GetComponentsInChildren<Door>();

        foreach (Door door in _doors)
        {
            door.Opened += OnOpened;
        }    
    }

    private void OnDisable()
    {
        foreach (Door door in _doors)
        {
            door.Opened -= OnOpened;
        }      
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();      
    }

    private void SetAlarmAudioLevel()
    {
        if (_AudioFadeInJob != null)
        {
            StopCoroutine(_AudioFadeInJob);
        }

        _AudioFadeInJob = StartCoroutine(AudioFadeIn());
    }

    private IEnumerator AudioFadeIn()
    {
        float step = 0.005f;
        float targetVolume = Convert.ToSingle(_isInside);

        while (_audio.volume != targetVolume)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, targetVolume, step);
            yield return null;
        }
    }

    private void SetAnimationTrigger()
    {
        if (_isInside)
        {
            _animator.SetTrigger(_alarm);
        }
        else
        {
            _animator.SetTrigger(_endAlarm);
        }
    }

    private void OnOpened()
    {
        _isInside = !_isInside;
        SetAlarmAudioLevel();
        SetAnimationTrigger();
    }
}
