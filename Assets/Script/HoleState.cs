using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoleState : Singleton<HoleState>
{
    [SerializeField]
    private List<Vector3> positions = new List<Vector3>();
    [SerializeField]
    private int currentState;
    [SerializeField]
    private float duration = 1f;
    public void GoToPos(int index, UnityAction endAction = null)
    {
        StartCoroutine(GoTo(index, endAction));
    }

    private IEnumerator GoTo(int index, UnityAction endAction = null)
    {
        var startPos = transform.localPosition;
        currentState = index % positions.Count;
        var destination = index == -1 ? new Vector3(0, transform.position.y, transform.position.z) : positions[currentState];

        var distance = destination - startPos;
        while ((transform.localPosition - destination).magnitude > 2)
        {
            transform.localPosition += (distance * Time.deltaTime) / ((index==-1)?0.2f:duration);
            yield return null;
        }
        transform.localPosition = destination;
        endAction?.Invoke();
        endAction = null;

    }
    public void GoToNext(UnityAction endAction = null) => GoToPos(currentState + 1, endAction);
}
