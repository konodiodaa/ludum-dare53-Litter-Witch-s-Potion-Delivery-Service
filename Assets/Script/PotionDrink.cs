using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionDrink : MonoBehaviour
{
    private PotionName pn;
    [SerializeField]
    private GameObject PotionEffectPanel;
    private Text timer_text;
    private Image PotionImg;

    private HeroUnit hu;

    private float timer;
    [SerializeField]
    private float effectTimer;

    public bool isDrink;

    private float freq;
    private float nextEffectTime;

    private int DrinkCout;

    private int OriginATK;

    private void Awake()
    {
        isDrink = false;
        DrinkCout = 0;
        pn = PotionName.Null;
        hu  = GetComponent<HeroUnit>();
        timer_text = PotionEffectPanel.GetComponentInChildren<Text>();
        PotionImg = PotionEffectPanel.GetComponent<Image>();
    }


    private void Update()
    {
        timer -= Time.deltaTime;
        timer_text.text = ((int)timer).ToString();

        if(timer <= 0)
        {
            Effect_End();
            isDrink = false;
            PotionEffectPanel.gameObject.SetActive(false);
            pn= PotionName.Null;
        }
        else
        {
            switch (pn)
            {
                case PotionName.Leaf:
                    Effect_Leaf_Slow();
                    break;
                case PotionName.Mutation:
                    Effect_Mutation_Slow();
                    break;
                case PotionName.Stun:
                    Effect_Stun_Slow();
                    break;
                case PotionName.Null:
                    break;
            }
        }

    }

    public void Drink(PotionName pn)
    {
        DrinkCout++;
        isDrink = true;
        PotionEffectPanel.SetActive(true);
        PotionImg.sprite = PotionData.Instance.GetPotionImage(pn);
        timer = effectTimer;
        this.pn = pn;
        Effect_Quick();
    }

    private void Effect_Quick()
    {
        Debug.Log(pn);
        switch (pn)
        {
            case PotionName.Lava:
                Effect_Lava();
                break;
            case PotionName.Leaf:
                Effect_Leaf_Quick();
                break;
            case PotionName.Ocean:
                Effect_Ocean_Quick();
                break;
            case PotionName.Strength:
                Effect_STR_Quick();
                break;
            case PotionName.Mutation:
                Effect_Mutation_Quick();
                break;
            case PotionName.Agility:
                Effect_Agility_Quick();
                break;
            case PotionName.Stun:
                Effect_Stun_Quick();
                break;
            case PotionName.Potential:
                Effect_Potential_Quick();
                break;
            case PotionName.Immunity:
                Effect_Immunity_Quick();
                break;
            case PotionName.Universal:
                Effect_Universal_Quick();
                break;
        }
    }

    private void Effect_End()
    {
        switch (pn)
        {
            case PotionName.Lava:
                hu.ATKChange(-2);
                break;
            case PotionName.Leaf:
                //hu.TakenDamage(-3);
                break;
            case PotionName.Ocean:
                hu.oceanEffect = false;
                break;
            case PotionName.Strength:
                hu.strEffect = false;
                break;
            case PotionName.Mutation:
                Effect_Mutation_End();
                break;
            case PotionName.Agility:
                hu.AgEffect = false;
                break;
            case PotionName.Stun:
                Effect_Stun_End();
                break;
            case PotionName.Potential:
                Effect_Potential_End();
                break;
            case PotionName.Immunity:
                Effect_Immunity_End();
                break;
            case PotionName.Universal:
                Effect_Universal_End();
                break;
        }
    }

    private void Effect_Lava()
    {
        
        hu.ATKChange(2);
        hu.TakenDamage(2);
    }

    private void Effect_Leaf_Quick()
    {
        freq = 2.5f;
        nextEffectTime = Time.time + freq;
        hu.TakenDamage(6);
    }

    private void Effect_Leaf_Slow()
    {
        if (Time.time > nextEffectTime)
        {
            hu.TakenDamage(-3);
            nextEffectTime += freq;
        }
    }

    private void Effect_Ocean_Quick()
    {
        hu.oceanEffect = true;
    }

    private void Effect_Mutation_Slow()
    {
        if (Time.time > nextEffectTime)
        {
            hu.TakenDamage(-8);
            hu.ATKChange(-3);
            nextEffectTime += freq;
        }
    }

    private void Effect_Mutation_Quick()
    {
        freq = 5.0f;
        hu.ATKChange(2);
        hu.TakenDamage(3);
        nextEffectTime = Time.time + freq;
    }

    private void Effect_Mutation_End()
    {
        hu.ATKChange(1);
    }

    private void Effect_STR_Quick()
    {
        hu.ATKChange(1);
        hu.strEffect = true;
    }

    private void Effect_Agility_Quick()
    {
        hu.AgEffect = true;
    }

    private void Effect_Stun_Quick()
    {
        freq = 5.1f;
        hu.Stun();
        nextEffectTime = Time.time + freq;
    }

    private void Effect_Stun_Slow()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,1.5f,LayerMask.GetMask("EnemyLayer"));
        if (colliders.Length == 0) return;
        foreach(var collider in colliders)
        {
            if(collider.gameObject != null)
                collider.GetComponent<Enemy>().takenDMG(3);
        }
    }

    private void Effect_Stun_End()
    {
        hu.UnStun();
    }

    private void Effect_Potential_Quick()
    {
        hu.TakenDamage(-DrinkCout);
        hu.ATKChange(DrinkCout);
    }

    private void Effect_Potential_End()
    {
        hu.ATKChange(-DrinkCout);
    }

    private void Effect_Immunity_Quick()
    {
        hu.isImmunity = true;
        OriginATK = hu.ATKGet();
        hu.ATKSet(0);
    }

    private void Effect_Immunity_End()
    {
        hu.isImmunity = false;
        hu.ATKSet(OriginATK);
    }

    private void Effect_Universal_Quick()
    {
        hu.isUniversal = true;
    }

    private void Effect_Universal_End()
    {
        hu.isUniversal = false;
    }
}
