using System;
using UnityEngine;

public class DoRotation : MonoBehaviour
{
    [SerializeField] private float _cicleTime = 0.5f;

    [SerializeField] private Vector3 rotateAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        transform.Rotate(rotateAngle * Time.deltaTime);
    }
}