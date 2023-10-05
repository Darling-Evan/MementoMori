using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour {
    [SerializeField] private GameObject InventoryItem;
    [SerializeField] private Transform Content;

    public static DataManager Instance;


    private void Awake() {
        Instance = this;
    }
}