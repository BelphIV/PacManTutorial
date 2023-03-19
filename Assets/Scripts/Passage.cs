using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
    [SerializeField] Transform connection;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 pos = collision.transform.position;
        pos.x = this.connection.position.x;
        pos.y = this.connection.position.y;
        collision.transform.position = pos;
    }
}
