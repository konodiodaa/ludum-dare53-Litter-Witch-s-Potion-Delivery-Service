using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPage_2 : MonoBehaviour
{
    [SerializeField]
    private Button btn;
    // Start is called before the first frame update
    void Awake()
    {
        btn.onClick.AddListener(EndPage);
    }

    private void EndPage()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}
