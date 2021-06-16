using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject victoryMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Backpack>())
        {
            victoryMenu.SetActive(true);
        }
    }
}