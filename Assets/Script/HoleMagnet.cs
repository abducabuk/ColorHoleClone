using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMagnet : MonoBehaviour
{
    [SerializeField]
    private string collectableTag = "Collectable";
    private Collider collider;
    [SerializeField]
    private float holePower = 5f;

    private bool active = true;

    private HoleController holeController;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active) return;
        if (other.tag.Equals(collectableTag))
        {
            StartCoroutine(Vacum(other));
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (!active) return;
    }
    private float thresh = 10f;
    private IEnumerator Vacum(Collider other)
    {
        if (!other)
            yield break;
        var r = other.GetComponent<Rigidbody>();
        while (other )
        {
            var direction = transform.position - other.transform.position;
            if (direction.magnitude > thresh) break;
            r.AddForce((direction * (holePower * Time.deltaTime)) / direction.magnitude);
            yield return new WaitForEndOfFrame();
        }
    }

    public void DisableHoleMagnet()
    {
        active = false;
    }

    public void EnableHoleMagnet()
    {
        active = true;
    }
}
