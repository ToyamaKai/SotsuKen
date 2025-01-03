using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    [SerializeField]
    AudioSource m_runAudioSource;
    [SerializeField]
    AudioClip m_runAudio;

    public void PlayRunSound()
    {
        m_runAudioSource.PlayOneShot(m_runAudio);
    }
}
