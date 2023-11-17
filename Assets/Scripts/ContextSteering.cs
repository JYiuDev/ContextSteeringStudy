using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class ContextSteering : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    public int rayResolution;
    public float rayLength = 0.5f;
    public Transform target;
    private float[] dangerMap;
    private Vector2[] interestMap;
    private float rayIncrement;

    void Start()
    {
        rayIncrement = 360f/rayResolution;
        dangerMap = new float[rayResolution];
        interestMap = new Vector2[rayResolution];
    }

    // Update is called once per frame
    void Update()
    {
        //DangerMapGenerate();   
        InteresMapGenerate();
    }

    private void DangerMapGenerate()
    {
        for (int i = 0; i < dangerMap.Length; i++)
        {
            float rayAngle = i * rayIncrement * (Mathf.PI / 180f);
            Vector2 dir = new Vector2(Mathf.Cos(rayAngle), Mathf.Sin(rayAngle));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, rayLength);

            if(hit)
            {
                Debug.DrawRay((Vector2)transform.position, dir * hit.distance, Color.red);
            } else
            {
                Debug.DrawRay((Vector2)transform.position, dir * rayLength, Color.green);
            }
        }
    }

    private void InteresMapGenerate()
    {
        for (int i = 0; i < interestMap.Length; i++)
        {
            float rayAngle = i * rayIncrement * (Mathf.PI / 180f);
            Vector2 rayDir = new Vector2(Mathf.Cos(rayAngle), Mathf.Sin(rayAngle));

            Vector2 targetDir =(target.transform.position - transform.position).normalized;

            float rayMagnitude = Vector2.Dot(rayDir,targetDir);
            interestMap[i] = rayDir * rayMagnitude;

            Debug.DrawRay(transform.position, rayDir * rayMagnitude, Color.green);
        }

        
    }
}
