using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collecting : MonoBehaviour
{
    [SerializeField]
    private List<Essence> collections;

    [SerializeField]
    private List<Image> balls;

    [SerializeField]
    private Text tip;

    private bool craftavb;

    private void Awake()
    {
        craftavb = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && craftavb && collections.Count == 3)
        {
            Craft();
            Debug.Log("crafting success!");
        }
    }

    private void Craft()
    {
        if (collections.Count != 3) return;

        EventCenter.Broadcast(EventDefine.CraftOrder, collections[0], collections[1], collections[2]);

        foreach(var ball in balls)
        {
            ball.color = Color.white;
        }

        collections.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Essence")
        {
            if (collections.Count < 3)
            {
                collections.Add(collision.GetComponent<EssenceBall>().GetEssenceType());
                Destroy(collision.gameObject);
                switch (collections[collections.Count - 1])
                {
                    case Essence.Red:
                        balls[collections.Count - 1].color = Color.red;
                        break;
                    case Essence.Green:
                        balls[collections.Count - 1].color = Color.green;
                        break;
                    case Essence.Blue:
                        balls[collections.Count - 1].color = Color.blue;
                        break;
                }
            }
        }
        else if(collision.gameObject.tag == "Pot")
        {
            craftavb = true;
            if (collections.Count == 3)
            {
                tip.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pot")
        {
            tip.gameObject.SetActive(false);
            craftavb = false;
        }
    }
}
