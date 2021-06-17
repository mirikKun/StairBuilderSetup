using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBuilder : MonoBehaviour
{
    [SerializeField] private Vector2 offset;
    [SerializeField] private Transform placeToBuild;
    [SerializeField] private Transform parent;
    [SerializeField] private int goalStepCount = 28;
    [SerializeField] private GameObject stepParticle; 
    private int _currentStepCount; 

    private void OnTriggerEnter(Collider other)
    {
        Transform newStep = other.gameObject.GetComponent<Backpack>()?.StepGetOut();
        if (newStep)
        {
            _currentStepCount++;
            newStep.parent = parent;
            newStep.localEulerAngles = Vector3.zero;
            newStep.position = placeToBuild.position;
            Instantiate(stepParticle, placeToBuild.position, Quaternion.identity);

            transform.position += (Vector3) offset;
            if (_currentStepCount >= goalStepCount)
            {
                Destroy(gameObject);
            }
        }
    }
}