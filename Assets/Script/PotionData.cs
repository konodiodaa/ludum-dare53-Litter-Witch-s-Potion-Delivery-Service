using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionData : MonoBehaviour
{

    private Dictionary<PotionName, int> dict = new Dictionary<PotionName, int>();

    [SerializeField]
    private Sprite[] PotionSprs;

    private static PotionData _instance;

    public static PotionData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PotionData)) as PotionData;
            }
            return _instance;
        }
    }

    private void Awake()
    {
        init();
    }

    private void init()
    {
        dict.Add(PotionName.Lava, 0);
        dict.Add(PotionName.Leaf, 1);
        dict.Add(PotionName.Ocean, 2);
        dict.Add(PotionName.Strength, 3);
        dict.Add(PotionName.Mutation, 4);
        dict.Add(PotionName.Agility, 5);
        dict.Add(PotionName.Stun, 6);
        dict.Add(PotionName.Potential, 7);
        dict.Add(PotionName.Immunity, 8);
        dict.Add(PotionName.Universal, 9);
    }

    public Sprite GetPotionImage(PotionName pn)
    {
        if (dict.ContainsKey(pn))
        {
            return PotionSprs[dict[pn]];
        }
        else
            return null;
    }

    public int GetPotionIndex(PotionName pn)
    {
        Debug.Log(pn);
        if (dict.ContainsKey(pn))
        {
            return dict[pn];
        }
        else
            return -1;
    }
}
