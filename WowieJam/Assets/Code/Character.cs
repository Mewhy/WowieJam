using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Movement Variables
    public bool flipped;
    public bool jumped;
    private float movementSpeed = 0;
    public float walkSpeed = 1;
    public float runSpeed = 2;
    public float jumpSpeed = 200;
    public float cameraDistance = 10;
    public float distanceFromPlayer;

    //Game Objects and Components
    public GameObject character;
    private Rigidbody2D characterRig;
    public GameObject mainCamera;
    public GameObject respawn;

    private void Start() {
        characterRig = character.GetComponent<Rigidbody2D>();
    }

    void Update() {
        //Character Movement
        characterRig.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, characterRig.velocity.y);

        //Camera Movement
        Vector3 charPos = character.transform.position;
        Vector3 camPos = mainCamera.transform.position;
        charPos.z = -10;

        //Compiling all distances into one number
        distanceFromPlayer = 0;
        if ((camPos.x - charPos.x) >= cameraDistance)
        {
            distanceFromPlayer = distanceFromPlayer + (camPos.x - charPos.x) - cameraDistance;
        }
        if ((camPos.x - charPos.x) <= -cameraDistance)
        {
            distanceFromPlayer = distanceFromPlayer + -(camPos.x - charPos.x) - cameraDistance;
        }
        if ((camPos.y - charPos.y) >= cameraDistance)
        {
            distanceFromPlayer = distanceFromPlayer + (camPos.y - charPos.y) - cameraDistance;
        }
        if ((camPos.y - charPos.y) <= -cameraDistance)
        {
            distanceFromPlayer = distanceFromPlayer + -(camPos.y - charPos.y) - cameraDistance;
        }

        //Lerping that number
        if (distanceFromPlayer != 0)
        {
            mainCamera.transform.position = Vector3.Lerp(camPos, charPos, distanceFromPlayer * Time.deltaTime);
        }

        //Jumping
        if (Input.GetAxis("Jump") > 0.1 && jumped == false)
        {
            if (flipped)
            {
                characterRig.AddForce(new Vector2(0, jumpSpeed * -1));
            }
            else
            {
                characterRig.AddForce(new Vector2(0, jumpSpeed));
            }
            jumped = true;
        }
        //Running
        if (Input.GetAxis("Fire3") > 0.1)
        {
            movementSpeed = runSpeed;
        }
        else
        {
            movementSpeed = walkSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Untagged")
        {
            jumped = false;
        }
        if (col.gameObject.tag == "Flip")
        {
            Flip();
        }
        if (col.gameObject.tag == "Death")
        {
            Respawn();
        }
    }

    void Flip() {
        if (flipped)
        {
            flipped = false;
            characterRig.gravityScale = 1;
        }
        else
        {
            flipped = true;
            characterRig.gravityScale = -1;
        }

    }
    void Respawn() {
        character.transform.position = respawn.transform.position;
    }
}