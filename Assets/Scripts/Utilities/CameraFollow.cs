using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /*
    [HideInInspector] public Vector3 top_right_bounds;
    [HideInInspector] public Vector3 bottom_left_bounds;

    [HideInInspector] public Vector3 tR;
    [HideInInspector] public Vector3 bL;
    [HideInInspector] public Vector3 camera_dimensions = new Vector3(7f, 5f, 0f);

    public Transform target;
    [HideInInspector] public float smoothing = 5f;


    private Vector3 offset;
    private Transform main_cam_trans;

    private void Awake()
    {
        m_time_step_wait = new WaitForSeconds(m_time_step);
        main_cam_trans = transform.GetChild(0).gameObject.transform;
    }

    private void Start()
    {
        offset = transform.position - target.position;
        tR.x = top_right_bounds.x - camera_dimensions.x + offset.x;
        tR.y = top_right_bounds.y - camera_dimensions.y + offset.y;
        bL.x = bottom_left_bounds.x + camera_dimensions.x + offset.x;
        bL.y = bottom_left_bounds.y + camera_dimensions.y + offset.y;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        targetCamPos.x = Mathf.Clamp(targetCamPos.x, bL.x, tR.x);
        targetCamPos.y = Mathf.Clamp(targetCamPos.y, bL.y, tR.y);

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }*/

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