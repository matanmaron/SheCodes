using Shecodes.Frame;
using Shecodes.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] movementButtons;
    [SerializeField] private GameObject[] levelButtons;

    private Vector2 buttonsSpaceLeft = new Vector2(857.5f, 1687.5f);
    private Vector2 buttonsSpaceRight = new Vector2(899.5f, 69.5f);
    private float buttonSize = 286f;

    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        InitButtonBase();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    void InitButtonBase()
    {
        InitMovementButtons();
        InitLevelButtons();

    }

    void InitMovementButtons()
    {
        foreach (var btn in movementButtons)
        {
            Debug.Log("btn: " + btn);
            btn.GetComponent<Button>().interactable = true;
            string btnText = btn.GetComponentInChildren<TextMeshProUGUI>().text;
            btn.GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(btnText); });
        }
    }

    void InitLevelButtons()
    {
        int j = 1 ;
        int buttonsCount = levelButtons.Length > 2 ? 3 : levelButtons.Length;
        for (int i=0; i<buttonsCount; i++,j++)
        {
            float left = 857.5f;
            float right = 899.5f;
            RectTransform rt = levelButtons[i].GetComponent<RectTransform>();
            Debug.Log("btn: " + levelButtons[i]);
            levelButtons[i].GetComponent<Button>().interactable = true;
            string btnText = levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text;
            levelButtons[i].GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(btnText); });
            rt.offsetMin = new Vector2((j*buttonSize)+left, rt.offsetMin.y);
            rt.offsetMax = new Vector2((j*buttonSize)-right, rt.offsetMax.y);
        } 

    }

    void SetLevelButton(int index, string text)
    {
        levelButtons[index].GetComponent<Button>().interactable = true;
        levelButtons[index].GetComponent<Button>().GetComponent<TextMeshProUGUI>().text = text;
        levelButtons[index].GetComponent<Button>().onClick.AddListener(delegate { ButtonClick(text); });
    }

    void ButtonClick(string buttonText)
    {
        Debug.Log("Pressed " + buttonText + "!");
        switch (buttonText)
        {
            // TODO: Figure out why GameManager.Instance.MovePlayer(...) is null
            case ">":
                GameManager.Instance.MovePlayer(Direction.Right);
                break;
            case "<":
                GameManager.Instance.MovePlayer(Direction.Left);
                break;
            case "^":
                GameManager.Instance.MovePlayer(Direction.Up);
                break;
            case "V":
                GameManager.Instance.MovePlayer(Direction.Down);
                break;
            case "For":
                // TODO: Implement this
                break;
            case "Press Button":
                // TODO: Implement this
                break;
            case "Pick up / Drop":
                // TODO: Implement this
                break;
            default:
                break;
        }
        
    }
}
