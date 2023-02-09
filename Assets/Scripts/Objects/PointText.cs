using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointText : MonoBehaviour
{
    [SerializeField] private int playerID;
    TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        text.text = 0.ToString();
    }
    private void OnEnable()
    {
        Config.OnPlayerPointChanged += OnPlayerPointChanged;
    }

    private void OnPlayerPointChanged(int ID, int Point)
    {
        if(ID == playerID)
        {
            text.text = Point.ToString();
        }
    }

    private void OnDisable()
    {
        Config.OnPlayerPointChanged -= OnPlayerPointChanged;

    }
}
