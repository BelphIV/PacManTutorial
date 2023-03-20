using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Ghost ghost;
    Pacman pacman;
    Movement pacmanMovement;
    Ghost blinky;

    Vector2 targetPos;

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.pacman = FindObjectOfType<Pacman>();
        this.pacmanMovement = pacman.gameObject.GetComponent<Movement>();
        
        Ghost[] ghosts = FindObjectsOfType<Ghost>();
        foreach (var item in ghosts)
        {
            if(item.ghostName == "Blinky")
            {
                this.blinky = item;
            }
        }
    }

    private void Update()
    {
        if(ghost.ghostName == "Blinky")
        {
            ghost.target = pacman.transform;
        }
        else if (ghost.ghostName == "Pinky")
        {
            targetPos = new Vector2(pacman.transform.position.x + (pacmanMovement.direction.x * 4), pacman.transform.position.y + (pacmanMovement.direction.y * 4));
            ghost.target.position = targetPos;
        }
        else if (ghost.ghostName == "Inky")
        {
            Vector3 direction = blinky.transform.position - pacman.transform.position;
            targetPos = pacman.transform.position - direction;
            ghost.target.position = targetPos;
        }
        else if (ghost.ghostName == "Clyde")
        {
            ghost.target = pacman.transform;
        }
    }
}
