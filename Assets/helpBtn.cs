using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class helpBtn : MonoBehaviour
{

    private Button btn;

    [SerializeField]
    private GameObject helpPage;

    // Start is called before the first frame update
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OpenHelpPage);
    }

    private void OpenHelpPage()
    {
        Time.timeScale = 0;
        helpPage.SetActive(true);
    }

}
