using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSort : MonoBehaviour
{
    [System.Serializable]
    class CoversClass
    {
        public List<Transform> allCovers = new List<Transform>();
    }

    [System.Serializable]
    class ZonesClass
    {
        public List<CoversClass> allZones = new List<CoversClass>();
    }
    //Serialized
    [SerializeField] ZonesClass allCover = new ZonesClass();
    [SerializeField] List<Transform> enemies = new List<Transform>();
    [SerializeField] float checkDelay;
    [SerializeField] LayerMask obsMask;
    int currentZone;
    bool isDisabled;
    Transform player;
    List<Transform> eligibleCover = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MainMethod();
    }

    private void MainMethod()
    {
        //Raycast from cover to player to see if it's eligible to be used
        // if (isDisabled) return;
        for (int i = 0; i < allCover.allZones[currentZone].allCovers.Count; i++)    //For all cover posses in the current zone
        {
            RaycastHit hit;
            //If there is an obstacle between the cover position and player, add it to the list of eligible positions (if not already present)
            if (Physics.Raycast(allCover.allZones[currentZone].allCovers[i].position, (player.transform.position - allCover.allZones[currentZone].allCovers[i].position), 100f, obsMask))
            {
                if (!eligibleCover.Contains(allCover.allZones[currentZone].allCovers[i]))
                    eligibleCover.Add(allCover.allZones[currentZone].allCovers[i]);  //Add eligible cover to a list of eligible covers
            }
            //If it there is no obj between the player and cover pos, and it is present in the eligible cover list, remove it. 
            else if (eligibleCover.Contains(allCover.allZones[currentZone].allCovers[i]))
            {
                eligibleCover.Remove(allCover.allZones[currentZone].allCovers[i]);
            }
        }

        //Sort enemies and cover list baseed on distance to player
        enemies.Sort((x, y) => { return (player.transform.position - x.transform.position).sqrMagnitude.CompareTo((player.transform.position - y.transform.position).sqrMagnitude); });
        eligibleCover.Sort((x, y) => { return (player.transform.position - x.position).sqrMagnitude.CompareTo((player.transform.position - y.position).sqrMagnitude); });

        //Update AI's internal cover pos (not yet implemented)
        for (int i = 0; i < enemies.Count && i < eligibleCover.Count; i++)
        {
            enemies[i].transform.position = eligibleCover[i].position;
            // state = enemies[i].GetComponent<ZombieAI>();
            // //Debug.Log(state.isDead);
            // if (!state.isDead)
            // {
            //     state.coverLocation = eligibleCover[i];
            //     if (eligibleCover[i].CompareTag("Aside"))
            //     {
            //         state.isStepAside = true;
            //         state.asidePos = eligibleCover[i].GetChild(0);
            //     }
            //     else
            //     {
            //         state.isStepAside = false;
            //         state.asidePos = null;
            //     }
            // }
        }

        // eligibleCover.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        if (allCover.allZones[currentZone].allCovers.Count <= 0 || player == null || allCover.allZones.Count <= 0) return;
        foreach (Transform c in allCover.allZones[currentZone].allCovers)
        {
            if (eligibleCover.Contains(c))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(c.position, .5f);
                Debug.DrawLine(c.position, player.position, Color.green);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(c.position, .5f);
                Debug.DrawLine(c.position, player.position, Color.red);
            }
        }
    }

    public void ChangeZone(int zoneNum)
    {
        currentZone = zoneNum;
        eligibleCover.Clear();
    }
}
