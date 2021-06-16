using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    [SerializeField] private TeamColor currentTeam;
    public event Action<Vector2Int> OnStepPickUp;
    private Vector2Int _position;
    private BoxCollider _collider;
    private SphereCollider _pickUpCollider;
    private TrailRenderer _trail;


    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
        _trail.enabled = false;
        _pickUpCollider = GetComponent<SphereCollider>();
        _collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
    }

    public void SavePosition(Vector2Int pos)
    {
        _position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Backpack>()?.GetTeamColor() == currentTeam)
        {
            _trail.enabled = true;
            _collider.enabled = true;
            Destroy(_pickUpCollider);
            other.gameObject.GetComponent<Backpack>().StepPickUp(transform);

            OnStepPickUp?.Invoke(_position);
        }
    }

    public void DisableTrail()
    {
        _trail.enabled = false;
    }
}