using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    
    public float size = 3.0f;
    public float minSize = 1.5f;
    public float maxSize = 4.0f;
    public float speed = 20.0f;
    public float maxLifetime = 30.0f;
    
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;

    private void Awake() 
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.enabled = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
        _spriteRenderer.material.renderQueue = 4000;
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
    }
    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if ((this.size * 0.6f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }
        
        }
        FindObjectOfType<GameManager>().AsteroidDestroyed(this);
        Destroy(this.gameObject);
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
