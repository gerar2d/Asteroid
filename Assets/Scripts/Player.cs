using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefeb;

    public float thrustSpeed = 3.0f;
    public float turnSpeed = 0.1f;
    
    private Rigidbody2D _ridgedbody;
    private SpriteRenderer _spriteRenderer;
    private bool _thrusting;
    private float _turnDirection;
    
    private void Awake()
    {
        _ridgedbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    
    private void Start()
    {
        _spriteRenderer.material.renderQueue = 8000;
    }
    
    private void Update()
    {
        _thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            _turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            _turnDirection = -1.0f;
        } else {
            _turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Shoot();
        }

    }

    private void FixedUpdate()
    {
        if (_thrusting){
            _ridgedbody.AddForce(this.transform.up * this.thrustSpeed);
        }
        if (_turnDirection  != 0.0f){
           _ridgedbody.AddTorque(_turnDirection * turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefeb, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid"){
            
            _ridgedbody.velocity = Vector3.zero;
            _ridgedbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }

}