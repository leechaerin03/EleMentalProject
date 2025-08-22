using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby_Camera : MonoBehaviour
{
    private float xRotateMove;
    public float rotateSpeed = 30.0f;

    // Update is called once per frame
    void Update()
    {
        xRotateMove = Time.deltaTime * rotateSpeed;

        Vector3 stagePosition = transform.position;

        transform.RotateAround(stagePosition, Vector3.up, xRotateMove);

        transform.LookAt(stagePosition);

    }
}
