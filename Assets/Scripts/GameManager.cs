using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    
    
    public float respawnTime = 3.0f;
    public float respTime = 3.0f;

    public int lives = 3;
    public int score = 0;

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if (asteroid.size < 6.0f) {
            score += 100;
        }else if (asteroid.size < 5.0f){
            score += 50;
        } else {
            score += 25;
        }
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;

        if (this.lives <= 0){
            GameOver();
        } else {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }
    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Respawn");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollision), respawnTime);
    }

    private void TurnOnCollision()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
         this.lives = 3;
         this.score = 0;
         Invoke(nameof(Respawn), this.respawnTime);
    }
}
