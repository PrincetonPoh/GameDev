using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public float speed;
    private bool collision;
    private bool start = false;
    private bool movingRight = true;
    private Rigidbody2D mushroomBody;
    private bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomBody.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        start = true;
        
    }

    void FixedUpdate()
    {
        if (!start || collision) return;
        var velocity = mushroomBody.velocity;
        velocity = movingRight ? new Vector2(speed, velocity.y) : new Vector2(-speed, velocity.y);
        print("Deeznuts");
        mushroomBody.velocity = velocity;
    }

    void OnBecameInvisible(){
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other){
        if (!collision && other.gameObject.CompareTag("Player")){
            print("HIT");
            collision = true;
            mushroomBody.velocity = Vector2.zero;
            return;
        }
        var dir = other.GetContact(0).normal;
        if (dir==Vector2.up) return;
        if (other.gameObject.CompareTag("Obstacle")){
            movingRight = !movingRight;
        }
    }
}