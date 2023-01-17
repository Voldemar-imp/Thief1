using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AlarmLamp : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;

    private Coroutine _alarmAudioLevel;
    private Animator _animator;
    private bool _isInside = false;
    private const string _alarm = "Alarm";
    private const string _endAlarm = "EndAlarm";

    private void Start()
    {
        _animator = GetComponent<Animator>();      
    }

    private void SetAlarmAudioLevel()
    {
        if (_alarmAudioLevel != null)
        {
            StopCoroutine(_alarmAudioLevel);
        }

        _alarmAudioLevel = StartCoroutine(AudioFadeIn());
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

    public void SwithAlarm(bool isInside)
    {
        _isInside = isInside;
        SetAlarmAudioLevel();
        SetAnimationTrigger();
    }
}
