using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AlarmLamp _alarmLamp;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _animator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {  
            float sign = 1f;
            bool isOutside = thief.IsOutside();
            _alarmLamp.OnAlarm(isOutside);

            if (isOutside) { sign = -sign; }           

            var volume = StartCoroutine(AudioFadeIn(sign));
        }
    }

    private IEnumerator AudioFadeIn(float sign)
    {
        float stepsNumber = 1000f;
        var audioVolume = _audio.volume;

        for (int i = 0; i < stepsNumber; i++)
        {    
            audioVolume += sign / stepsNumber;           
            _audio.volume = audioVolume;
            yield return null;
        }
    }
}
