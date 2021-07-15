using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject LevelsPanel;
    [SerializeField] private Button ContinueBTN = null;
    [SerializeField] private GameObject CreditsPanel = null;
    [SerializeField] private GameObject OptionsPanel = null;
    [SerializeField] private GameObject HowToPlayPanel = null;
    [SerializeField] private Image AndroidInstall = null;
    [SerializeField] Transform ParentsGrid = null;
    [SerializeField] GameObject LevelButtonPrefab = null;
    const string URL = @"https://drive.google.com/uc?export=download&id=18ftMMDTJjuZI4K8E3mmd55yXqCx7sV_-";
    private int lastLevel = 0;

    public void Start()
    {
#if !UNITY_ANDROID
        StartCoroutine(GetImage(AndroidInstall));
#else
        Destroy(AndroidInstall);
#endif
        AudioManager.Instance.PlayMenu();
        OnBack();
        lastLevel = PlayerPrefs.GetInt("level", 0);
        if (lastLevel == 0)
        {
            Debug.Log("no saved level");
            ContinueBTN.interactable = false;
        }
        BuildLevelButtons();
    }

    IEnumerator GetImage(Image img)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(URL))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("ERROR IN QR CODE IMG");
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                var bytes = texture.EncodeToPNG();
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                img.overrideSprite = sprite;
            }
        }
    }

    private void BuildLevelButtons()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 1; i < sceneCount; i++)
        {
            var btn = Instantiate(LevelButtonPrefab, ParentsGrid);
            var name = i.ToString("D2");
            btn.name = name;
            btn.GetComponentInChildren<TextMeshProUGUI>().text = name;
            btn.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(name); });
        }
            Debug.Log($"Found {sceneCount-1} Levels");
    }

    private void ShowLevels()
    {
        MenuPanel.SetActive(false);
        LevelsPanel.SetActive(true);
    }

    public void OnBack()
    {
        MenuPanel.SetActive(true);
        LevelsPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
    }

    public void LoadLevel(string number)
    {
        SceneManager.LoadScene(number);
    }

    public void OnStart()
    {
        ShowLevels();
    }

    public void OnContinue()
    {
        SceneManager.LoadScene(lastLevel.ToString("D2"));
    }

    public void OnHowToPlay()
    {
        HowToPlayPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void OnOptions()
    {
        OptionsPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }

    public void OnCredits()
    {
        CreditsPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
}
