using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BaseController))]
public class Smart : MonoBehaviour
{
    [System.Serializable]
    public enum EnemyState
    {
        shooting,
        covering,
        strafing
    }
    public EnemyState state = EnemyState.covering;
    BaseController _MyController;
    public Vector3 myCoverPos = Vector3.zero;
    ZoneController currentZone;
    List<Vector3> strafePath;
    int currStrafIndex = 0;
    bool strafeSetupDone = false;
    private void Awake()
    {
        _MyController = GetComponent<BaseController>();
    }

    private void Update()
    {
        switch (state)
        {
            case EnemyState.covering:
                Covering();
                break;
            case EnemyState.strafing:
                Strafing();
                break;
            case EnemyState.shooting:
                break;
        }
    }

    void Covering()
    {
        _MyController.GoToPosition(myCoverPos, 0, 5);
    }

    void StartStrafe()
    {
        ZoneController[] zns = FindObjectsOfType<ZoneController>();
        List<ZoneController> pickZone = new List<ZoneController>();
        foreach (ZoneController z in zns)
        {
            if (!z.amActive) pickZone.Add(z);
        }
        currentZone = pickZone[Random.Range(0, pickZone.Count)];
        strafePath = currentZone.ProvidePoints();

        strafeSetupDone = true;
    }

    void Strafing()
    {
        if (!strafeSetupDone)
        {
            StartStrafe();
            return;
        }
        _MyController.GoToPosition(strafePath[currStrafIndex], 0, 4);
        if (_MyController.RemainingDistance() <= 0.2f)
        {
            currStrafIndex++;
            if (currStrafIndex == strafePath.Count)
            {
                state = EnemyState.shooting;
                strafeSetupDone = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
