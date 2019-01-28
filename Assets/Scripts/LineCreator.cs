﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// © 2018 TheFlyingKeyboard and released under MIT License 
// theflyingkeyboard.net 
public class LineCreator : MonoBehaviour
{
    [SerializeField] private GameObject line;
    private Vector2 mousePosition;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Or use GetKeyDown with key defined with mouse button
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(line, mousePosition, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
    }
}