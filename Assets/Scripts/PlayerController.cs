using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class PlayerController : MonoBehaviour
{
   [Header("Player Control Data")] 
   InputController inputController;
   Vector2 movemenInput;
   Vector2 cameraInput;

   public InputElement northInput = new InputElement();

   [Header("Player Camera Data")] 
   public Transform cameraSystem;
   public Transform cameraPivot;
   public Transform camera;
   public Transform cameraFollowTarget;
   [Range(0, 10)] 
   public float cameraFollowSpeed;
   [Range(0, 10)] 
   public float cameraRotatedSpeed;
   public float cameraMaxAngle;
   public float cameraMinAngle;
   public Vector2 cameraAngles;
   
   [Header("Player Movement Data")] 
   public CharacterController controller;
   public Vector3 moveDirection;
   [Range(0, 10)] 
   public float rotatioSpeed;
   [Range(0,10)]
   public float movementSpeed;
   public bool isThirdPerson;

   public void OnEnable()
   {
      
      if (inputController == null)
      {  
         inputController = new InputController();  
         //the movement of object
         inputController.Movement.Move.performed += 
            inputController => movemenInput = inputController.ReadValue<Vector2>();
         //the movement of camera
         inputController.Movement.Camera.performed +=
            inputController => movemenInput = inputController.ReadValue<Vector2>();
         
         //the start view
         inputController.Actions.ChangeView.started += inputController => northInput.risingEdge = true;
         inputController.Actions.ChangeView.performed += inputController => northInput.longPress = true;
         inputController.Actions.ChangeView.canceled += inputController => northInput.releaseEdges();
      }
      inputController.Enable();
   }

   public void OnDisable()
   {
      inputController.Disable();
   }

   private void Update()
   {
      HandleMovement();
      MovementRotation();
     
      //TODO:
      
      CameraChangeView();
      CameraMovement();
   }

   private void LateUpdate()
   {
      northInput.resetEdge();
   }

   Vector3 normalVector=Vector3.up;
   private void HandleMovement()
   {
      moveDirection = camera.forward * movemenInput.y;
      moveDirection += camera.right * movemenInput.x;
      Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
      projectedVelocity.Normalize();
      projectedVelocity *= movementSpeed;
      controller.Move(projectedVelocity * Time.deltaTime);
   }

   private void MovementRotation()
   {
      Vector3 targetDirection=Vector3.zero;
      if (isThirdPerson)
      {
         targetDirection = camera.forward * movemenInput.y;
         targetDirection = camera.right * movemenInput.y;
      }
      else
      {
         targetDirection = camera.forward;
      }

      if (targetDirection == Vector3.zero)
      {
         targetDirection = transform.forward;
      }

      Vector3 projectedDirection =Vector3.ProjectOnPlane(targetDirection,normalVector);
      projectedDirection.Normalize();

      Quaternion targerDir = Quaternion.LookRotation(projectedDirection);
      Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, targerDir, rotatioSpeed * Time.deltaTime);
      transform.rotation = smoothRotation;
   }

   private void CameraChangeView()
   {
      
   }

   private void CameraMovement()
   {
      cameraSystem.position = Vector3.Lerp(cameraSystem.position, cameraFollowTarget.position,
         Time.deltaTime * cameraFollowSpeed);
   }

   // Start is called before the first frame update
   void Start()
   {
        
   }

}
