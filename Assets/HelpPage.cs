using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPage : MonoBehaviour
{
    [SerializeField]
    private Button btn;

    [SerializeField]
    private GameObject nextPage;

    private void Awake()
    {
        btn.onClick.AddListener(NextPage);
    }

    private void NextPage()
    {
        nextPage.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
