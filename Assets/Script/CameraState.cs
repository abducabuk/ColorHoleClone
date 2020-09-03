using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraState : Singleton<CameraState>
{
    [SerializeField]
    private List<Vector3> positions = new List<Vector3>();
    public int currentState;
    [SerializeField]
    private float duration = 1f;
    public void GoToPos(int index)
    {
        StartCoroutine(GoTo(index));
    }


    private IEnumerator GoTo(int index)
    {
        var startPos = transform.position;
        currentState = index % positions.Count;
        var destination = positions[currentState];

        var distance = destination - startPos;
        while ((transform.position - destination).magnitude > 3f)
        {
            transform.position += (distance * Time.deltaTime) / duration;
            yield return null;
        }
        transform.position = destination;

    }
    public void GoToNext() => GoToPos(currentState + 1);

    public void ShakeCamera(UnityAction endAction=null)
    {
        GetComponent<CameraShake>().ShakeCamera(endAction);
    }
}


