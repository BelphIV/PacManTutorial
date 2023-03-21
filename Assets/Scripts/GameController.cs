
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }

    bool isGameOver;

    [Space]
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;


    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if(isGameOver && Input.anyKeyDown){NewGame();}
        scoreText.text = score.ToString();
    }

    void NewGame()
    {

        isGameOver = false;
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    void NewRound()
    {
        foreach (Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }

    void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);

        isGameOver = true;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives ;
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.pacman.gameObject.SetActive(false);
        this.pacman.gameObject.transform.parent.transform.GetChild(1).gameObject.SetActive(true);
        this.pacman.gameObject.transform.parent.transform.GetChild(1).transform.position = this.pacman.gameObject.transform.position;

        SetLives(this.lives - 1);

        if(this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        SetScore(this.score + pellet.point);
        pellet.gameObject.SetActive(false);
        if (!HasPellet())
        {
            this.pacman.gameObject.SetActive(false);
            this.pacman.gameObject.transform.parent.transform.GetChild(2).gameObject.SetActive(true);
            this.pacman.gameObject.transform.parent.transform.GetChild(2).transform.position = this.pacman.gameObject.transform.position;
            Invoke(nameof(NewRound), 3.0f);

        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);

    }

    bool HasPellet()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }

    
}
