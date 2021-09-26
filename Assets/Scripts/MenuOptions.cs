using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] Image MusicBTN;
    [SerializeField] Image SFXBTN;
    [SerializeField] Image DemoMode;
    [SerializeField] Sprite ButtonOff;
    [SerializeField] Sprite ButtonOn;

    private bool mutemusic = false;
    private bool mutesfx = false;
    private bool showDemoMode = false;

    bool[] holdingDemo = new bool[4];

    void Start()
    {
        showDemoMode = PlayerPrefs.GetInt("DemoMode", 0) == 0 ? false : true;
        GameManager.Instance.IsDemoActive = showDemoMode;
        DemoMode.gameObject.SetActive(showDemoMode);
        mutemusic = PlayerPrefs.GetInt("mutemusic", 0) == 0 ? false : true;
        mutesfx = PlayerPrefs.GetInt("mutesfx", 0) == 0 ? false : true;
        if (mutemusic)
        {
            MusicBTN.sprite = ButtonOn;
        }
        if (mutesfx)
        {
            SFXBTN.sprite = ButtonOn;
        }
    }

    public void OnMuteMusic()
    {
        mutemusic = !mutemusic;
        int val = mutemusic == false ? 0 : 1;
        MusicBTN.sprite = mutemusic == false ? ButtonOff : ButtonOn;
        PlayerPrefs.SetInt("mutemusic", val);
        AudioManager.Instance.SetMute(mutemusic, mutesfx);
    }

    public void OnMuteSFX()
    {
        mutesfx = !mutesfx;
        int val = mutesfx == false ? 0 : 1;
        SFXBTN.sprite = mutesfx == false ? ButtonOff : ButtonOn;
        PlayerPrefs.SetInt("mutesfx", val);
        AudioManager.Instance.SetMute(mutemusic, mutesfx);
    }
}
