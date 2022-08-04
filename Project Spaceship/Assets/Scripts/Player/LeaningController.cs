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

    private void Start()
    {
        defaultPos = transform.localPosition;
        defaultRot = transform.localEulerAngles;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            currentPos = defaultPos - sidePos;
            currentRot = defaultPos + newRotation;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            currentPos = defaultPos + sidePos;
            currentRot = defaultRot - newRotation;
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
}
