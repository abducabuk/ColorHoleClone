using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : Singleton<Hole>
{
    [SerializeField]
    private string collectableTag = "Collectable";
    private Collider collider;
    [SerializeField]
    private float holePower = 5f;

    private bool active = true;

    private HoleController holeController;
    private HoleMagnet holeMagnet;


    private void Awake()
    {
        collider = GetComponent<Collider>();
        holeController = GetComponent<HoleController>();
        holeMagnet = GetComponentInChildren<HoleMagnet>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active) return;
        if (other.tag.Equals(collectableTag)) 
        {
            other.isTrigger = true;
            StartCoroutine(Vacum(other));
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (!active) return;

    }

    private IEnumerator Vacum(Collider other)
    {
        if (!other)
            yield break;
        var r = other.GetComponent<Rigidbody>();
        while (other && other.isTrigger)
        {
            var direction = transform.position - other.transform.position;
            r.AddForce(direction * holePower*Time.deltaTime);
            r.AddRelativeTorque(direction * holePower * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    public void DisableHole()
    {
        active = false;
        holeController.DisableController();
        holeMagnet.DisableHoleMagnet();
    }

    public void EnableHole()
    {
        active = true;
        holeController.EnableController();
        holeMagnet.EnableHoleMagnet();



    }
}
