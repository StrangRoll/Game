using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioSource _audioSource;

    public void Jump()
    {
        _audioSource.PlayOneShot(_jumpSound);
    }

    public void Death()
    {
        _audioSource.PlayOneShot(_deathSound);
    }
}
