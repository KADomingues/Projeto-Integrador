using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAudioControl : MonoBehaviour
{
    private bool AudioPlaying;
    private AudioSource SourceAudio;

    private void Start()
    {
        SourceAudio = GetComponent<AudioSource>();
        SourceAudio.volume = 0;
    }

    private void Update()
    {
        if (AudioPlaying)
        {
            if (SourceAudio.volume <= 0.2f)
            {
                SourceAudio.volume += 0.005f * Time.deltaTime;
            }
        }

        if (!AudioPlaying)
        {
            if (SourceAudio.volume >= 0)
            {
                SourceAudio.volume -= 0.01f * Time.deltaTime;
            }

            if (SourceAudio.volume == 0)
            {
                SourceAudio.Stop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && AudioPlaying == false)
        {
            AudioPlaying = true;
            SourceAudio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && AudioPlaying == true)
        {
            AudioPlaying = false;
        }
    }
}