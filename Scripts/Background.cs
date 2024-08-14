using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Transform folowingTarget;
    [SerializeField, Range(0f, 1f)] float ParalaxSettings = 0.1f;
    [SerializeField] bool DisableVerticalParalax;
    Vector3 targetPreviousPosition;
    public Camera main;


    void Start()
    {

        if (!folowingTarget)
        {
            folowingTarget = main.transform;
        }

        targetPreviousPosition = folowingTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        var delta = targetPreviousPosition = folowingTarget.position - targetPreviousPosition;

        if (DisableVerticalParalax)
            delta.y = 0;

        targetPreviousPosition = folowingTarget.position;

        transform.position += delta * ParalaxSettings;
    }
}