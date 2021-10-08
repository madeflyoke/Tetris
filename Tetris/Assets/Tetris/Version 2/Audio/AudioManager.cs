using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Sound> sounds = new List<Sound>();
    private AudioSource pointsAddSfx;
    private AudioSource mainTheme;

    private void OnEnable()
    {
        EventManager.pointsAddEvent += PointsAddPlaySFX;
    }
    private void OnDisable()
    {
        EventManager.pointsAddEvent -= PointsAddPlaySFX;

    }

    private void Awake()
    {
        foreach (Sound item in sounds)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = item.audioClip;
            audioSource.volume = item.volume;
            audioSource.loop = item.isLoop;
            audioSource.playOnAwake = item.isPlayOnAwake;
            if (item.name=="PointsAddSfx")
            {
                pointsAddSfx = audioSource;
            }
            if (item.name=="MainTheme")
            {
                mainTheme = audioSource;
            }
        }
    }

    private void Start()
    {
        mainTheme.Play();
    }

    private void PointsAddPlaySFX(int tmp)
    {
        pointsAddSfx.Play();
    }
}
