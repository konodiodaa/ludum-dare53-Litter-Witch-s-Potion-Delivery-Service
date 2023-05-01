using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PotionDec : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text Potion_Dec;

    [SerializeField]
    private string Dec_text;

    [SerializeField]
    private GameObject Known;
    [SerializeField]
    private GameObject UnKnown;

    private bool isRevealed;

    private void Awake()
    {
        Potion_Dec = GameObject.Find("PotionDec").GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Time.timeScale = 0f;

        if (!isRevealed)
        {
            Potion_Dec.text = "UnKnown Potion, reveal its effect after it used";
        }
        else
        {
            Potion_Dec.text = Dec_text;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Time.timeScale = 1f;
        Potion_Dec.text = " ";
    }
    public void Reveal()
    {
        isRevealed = true;
        Known.SetActive(true);
        UnKnown.SetActive(false);
    }

    public bool getReveal()
    {
        return isRevealed;
    }

}
