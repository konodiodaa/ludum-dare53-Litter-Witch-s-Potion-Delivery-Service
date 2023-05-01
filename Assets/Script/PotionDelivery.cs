using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionDelivery : MonoBehaviour
{
    [SerializeField]
    private PotionName pn;

    [SerializeField]
    private Image potionImage;

    private void Awake()
    {
        EventCenter.AddListener<PotionName>(EventDefine.CraftSuccess, CarryPotion);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<PotionName>(EventDefine.CraftSuccess, CarryPotion);
    }

    private void CarryPotion(PotionName pn)
    {
        this.pn = pn;
        Sprite spr =  PotionData.Instance.GetPotionImage(pn);
        if (spr != null)
        {
            potionImage.gameObject.SetActive(true);
            potionImage.sprite = spr;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hero" && pn != PotionName.Null && !collision.GetComponent<PotionDrink>().isDrink)
        {
            EventCenter.Broadcast(EventDefine.Reveal, PotionData.Instance.GetPotionIndex(pn)); 
            potionImage.gameObject.SetActive(false);
            collision.GetComponent<PotionDrink>().Drink(pn);
            pn = PotionName.Null;
        }
    }
}
