using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] 
    private bool enable = true;
    [SerializeField, Range(0, 0.1f)]
    private float amplitude = 0.015f;
    [SerializeField, Range(0, 30f)]
    private float frequency = 10f;
    [SerializeField]
    private Transform cam = null;
    [SerializeField]
    private Transform cameraRoot = null;

    private float toggleSpeed = 3.0f;
    private Vector3 startPos;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        startPos = cam.localPosition;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        ResetPosition();

        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }
    
    private void ResetPosition()
    {
        if (cam.localPosition == startPos) return;
        cam.localPosition = Vector3.Lerp(cam.localPosition, startPos, 1*Time.deltaTime);
    }

    void Start()
    {
        
    }
    void Update()
    {
        if (!enable) return;

        CheckMotion();
        cam.LookAt(FocusTarget());
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraRoot.localPosition.y, transform.position.z);
        pos += cameraRoot.forward * 15.0f;
        return pos;
    }

    private void PlayMotion(Vector3 motion)
    {
        cam.localPosition += motion;
    }
}
