using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaningController : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] Vector3 sidePos;
    [SerializeField] Vector3 newRotation;

    //Private Variables
    Vector3 defaultPos = Vector3.zero;
    Vector3 defaultRot;
    Vector3 currentPos;
    Vector3 currentRot;
    PlayerMovement playerMovement;

    private void Start()
    {
        defaultPos = transform.localPosition;
        defaultRot = transform.localEulerAngles;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            float clampedSidePos = Mathf.Clamp(sidePos.x, 0, RayCheck(-transform.right));
            currentPos = defaultPos - new Vector3(clampedSidePos, sidePos.y, sidePos.z);
            currentRot = defaultPos + newRotation * clampedSidePos / sidePos.x;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            float clampedSidePos = Mathf.Clamp(sidePos.x, 0, RayCheck(transform.right));
            currentPos = defaultPos + new Vector3(clampedSidePos, sidePos.y, sidePos.z);
            currentRot = defaultRot - newRotation * clampedSidePos / sidePos.x; ;
        }
        else
        {
            currentPos = defaultPos;
            currentRot = defaultRot;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * 3);
        Quaternion newRot = Quaternion.identity;
        newRot.eulerAngles = currentRot;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, newRot, Time.deltaTime * 3);
    }

    float RayCheck(Vector3 dir)
    {
        Ray ray = new Ray(transform.position, dir);
        // Debug.DrawRay(transform.position, dir, Color.green, 3);
        if (Physics.Raycast(ray, out RaycastHit hit, sidePos.x))
        {
            return Vector3.Distance(transform.position, hit.point);
        }
        return sidePos.x;
    }

    private void OnDrawGizmosSelected()
    {
        Ray ray1 = new Ray(transform.position, transform.right);
        Ray ray2 = new Ray(transform.position, -transform.right);
        Debug.DrawRay(transform.position, transform.right, Color.green);
        Debug.DrawRay(transform.position, -transform.right, Color.green);
        if (Physics.Raycast(ray1, out RaycastHit hit1, 10))
        { Gizmos.DrawSphere(hit1.point, .1f); }
        if (Physics.Raycast(ray2, out RaycastHit hit2, 10))
        { Gizmos.DrawSphere(hit2.point, .1f); }
    }

}
