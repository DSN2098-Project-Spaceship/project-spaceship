using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isActive = true;
    [System.Serializable]
    enum DoorDir
    {
        x, y, z
    }
    [SerializeField] float openAmount;
    [SerializeField] DoorDir doorDir = DoorDir.z;
    [SerializeField] Transform doorL, doorR;
    [SerializeField] float openSpeed = 2;
    [SerializeField] List<string> allowedTags = new List<string>();
    [SerializeField] AudioClip doorOpen, doorClosed;
    private AudioSource doorSound;
    private Vector3 _doorLDefaultPos;
    private Vector3 _doorRDefaultPos;
    private bool _open;
    private bool _doubleDoor;
    private bool _openSoundPlayed;

    private void Start()
    {
        doorSound = GetComponent<AudioSource>();
        _doorLDefaultPos = doorL.localPosition;
        if (doorR != null)
        {
            _doorRDefaultPos = doorR.localPosition;
            _doubleDoor = true;
        }
    }

    private void Update()
    {
        switch (doorDir)
        {
            case DoorDir.z:
                doorL.localPosition = new Vector3(doorL.localPosition.x, doorL.localPosition.y,
                    Mathf.Lerp(doorL.localPosition.z, _doorLDefaultPos.z - (_open ? openAmount : 0), Time.deltaTime * openSpeed));
                if (_doubleDoor)
                    doorR.localPosition = new Vector3(doorR.localPosition.x, doorR.localPosition.y,
                        Mathf.Lerp(doorR.localPosition.z, _doorRDefaultPos.z + (_open ? openAmount : 0), Time.deltaTime * openSpeed));
                break;
            case DoorDir.x:
                doorL.localPosition = new Vector3(Mathf.Lerp(doorL.localPosition.x, _doorLDefaultPos.x - (_open ? openAmount : 0), Time.deltaTime * openSpeed),
                    doorL.localPosition.y, doorL.localPosition.z);
                if (_doubleDoor)
                    doorR.localPosition = new Vector3(Mathf.Lerp(doorR.localPosition.x, _doorRDefaultPos.x + (_open ? openAmount : 0), Time.deltaTime * openSpeed),
                        doorR.localPosition.y, doorR.localPosition.z);
                break;
            case DoorDir.y:
                doorL.localPosition = new Vector3(doorL.localPosition.x,
                    Mathf.Lerp(doorL.localPosition.y, _doorLDefaultPos.y + (_open ? openAmount : 0), Time.deltaTime * openSpeed),
                    doorL.localPosition.z);
                if (_doubleDoor)
                    doorR.localPosition = new Vector3(doorR.localPosition.x,
                        Mathf.Lerp(doorR.localPosition.y, _doorRDefaultPos.y + (_open ? openAmount : 0), Time.deltaTime * openSpeed),
                        doorR.localPosition.z);
                break;
        }
    }

    List<string> enteredObs = new List<string>();
    private void OnTriggerEnter(Collider other)
    {
        DebugLogger.Log(other.transform.name, 6);
        if (allowedTags.Contains(other.tag))
        {
            _open = true & isActive;
            enteredObs.Add(other.tag);
            if (!_openSoundPlayed)
            {
                doorSound.clip = doorOpen;
                doorSound.Play();
                _openSoundPlayed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enteredObs.Contains(other.tag)) { enteredObs.Remove(other.tag); }
        if (enteredObs.Count <= 0)
        {
            _open = false;
            if (_openSoundPlayed)
            {
                doorSound.clip = doorClosed;
                doorSound.Play();
                _openSoundPlayed = false;
            }
        }
    }
}

