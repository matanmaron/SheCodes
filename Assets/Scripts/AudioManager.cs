using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    const string DROP = "Drop";
    const string END_LEVEL_REACHED = "End Level Reached";
    const string ERROR = "Error";
    const string MOVE_1 = "Move 1";
    const string MOVE_2 = "Move 2";
    const string MOVE_3 = "Move 3";
    const string PICKUP = "PickUp";
    const string PLACE_ON_PEDESTAL_1 = "Place On Pedestal 1";
    const string PLACE_ON_PEDESTAL_2 = "Place On Pedestal 2";
    const string SWITCH_1 = "Switch 1";
    const string SWITCH_2 = "Switch 2";
    const string OUTOFMOVES = "sad-trombone";

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        NextMusicFile();
        Player.OnDropVariaball += PlayDrop;
        LevelManager.OnLevelEnd += PlayEndLevelReached;
        Player.OnIllegalAction += PlayError;
        LevelManager.OnOutOfMoves += PlayOutOfMoves;
        Player.OnWalk += PlayMove;
        Player.OnPickUpVariaball += PlayPickUp;
        Player.OnPlaceVariaballOnPedestal += PlayPlaceOnPedestal;
        Player.OnPressSwitch += PlaySwitch;
    }

    public void PlayMenu()
    {
        musicFileNumber = -1;
        NextMusicFile();
    }

    public void NextMusicFile()
    {
        musicFileNumber++;
        if (musicFileNumber > MusicFiles-1)
        {
            musicFileNumber = 1;
        }
        var name = musicFileNumber.ToString();
        Debug.Log($"Loading music file: {name}");
        var clip = Resources.Load<AudioClip>($"Music/{name}");
        MusicPlayer.clip = clip;
        MusicPlayer.Play();
    }

    internal void SetDemo(bool pus)
    {
        if (pus)
        {
            MusicPlayer.Pause();
        }
        else
        {
            MusicPlayer.Play();
        }
    }

    private void PlayEffect(string name)
    {
        effectPlayerNumber++;
        if (effectPlayerNumber > SFXPlayer.Count - 1)
        {
            effectPlayerNumber = 0;
        }
        Debug.Log($"Loading sfx file: {name}");
        var clip = Resources.Load<AudioClip>($"SFX/{name}");
        SFXPlayer[effectPlayerNumber].clip = clip;
        SFXPlayer[effectPlayerNumber].Play();
    }

    public void SetMute(bool music, bool sfx)
    {
        MusicPlayer.mute = music;
        foreach (var player in SFXPlayer)
        {
            player.mute = sfx;
        }
        Debug.Log($"mute music is now: {music} | mute sfx is now: {sfx}");
    }

    void PlayDrop()
    {
        PlayEffect(DROP);
    }

    void PlayEndLevelReached()
    {
        PlayEffect(END_LEVEL_REACHED);
    }

    void PlayError()
    {
        PlayEffect(ERROR);
    }

    void PlayMove()
    {
        var res = Random.Range(1, 4);
        if (res==1)
        {
            PlayEffect(MOVE_1);
        }
        else if (res==2)
        {
            PlayEffect(MOVE_2);
        }
        else
        {
            PlayEffect(MOVE_3);
        }
    }

    void PlayPickUp()
    {
        PlayEffect(PICKUP);
    }

    void PlayPlaceOnPedestal()
    {
        var res = Random.Range(1, 3);
        if (res == 1)
        {
            PlayEffect(PLACE_ON_PEDESTAL_1);
        }
        else
        {
            PlayEffect(PLACE_ON_PEDESTAL_2);
        }
    }

    void PlaySwitch()
    {
        var res = Random.Range(1, 3);
        if (res == 1)
        {
            PlayEffect(SWITCH_1);
        }
        else
        {
            PlayEffect(SWITCH_2);
        }
    }

    void PlayOutOfMoves()
    {
        PlayEffect(OUTOFMOVES);
    }
}
