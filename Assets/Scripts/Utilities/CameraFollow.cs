using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Date created: 10/20/2018
 * Creator: Unity
 * 
 * Description: makes the attached object follow the target.
 * Taken from a Unity tutorial.
 */
public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float smoothing = 5f;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}