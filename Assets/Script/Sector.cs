using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Sector : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnSectorCompleted = new UnityEvent();
    [SerializeField]
    private UnityEvent OnSectorFailed = new UnityEvent();
    [SerializeField]
    private int registerCount = 0;
    private List<Collectable> registeredCollectableList = new List<Collectable>();

    [SerializeField] private Sector otherSector;

    public void Register(Collectable collectable)
    {
        if (registeredCollectableList.Contains(collectable)) return;
        registeredCollectableList.Add(collectable);
        registerCount = registeredCollectableList.Count;
        collectable.OnCollected.AddListener(OnCollected);
    }
    public void UnRegister(Collectable collectable)
    {
        if (!registeredCollectableList.Contains(collectable)) return;
        registeredCollectableList.Remove(collectable);
        registerCount = registeredCollectableList.Count;
        collectable.OnCollected.RemoveListener(OnCollected);
    }

    private void OnCollected(Collectable collectable)
    {
        switch (collectable.type)
        {
            case CollectableType.USEFUL:
                if (!registeredCollectableList.Contains(collectable)) 
                    return;
                UnRegister(collectable);
                if (UsefullCount > 0)
                    return;
                SectorCompleted();
                break;
            case CollectableType.HARMFULL:
                if (!registeredCollectableList.Contains(collectable))
                    return;
                UnRegister(collectable);
                SectorFailed();
                break;
            default:
                break;
        }
        
    }

    private int UsefullCount => registeredCollectableList.Where(c => c.type.Equals(CollectableType.USEFUL)).Count();
    private int HarmfullCount => registeredCollectableList.Where(c => c.type.Equals(CollectableType.HARMFULL)).Count();


    private void SectorCompleted()
    {
        Debug.Log("SectorCompleted");
        OnSectorCompleted.Invoke();
    }

    public void SectorFailed()
    {
        Debug.Log("SectorFailed");
        OnSectorFailed.Invoke();
        ClearSector();
        otherSector?.ClearSector();


    }

    public void ClearSector()
    {
        Debug.Log("ClearSector");
        registeredCollectableList.ForEach(r => r.OnCollected.RemoveListener(OnCollected));
        registeredCollectableList.Clear();
        registerCount = 0;
    }
}
