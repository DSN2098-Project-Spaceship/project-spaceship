using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFade : MonoBehaviour
{
    LineRenderer ren;
    Color myCol;
    private void Start()
    {
        ren = GetComponent<LineRenderer>();
        myCol = ren.startColor;
    }
    void Update()
    {
        myCol.a = Mathf.Lerp(myCol.a, 0, Time.deltaTime * 4);

        ren.startColor = myCol; ren.endColor = myCol;
    }
}
