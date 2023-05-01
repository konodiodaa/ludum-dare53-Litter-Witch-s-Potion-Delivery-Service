using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public enum PotionName
{
    Null,
    Lava,
    Leaf,
    Ocean,
    Strength,
    Mutation,
    Agility,
    Stun,
    Potential,
    Immunity,
    Universal
}

public class Pot : MonoBehaviour
{

    private Dictionary<string, PotionName> dict;

    private void Awake()
    {
        dict = new Dictionary<string, PotionName>();


        EventCenter.AddListener<Essence, Essence, Essence>(EventDefine.CraftOrder, Craft);
    }

    private void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<Essence, Essence, Essence>(EventDefine.CraftOrder, Craft);
    }

    private void Craft(Essence type_0, Essence type_1, Essence type_2)
    {

        int[] intArray = {(int)type_0, (int)type_1, (int)type_2};
        Array.Sort(intArray);
        Essence[] EssenceArray = { (Essence)intArray[0], (Essence)intArray[1], (Essence)intArray[2] };

        string tmp = EssenceArray[0].ToString() + "_" + EssenceArray[1].ToString() + "_" + EssenceArray[2].ToString();
        if(dict.ContainsKey(tmp))
        {
            PotionName pn = dict[tmp];
            EventCenter.Broadcast(EventDefine.CraftSuccess, pn);
            Debug.Log(pn);
        }
    }

    private void Init()
    {
        dict.Add("Green_Green_Green", PotionName.Leaf);
        dict.Add("Red_Red_Red", PotionName.Lava);
        dict.Add("Blue_Blue_Blue", PotionName.Ocean);
        dict.Add("Green_Red_Red", PotionName.Strength);
        dict.Add("Green_Green_Red", PotionName.Mutation);
        dict.Add("Red_Red_Blue", PotionName.Agility);
        dict.Add("Red_Blue_Blue", PotionName.Stun);
        dict.Add("Green_Blue_Blue", PotionName.Potential);
        dict.Add("Green_Green_Blue", PotionName.Immunity);
        dict.Add("Green_Red_Blue", PotionName.Universal);
    }

}
