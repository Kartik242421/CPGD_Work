using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] float aimRange=100f;
    [SerializeField] LayerMask aimMask;

    public GameObject GetAimTarget()
    {
        Vector3 aimStart = muzzle.position;
        
        //              fromhere, this direction,    ,         ,length , 
        if(Physics.Raycast(aimStart, GetAimDir(), out RaycastHit hitInfo,aimRange, aimMask))
        {
            return hitInfo.collider.gameObject;
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(muzzle.position, muzzle.position + GetAimDir() * aimRange);
    }

    Vector3 GetAimDir()
    {
        Vector3 muzzleDir = muzzle.forward;
        return new Vector3(muzzleDir.x, 0f, muzzleDir.z).normalized;
    }
}
