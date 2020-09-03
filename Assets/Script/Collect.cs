using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField]
    private string collectableTag = "Collectable";
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals(collectableTag)) return;

        other.GetComponent<Collectable>().Collect();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.tag.Equals(collectableTag)) return;


    }
}
