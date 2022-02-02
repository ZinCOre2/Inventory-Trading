using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public event Action<int, int> OnCashChanged;

    [SerializeField] private int[] ownersCash = new int[2];
    public int[] OwnersCash => ownersCash;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public bool TradeCheck(int itemCost, int sellerId, int buyerId)
    {
        if (sellerId == buyerId) { return true; }
        if (OwnersCash[buyerId] < itemCost) { return false; }

        OwnersCash[sellerId] += itemCost;
        OwnersCash[buyerId] -= itemCost;
        
        OnCashChanged?.Invoke(OwnersCash[buyerId], buyerId);
        OnCashChanged?.Invoke(OwnersCash[sellerId], sellerId);
        
        return true;
    }
}
