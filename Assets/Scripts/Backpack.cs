using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Backpack : MonoBehaviour
{
    [SerializeField] private Transform backpackTransform;
    [SerializeField] private float distance = 1;
    [SerializeField] private float takingDuration = 1;
    [SerializeField] private TeamColor currentTeam;
    private float _currentHeight = 0;
    private float _offset = 1f;
    private Stack<Transform> stepBackpack = new Stack<Transform>();

    public Transform StepGetOut()
    {
        if (stepBackpack.Count > 0)
        {
            Transform next = stepBackpack.Pop();
            DOTween.KillAll();
            _currentHeight -= distance;
            return next;
        }

        return null;
    }

    public TeamColor GetTeamColor()
    {
        return currentTeam;
    }

    public void StepPickUp(Transform step)
    {
        step.parent = backpackTransform;
        step.DOLocalRotate(Vector3.zero, takingDuration);

        var animSequence = DOTween.Sequence();
        animSequence.Append(step.DOLocalMove(Vector3.zero + Vector3.up * _currentHeight / 2 + Vector3.back * _offset,
            takingDuration / 2));
        animSequence.Append(step.DOLocalMove(Vector3.zero + Vector3.up * _currentHeight, takingDuration / 2));

        _currentHeight += distance;
        stepBackpack.Push(step);
        StartCoroutine(TrailDisappear(step.GetComponent<Step>()));
    }

    private IEnumerator TrailDisappear(Step step)
    {
        yield return new WaitForSeconds(takingDuration);
        step.DisableTrail();
    }
}