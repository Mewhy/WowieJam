using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{
    public bool flipped;
    public bool jumped;
    private float movementSpeed = 0;
    public float walkSpeed = 1;
    public float runSpeed = 2;
    public float jumpSpeed = 200;
    public GameObject character;
    public Rigidbody2D characterRig;

    private void Start(){
        characterRig = character.GetComponent<Rigidbody2D>();
    }

    void Update() {
        //Movement
        characterRig.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, characterRig.velocity.y);
        //Jumping
        if (Input.GetAxis("Jump") > 0.1 && jumped == false) {
            if (flipped){
                characterRig.AddForce(new Vector2(0, jumpSpeed * -1));
            }
            else {
                characterRig.AddForce(new Vector2(0, jumpSpeed));
            }
            jumped = true;
        }
        //Running
        if (Input.GetAxis("Fire3") > 0.1) {
            movementSpeed = runSpeed;
        }
        else {
            movementSpeed = walkSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        jumped = false;
    }
}
