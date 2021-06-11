using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region singleton
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    [SerializeField] AudioSource MusicPlayer = null;
    [SerializeField] List<AudioSource> SFXPlayer = null;
    [SerializeField] int MusicFiles = 0;
    private int musicFileNumber = -1;
    private int effectPlayerNumber = -1;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        NextMusicFile();
    }

    public void NextMusicFile()
    {
        musicFileNumber++;
        if (musicFileNumber > MusicFiles-1)
        {
            musicFileNumber = 0;
        }
        var name = musicFileNumber.ToString();
        Debug.Log($"Loading music file: {name}");
        var clip = Resources.Load<AudioClip>($"Music/{name}");
        MusicPlayer.clip = clip;
        MusicPlayer.Play();
    }

    private void PlayEffect(string name)
    {
        effectPlayerNumber++;
        if (effectPlayerNumber > SFXPlayer.Count - 1)
        {
            effectPlayerNumber = 0;
        }
        Debug.Log($"Loading music file: {name}");
        var clip = Resources.Load<AudioClip>($"SFX/{name}");
        SFXPlayer[effectPlayerNumber].clip = clip;
        SFXPlayer[effectPlayerNumber].Play();
    }
}
