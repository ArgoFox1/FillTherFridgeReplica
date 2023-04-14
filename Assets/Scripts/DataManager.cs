using System.Collections.Generic;
using UnityEngine;
public class DataManager : MonoBehaviour
{

    public int spaceCount;

    public int coinCount;

    public int boolCount;

    public bool isChoosed;

    public bool isRemoving;

    public bool canPlace;

    public bool canMilk;

    public bool canPringles;

    public bool canCoke;

    public bool isPlacing;

    public bool isSwiping;

    public List<bool> foodBools;
    private void Start()
    {
        foodBools = new List<bool>() { false,canCoke, canMilk, canPringles };
        boolCount = 0;
    }
}

