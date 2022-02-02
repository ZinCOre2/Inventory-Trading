using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private Text floatingText;
    public Text FloatingText => floatingText;
    [SerializeField] private Text[] cashOwnersTexts = new Text[2];
    [SerializeField] private Transform itemParent;
    public Transform ItemParent => itemParent;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    
    private void Start()
    {
        floatingText.color = Color.clear;
        
        for (int i = 0; i < cashOwnersTexts.Length; i++)
        {
            ChangeCashText(GameController.Instance.OwnersCash[i], i);
        }
        
        GameController.Instance.OnCashChanged += ChangeCashText;
    }

    private void ChangeCashText(int currentCash, int ownerId)
    {
        cashOwnersTexts[ownerId].text = $"$<i><b><color=#F1DC48>{currentCash}</color></b></i>";
    }
}
