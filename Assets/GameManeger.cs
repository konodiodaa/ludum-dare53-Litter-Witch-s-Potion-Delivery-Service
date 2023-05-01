using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject losePanel;
    private void Awake()
    {

        EventCenter.AddListener(EventDefine.Die, Lose);
        EventCenter.AddListener(EventDefine.Win, Win);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.Die, Lose);
        EventCenter.RemoveListener(EventDefine.Win, Win);
    }
    private void Lose()
    {
        Time.timeScale = 0;
        losePanel.SetActive(true);
    }

    private void Win()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }
}
