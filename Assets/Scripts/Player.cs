using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour
{
    public int PlayerId = 1;
    Controller2D controller;
    Vector3 velocity;

    float moveSpeed = 6;
    float gravity; // 2*jumpHeight/(timetojumpapex squared)
    float maxJumpVelocity; // gravity * timetoJumpApex
    float minJumpVelocity;
    float velocityXSmoothing;
    float accelTimeAirborne = .2f;
    float accelTimeGrounded = .1f;

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller2D>();
        gravity = - (2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2* Mathf.Abs(gravity)*minJumpHeight);
    }

    // Update is called once per frame
    void Update()
    {


        if (controller.collisions.above || controller.collisions.below){
            velocity.y = 0;
        }
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(controller.collisions.below) {
                velocity.y = maxJumpVelocity;
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(velocity.y > minJumpVelocity)
            velocity.y = minJumpVelocity;
        }

        if(Camera.main.transform.position.y > controller.transform.position.y + 35)
        {
            //need to hold Main Camera X
        }
        if ((Camera.main.transform.position.y > controller.transform.position.y + 50) || (this.GetComponent<Health>().health <= 0) )
        {
            Destroy(controller.gameObject);
            //Need to transition to Game Over scene instead.. 
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x =Mathf.SmoothDamp(velocity.x, targetVelocityX,ref velocityXSmoothing,(controller.collisions.below)?accelTimeGrounded:accelTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Fire();
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("We hit " + collision.collider.name);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision with enemy");
            this.GetComponent<Health>().loseHealth();
        }
    }

}
