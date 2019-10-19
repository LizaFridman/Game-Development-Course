using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioClip build;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip jump;

    public AudioClip Build { get => build;  }
    public AudioClip Hit { get => hit; }
    public AudioClip Jump { get => jump;  }
}
