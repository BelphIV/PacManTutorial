using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    int score = 200;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pacman"))
        {
            GameController gameController = FindObjectOfType<GameController>();
            gameController.SetScore(score);
            Destroy(gameObject);
        }
    }
}
