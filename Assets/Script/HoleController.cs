using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    private string controllerPlaneTag = "Plane";

    private Collider coll;
    private bool active = true;

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 2f,0f);

    private void Awake()
    {
        coll = GetComponent<Collider>();
    }
    void FixedUpdate()
    {
        if (!active) return;

        if (!Input.GetKey(KeyCode.Mouse0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition+ offset);

        var hits =Physics.RaycastAll(ray);

        foreach (var hit in hits)
        {
            if (hit.transform.tag.Equals(controllerPlaneTag))
            {
                var pos = transform.position;
                pos.x = hit.point.x;
                pos.z = hit.point.z;
                transform.position = LimitPos(hit.collider,pos);
                break;
            }
        }
    }

    [SerializeField] private float padding = .5f;
    private Vector3 LimitPos(Collider areaCol,Vector3 pos)
    {
        var minX = areaCol.bounds.min.x + coll.bounds.size.x / 2f + padding;
        var minZ = areaCol.bounds.min.z + coll.bounds.size.z / 2f + padding;
        var maxX = areaCol.bounds.max.x - coll.bounds.size.x / 2f - padding;
        var maxZ = areaCol.bounds.max.z - coll.bounds.size.z / 2f - padding;
        pos.x = pos.x < minX ? minX : pos.x;
        pos.x = pos.x > maxX ? maxX : pos.x;
        pos.z = pos.z < minZ ? minZ : pos.z;
        pos.z = pos.z > maxZ ? maxZ : pos.z;
        return pos;
    }

    public void DisableController()
    {
        active = false;
    }

    public void EnableController()
    {
        active = true;

    }
}
