using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    // frame p frame, acomp c�mera
    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); // acompanha o player
    }
}