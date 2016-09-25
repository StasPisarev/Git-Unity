using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovment : MonoBehaviour {

   
    [SerializeField] float speed = 1;
    [SerializeField] float torn = 1;
    [SerializeField] float gravity = 1;
 
    Animator playerAnimator;
    CharacterController controller;


    Vector3 moveDirection = Vector3.zero;

   
    void Start () {
        playerAnimator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

   
    void Update() {

        Move(new Vector2 (CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vettical")));

    }

    void Move(Vector2 movTarget) { // функция движения персонажа

        playerAnimator.SetFloat("speed", Mathf.Abs(movTarget.y)); 

        if (controller.isGrounded)
            {
                moveDirection = new Vector3(0f, 0f, movTarget.y);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;


            }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        // движения Charucter Controller  вперед- назад, при перемещении джойстика по оси y



        if (Mathf.Abs(movTarget.x) > 0) { 
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + movTarget.x * torn * Time.deltaTime, 0f);
            // повороты при перемещении джойстика по x
        }

    }


   
}
