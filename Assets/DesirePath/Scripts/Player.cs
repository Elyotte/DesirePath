using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    CharacterController bodyController;
    PlayerInput playerInput;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform renderer;

    float moveSpeed = 5f;

    // Inputs
    string moveInputsName = "Move";

    private void Awake()
    {
        bodyController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

    }


    private void Update()
    {
        Vector2 lMoveInput = playerInput.actions[moveInputsName].ReadValue<Vector2>();
        Vector3 to3DMovevement = new Vector3(lMoveInput.x, 0, lMoveInput.y);

        if (cameraTransform != null)
        {
            to3DMovevement = (cameraTransform.forward * lMoveInput.y) + (cameraTransform.right * lMoveInput.x);
            to3DMovevement.y = 0;
        }

        if (lMoveInput!= Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(to3DMovevement.normalized, Vector3.up);
            renderer.rotation = Quaternion.Lerp(renderer.rotation, targetRotation, .2f);
        }
        
        bodyController.Move(to3DMovevement.normalized * moveSpeed * Time.deltaTime);

    }

}
