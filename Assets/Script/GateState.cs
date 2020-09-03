using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GateState : Singleton<GateState>
{

    [SerializeField]
    private List<Vector3> positions = new List<Vector3>();
    [SerializeField]
    private int currentState;
    [SerializeField]
    private float duration = 1f;

    public void GoToPos(int index, UnityAction endAction=null)
    {
        StartCoroutine(GoTo(index,endAction));
    }


    private IEnumerator GoTo(int index, UnityAction endAction = null)
    {
        var startPos = transform.localPosition;
        currentState = index % positions.Count;
        var destination = positions[currentState];

        var distance = destination - startPos;
        while ((transform.localPosition - destination).magnitude > 0.5f)
        {
            transform.localPosition += (distance * Time.deltaTime) / duration;
            yield return null;
        }
        transform.localPosition = destination;
        endAction?.Invoke();
        endAction = null;


    }
    public void GoToNext(UnityAction endAction = null) => GoToPos(currentState + 1,endAction);

    public void Open(UnityAction endAction = null) => GoToPos(1, endAction);

    public void Close(UnityAction endAction = null) => GoToPos(0, endAction);
}
