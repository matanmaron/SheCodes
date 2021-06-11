using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForHandler : MonoBehaviour
{
    [Header("Do Not Change")]
    [SerializeField] private Text forText = null;
    [SerializeField] private Button minusButton = null ;
    [SerializeField] private Button plusButton = null;

    public bool isActive = false;

    public int forNum = 1;
    
    public static ForHandler Instance { get; private set; } //singleton
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
    }
    
    private void Start() {
        ResetFor();
    }

    public void ToggleFor ()
    {
        forText.gameObject.SetActive(!isActive);
        minusButton.gameObject.SetActive(!isActive);
        plusButton.gameObject.SetActive(!isActive);

        isActive = !isActive;

        if (!isActive)
        {
            forNum = 1;
        }
        else
        {
            forText.text = forNum.ToString();
        }
    }

    public void IncreaseFor()
    {
        if (forNum < 9)
        {
            forNum++;
            forText.text = forNum.ToString();
        }
    }
    public void DecreaseFor()
    {
        if (forNum > 1)
        {
            forNum--;
            forText.text = forNum.ToString();
        }
    }

    public void ResetFor()
    {
        forNum = 1;
        forText.text = forNum.ToString();

        forText.gameObject.SetActive(false);
        minusButton.gameObject.SetActive(false);
        plusButton.gameObject.SetActive(false);

        isActive = false;

    }
}
