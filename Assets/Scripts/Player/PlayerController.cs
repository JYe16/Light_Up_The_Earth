using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;

public class PlayerController : MonoBehaviour
{
   [Header("Player Control Data")] 
   InputController inputController;
   Vector2 movementInput;
   Vector2 cameraInput;
   private float hor;
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
   public CharacterController SpaceShipcontroller;
   public Vector3 moveDirection;
   [Range(0, 10)] 
   public float rotationSpeed;
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
            inputController => movementInput = inputController.ReadValue<Vector2>();
         //the movement of camera
         inputController.Movement.Camera.performed +=
            inputController => cameraInput = inputController.ReadValue<Vector2>();

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
      
      
     CameraChangeView();
     CameraMovement();
   }
   private void LateUpdate()
   {
      northInput.resetEdge();
   }

   
   
   
   Vector3 normalVector=Vector3.up;
   //character movement function
   private void HandleMovement()
   {
      moveDirection = camera.forward * movementInput.y;
      moveDirection += camera.right * movementInput.x;
      moveDirection.y = 0;
      Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
      projectedVelocity.Normalize();
      projectedVelocity *= movementSpeed;
      SpaceShipcontroller.Move(projectedVelocity * Time.deltaTime*5f);
   }

   private void MovementRotation()
   {
      Vector3 targetDir=Vector3.zero;
      if (isThirdPerson)
      {
         targetDir = camera.forward * movementInput.y;
         targetDir += camera.right * movementInput.x;
      }else
      {
         targetDir = camera.forward;
      }

      if (targetDir == Vector3.zero)
      {
         targetDir = transform.forward;
      }

      Vector3 projectedDirection =Vector3.ProjectOnPlane(targetDir,normalVector);
      projectedDirection.Normalize();

      

      Quaternion targetDirection = Quaternion.LookRotation(projectedDirection);
      Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, targetDirection, rotationSpeed * Time.deltaTime);
      transform.rotation = smoothRotation;
   }

   private void CameraChangeView()
   {
      if (northInput.longPress)
      {
         isThirdPerson = true;
         Vector3 newPosition=new Vector3(0,10,-300);
         cameraPivot.localPosition =
            Vector3.Lerp(cameraPivot.localPosition,  newPosition, Time.deltaTime * cameraFollowSpeed*2f);
      }
      else
      {
         isThirdPerson = false;
         Vector3 newPosition = Vector3.zero;
         cameraPivot.localPosition =
            Vector3.Lerp(cameraPivot.localPosition, newPosition, Time.deltaTime * cameraFollowSpeed*2f);
      }
   }

   private void CameraMovement()
   {
      cameraSystem.position = Vector3.Lerp(cameraSystem.position, cameraFollowTarget.position,
         Time.deltaTime * cameraFollowSpeed);
      cameraAngles.x += (cameraInput.x * cameraFollowSpeed) * Time.fixedDeltaTime;
      cameraAngles.y += (cameraInput.y * cameraFollowSpeed) * Time.fixedDeltaTime;
      if (isThirdPerson)
      {
         cameraAngles.y = Mathf.Clamp(cameraAngles.y, cameraMinAngle, cameraMaxAngle);
      }
      else
      {
         cameraAngles.y = Mathf.Clamp(cameraAngles.y, cameraMinAngle * 1.5f, cameraMaxAngle * 1.5f);
      }

      Vector3 rotation = Vector3.zero;
      rotation.x = cameraAngles.x;
      cameraSystem.rotation = Quaternion.Euler(rotation);
      
      rotation = Vector3.zero;
      rotation.y = cameraAngles.y;
      cameraPivot.localRotation = Quaternion.Euler(rotation);
   }


   // Start is called before the first frame update
   void Start()
   {
      
   }
   
}