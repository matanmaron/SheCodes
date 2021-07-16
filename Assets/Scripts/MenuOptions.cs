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

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private void Update()
    {
        if (!holdingDemo[0] && Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("DEMO D");
            holdingDemo[0] = true;
            return;
        }
        else if (holdingDemo[0] && Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log("DEMO D");
            holdingDemo[0] = false;
            return;
        }
        if (!holdingDemo[1] && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("DEMO E");
            holdingDemo[1] = true;
            return;
        }
        else if (holdingDemo[1] && Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("DEMO E");
            holdingDemo[1] = false;
            return;
        }
        if (!holdingDemo[2] && Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("DEMO M");
            holdingDemo[2] = true;
            return;
        }
        else if (holdingDemo[2] && Input.GetKeyUp(KeyCode.M))
        {
            Debug.Log("DEMO M");
            holdingDemo[2] = false;
            return;
        }
        if (!holdingDemo[3] && Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("DEMO O");
            holdingDemo[3] = true;
            return;
        }
        else if (holdingDemo[3] && Input.GetKeyUp(KeyCode.O))
        {
            Debug.Log("DEMO O");
            holdingDemo[3] = false;
            return;
        }
        if (holdingDemo[0] && holdingDemo[1] && holdingDemo[2] && holdingDemo[3])
        {
            showDemoMode = !showDemoMode;
            GameManager.Instance.IsDemoActive = showDemoMode;
            int val = showDemoMode == false ? 0 : 1;
            DemoMode.gameObject.SetActive(showDemoMode);
            PlayerPrefs.SetInt("DemoMode", val);
            Debug.Log($"***********DEMO ACTIVATED {showDemoMode}***********");
            holdingDemo[0] = false;
            holdingDemo[1] = false;
            holdingDemo[2] = false;
            holdingDemo[3] = false;
            return;
        }
    }
#endif

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
