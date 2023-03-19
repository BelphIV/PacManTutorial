using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    public Tilemap dotTilemap;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the user
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction based on the input
        moveDirection = new Vector2(xInput, yInput).normalized;
    }

    void FixedUpdate()
    {
        // Move the player based on the movement direction and move speed
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dot"))
        {
            // Get the position of the dot tile
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3Int cellPosition = dotTilemap.WorldToCell(contactPoint);
            TileBase tile = dotTilemap.GetTile(cellPosition);
            Debug.Log(tile);
            //if (tile != null)
            //{
            //    // Destroy the dot tile
            //    dotTilemap.SetTile(cellPosition, null);

            //    // Refresh the tilemap to update its contents
            //    dotTilemap.RefreshAllTiles();
            //}
        }
    }
}
