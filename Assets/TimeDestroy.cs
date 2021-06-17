using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 0.5f;
    void Start()
    {
        Destroy(gameObject,timeToDestroy);
    }

}
