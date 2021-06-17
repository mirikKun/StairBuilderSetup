using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField] private Menu menu;

    [SerializeField] private GameObject boom;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Backpack>())
        {
            Instantiate(boom, other.transform.position, Quaternion.identity);
            menu.Victory();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}