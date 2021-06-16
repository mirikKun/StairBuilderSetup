using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StepSpawner : MonoBehaviour
{
    [SerializeField] private Step[] stepPrefabs;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Vector2Int size;
    [SerializeField] private Vector2 distance;
    [SerializeField] private float timeToRespawn = 3;
    private Vector3 _startPosition;
    private List<Step> _steps;
    private int _teamCount;

    void Awake()
    {
        _teamCount = stepPrefabs.Length;
        _startPosition = startPoint.position;
        _steps = new List<Step>();
        StepSpawn();
    }

    private void StepSpawn()
    {
        for (int i = 0; i < size.x; i++)
        for (int j = 0; j < size.y; j++)
        {
            Vector3 newPos = new Vector3(_startPosition.x + i * distance.x, _startPosition.y,
                _startPosition.z + j * distance.y);
            Step newStep = Instantiate(stepPrefabs[Random.Range(0, _teamCount)], newPos, Quaternion.identity,
                startPoint);
            newStep.SavePosition(new Vector2Int(i, j));
            _steps.Add(newStep);
        }
    }

    private void StepGone(Vector2Int pos)
    {
        _steps.Add(_steps[pos.x * size.x + pos.y]);
        _steps[pos.x * size.x + pos.y] = null;
        StartCoroutine(WaitToRespawn(pos.x, pos.y));
    }

    private IEnumerator WaitToRespawn(int i, int j)
    {
        yield return new WaitForSeconds(timeToRespawn);
        Vector3 newPos = new Vector3(_startPosition.x + i * distance.x, _startPosition.y,
            _startPosition.z + j * distance.y);
        Step newStep = Instantiate(stepPrefabs[Random.Range(0, _teamCount)], newPos, Quaternion.identity, startPoint);
        newStep.SavePosition(new Vector2Int(i, j));
        _steps[i * size.x + j] = newStep;
        _steps[i * size.x + j].OnStepPickUp += StepGone;
    }

    private void OnEnable()
    {
        _steps.ForEach(step => step.OnStepPickUp += StepGone);
    }

    private void OnDisable()
    {
        _steps.ForEach(step => step.OnStepPickUp -= StepGone);
    }
}