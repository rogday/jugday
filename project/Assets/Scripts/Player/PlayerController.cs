using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    public float jumpHeight;
    public float gravity;

    float oldHeight = -1_000_000;

    float velocityY = 0.0f;
    bool jumped = false;
    bool wishJump = false;
    
    CharacterController controller;
    Transform cameraT;

    // Start is called before the first frame update
    void Start(){
        transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 90, Random.Range(-10.0f, 10.0f));
        controller = GetComponent<CharacterController>();
        cameraT = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (inputDirection != Vector2.zero)
            transform.eulerAngles = Vector3.up * (Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y);

        bool running = Input.GetKey(KeyCode.E);
        float speed = ((running) ? runSpeed : walkSpeed) * inputDirection.magnitude;

        if (transform.position.y - oldHeight < jumpHeight && jumped) {
            velocityY += Time.deltaTime * - gravity * 1.5f;
        } else {
            oldHeight = -1_000_000;
            velocityY += Time.deltaTime * gravity;
        }

        Vector3 velocity = transform.forward * speed + Vector3.up * velocityY;

        controller.Move(velocity*Time.deltaTime);

        if (controller.isGrounded) {
            jumped = false;
            velocityY = 0.0f;
            if (wishJump)
                Jump();
            wishJump = false;
        }
    }

    void Jump() {
        if (!controller.isGrounded) {
            wishJump = true;
            return;
        }

        oldHeight = transform.position.y;
        jumped = true;
    }

}
