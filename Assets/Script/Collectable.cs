using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CollectableType
{
    USEFUL,
    HARMFULL
}

[System.Serializable]
public class CollectableEvent : UnityEvent<Collectable> { }

public class Collectable : MonoBehaviour
{
    public CollectableType type;
    private string sectorTag = "Sector";

    public CollectableEvent OnCollected = new CollectableEvent();

    private Sector sector;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals(sectorTag)) return;
        sector = other.GetComponent<Sector>();
        sector.Register(this);

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.tag.Equals(sectorTag)) return;
        sector = other.GetComponent<Sector>();
        sector.UnRegister(this);

    }

    public void Collect()
    {

        OnCollected.Invoke(this);
        Destroy(gameObject);

#if UNITY_ANDROID || UNITY_IPHONE
        try
        {
            Handheld.Vibrate();
        }
        catch (System.Exception){}
#endif

    }

}
