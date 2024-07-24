// Decompiled with JetBrains decompiler
// Type: DroneController.CameraMovement.CameraScript
// Assembly: DroneControllerPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F0498C7-D6E3-4EA5-898B-1106F4644D80
// Assembly location: C:\Project Unity\New Unity Project\New Unity Project\Assets\DroneControllerPlugin.dll

using DroneController.Physics;
using System;
using System.Collections;
using UnityEngine;

namespace DroneController.CameraMovement
{
  public class CameraScript : MonoBehaviour
  {
   
    public int inputEditorFPS;
   
    public bool FPS;
    
    public LayerMask fpsViewMask;
   
    public Vector3 positionInsideDrone;
   
    public Vector3 rotationInsideDrone;
   
    public float fpsFieldOfView = 90f;
  
    public GameObject[] dronesToControl;
 
    public bool pickedMyDrone = false;
   
    public GameObject ourDrone;
    [Header("Position of the camera behind the drone.")]
   
    public Vector3 positionBehindDrone = new Vector3(0.0f, 2f, -4f);
    [Tooltip("How fast the camera will follow drone position. (The lower the value the faster it will follow)")]
    [Range(0.0f, 0.1f)]

    public float cameraFollowPositionTime = 0.1f;
    [Tooltip("Value where if the camera/drone is moving upwards will raise the camera view upward to get a better look at what is above, same goes when going downwards.")]
  
    public float extraTilt = 10f;
    [Tooltip("Parts of drone we wish to see in the third person.")]
 
    public LayerMask tpsLayerMask;
 
    public float tpsFieldOfView = 60f;
    [Header("Mouse movement variables")]
    [Tooltip("Allows to freely view around the drone with your mouse and not depending on drone look rotation.")]
  
    public bool freeMouseMovement = false;
  
    public bool useJoystickFreeMovementOnly = false;
   
    public string mouse_X_axisName = "Mouse X";
   
    public string mouse_Y_axisName = "Mouse Y";
  
    public string dPad_X_axisName = "dPad_X";
 
    public string dPad_Y_axisName = "dPad_Y";
    [Tooltip("Value that will determine how fast your free look mouse will behave.")]

    public float mouseSensitvity = 100f;
    [Tooltip("Value that will follow the camera view behind the mouse movement.(The lower the value, the faster it will follow mouse movement)")]

    public float mouseFollowTime = 0.2f;
    private int counterToControl = 0;
    private Vector3 velocitiCameraFollow;
    private float cameraYVelocity;
    private float previousFramePos;
    private float currentXPos;
    private float currentYPos;
    private float xVelocity;
    private float yVelocity;
    private float mouseXwanted;
    private float mouseYwanted;
    private float zScrollAmountSensitivity = 1f;
    private float yScrollAmountSensitivity = -0.5f;
    private float zScrollValue;
    private float yScrollValue;
    [Header("Canvas drone selection UI variables")]
    [Tooltip("UI Canvas buttons that are used to select wanting drone.")]
    public GameObject[] canvasSelectButtons;
    [Tooltip("UIU Canvas button that is used to exit the current drone selection.")]
    public GameObject[] canvasExitButtons;
    [Tooltip("Track timer canvas")]
    public GameObject canvasTimeTrack;

    public virtual void Awake() => this.Drone_Pick_Initialization();

    private void Drone_Pick_Initialization()
    {
      this.pickedMyDrone = false;
      this.ourDrone = (GameObject) null;
      this.dronesToControl = GameObject.FindGameObjectsWithTag("Player");
      if (this.dronesToControl.Length > 1)
      {
        this.pickedMyDrone = false;
        this.ourDrone = this.dronesToControl[this.counterToControl].gameObject;
      }
      else
        this.StartCoroutine(this.KeepTryingToFindOurDrone());
    }

    private void FPSCameraPositioning()
    {
      if (transform.parent == null)
        ((Component) this).transform.SetParent(((Component) this.ourDrone.GetComponent<DroneMovementScript>().animatedGameObject).transform);
            Camera thisCamera = ((Component)this).GetComponent<Camera>();

            if (thisCamera != null)
            {
                if (thisCamera.cullingMask != this.fpsViewMask)
                {
                    thisCamera.cullingMask = this.fpsViewMask;
                }
            }
            if ((double) ((Component) this).GetComponent<Camera>().fieldOfView != (double) this.fpsFieldOfView)
        ((Component) this).GetComponent<Camera>().fieldOfView = this.fpsFieldOfView;
      ((Component) this).transform.localPosition = this.positionInsideDrone;
      ((Component) this).transform.localEulerAngles = this.rotationInsideDrone;
    }

    private void TPSCameraPositioning()
    {
            Transform thisTransform = ((Component)this).transform;
            Camera thisCamera = ((Component)this).GetComponent<Camera>();

            if (thisTransform.parent != null)
            {
                thisTransform.SetParent(null);
            }

            if (thisCamera != null && thisCamera.cullingMask != this.tpsLayerMask)
            {
                thisCamera.cullingMask = this.tpsLayerMask;
            }
            if ((double) ((Component) this).GetComponent<Camera>().fieldOfView != (double) this.tpsFieldOfView)
        ((Component) this).GetComponent<Camera>().fieldOfView = this.tpsFieldOfView;
      this.FollowDroneMethod();
      this.TiltCameraUpDown();
      this.FreeMouseMovementView();
    }

    private void FollowDroneMethod()
    {
      if (this.pickedMyDrone &&  this.ourDrone)
      {
                Transform thisTransform = ((Component)this).transform;
                Transform ourDroneTransform = this.ourDrone.transform;

                if (thisTransform != null && ourDroneTransform != null)
                {
                    Vector3 targetOffset = this.positionBehindDrone + new Vector3(0.0f, this.yScrollValue, this.zScrollValue);
                    Vector3 targetPosition = ourDroneTransform.TransformPoint(targetOffset);

                    thisTransform.position = Vector3.SmoothDamp(
                        thisTransform.position, // Vị trí hiện tại
                        targetPosition, // Vị trí mục tiêu
                        ref this.velocitiCameraFollow, // Tham chiếu đến vận tốc di chuyển mượt
                        this.cameraFollowPositionTime // Thời gian di chuyển mượt
                    );
                }
            }
      else
      {
        if (this.pickedMyDrone || this.dronesToControl.Length == 0)
          return;
                Transform thisTransform = ((Component)this).transform;
                Transform targetDroneTransform = this.dronesToControl[this.counterToControl].transform;

                if (thisTransform != null && targetDroneTransform != null)
                {
                    Vector3 targetPosition = targetDroneTransform.TransformPoint(this.positionBehindDrone + new Vector3(0.0f, this.yScrollValue, this.zScrollValue));

                    thisTransform.position = Vector3.SmoothDamp(
                        thisTransform.position, // Vị trí hiện tại
                        targetPosition, // Vị trí mục tiêu
                        ref this.velocitiCameraFollow, // Tham chiếu đến vận tốc di chuyển mượt
                        this.cameraFollowPositionTime // Thời gian di chuyển mượt
                    );
                }
            }
    }

    private void TiltCameraUpDown()
    {
      this.cameraYVelocity = Mathf.Lerp(this.cameraYVelocity, (float) (((double) ((Component) this).transform.position.y - (double) this.previousFramePos) * -(double) this.extraTilt), Time.deltaTime * 10f);
      this.previousFramePos = ((Component) this).transform.position.y;
    }

    private void FreeMouseMovementView()
    {
      if (this.freeMouseMovement)
      {
        float num1 = (!this.useJoystickFreeMovementOnly ? Input.GetAxis(this.mouse_Y_axisName) : 0.0f) * Time.deltaTime * this.mouseSensitvity;
        float num2 = (!this.useJoystickFreeMovementOnly ? Input.GetAxis(this.mouse_X_axisName) : 0.0f) * Time.deltaTime * this.mouseSensitvity;
        if ((double) num1 == 0.0)
          num1 = Input.GetAxis(this.dPad_Y_axisName) * Time.deltaTime * this.mouseSensitvity;
        if ((double) num2 == 0.0)
          num2 = Input.GetAxis(this.dPad_X_axisName) * Time.deltaTime * this.mouseSensitvity;
        this.mouseXwanted -= num1;
        this.mouseYwanted += num2;
        this.currentXPos = Mathf.SmoothDamp(this.currentXPos, this.mouseXwanted, ref this.xVelocity, this.mouseFollowTime);
        this.currentYPos = Mathf.SmoothDamp(this.currentYPos, this.mouseYwanted, ref this.yVelocity, this.mouseFollowTime);
                Transform thisTransform = ((Component)this).transform;
                DroneMovementScript droneMovementScript = this.ourDrone.GetComponent<DroneMovementScript>();

                if (thisTransform != null && droneMovementScript != null)
                {
                    Quaternion yRotation = Quaternion.Euler(14f, droneMovementScript.currentYRotation, 0.0f);
                    Quaternion xRotation = Quaternion.Euler(this.currentXPos, this.currentYPos, 0.0f);

                    thisTransform.rotation = yRotation * xRotation;
                }
            }
      else if (this.pickedMyDrone &&  this.ourDrone)
        ((Component) this).transform.rotation = Quaternion.Euler(new Vector3(14f + this.cameraYVelocity, this.ourDrone.GetComponent<DroneMovementScript>().currentYRotation, 0.0f));
      else if (!this.pickedMyDrone && this.dronesToControl.Length != 0)
        ((Component) this).transform.rotation = Quaternion.Euler(new Vector3(14f, ((Component) this.dronesToControl[this.counterToControl].transform).GetComponent<DroneMovementScript>().currentYRotation, 0.0f));
    }

    public void FPVTPSCamera()
    {
      if (this.FPS && this.pickedMyDrone)
        this.FPSCameraPositioning();
      else
        this.TPSCameraPositioning();
    }

    public void PickDroneToControl()
    {
      if (this.dronesToControl.Length > 1)
      {
        if (!this.pickedMyDrone)
        {
          if (this.canvasSelectButtons.Length != 0)
          {
            foreach (GameObject canvasSelectButton in this.canvasSelectButtons)
              canvasSelectButton.SetActive(true);
            foreach (GameObject canvasExitButton in this.canvasExitButtons)
              canvasExitButton.SetActive(false);
          }
          if (Input.GetKeyDown((KeyCode) 13))
            this.Select();
          if (Input.GetKeyDown((KeyCode) 97) || Input.GetKeyDown((KeyCode) 276))
            this.PressedLeft();
          if (!Input.GetKeyDown((KeyCode) 100) && !Input.GetKeyDown((KeyCode) 275))
            return;
          this.PressedRight();
        }
        else
        {
          if (this.canvasSelectButtons.Length != 0)
          {
            foreach (GameObject canvasSelectButton in this.canvasSelectButtons)
            {
              if ( canvasSelectButton)
                canvasSelectButton.SetActive(false);
            }
            foreach (GameObject canvasExitButton in this.canvasExitButtons)
            {
              if ( canvasExitButton)
                canvasExitButton.SetActive(true);
            }
          }
          if (Input.GetKeyDown((KeyCode) 27))
            this.ReturnToPick();
        }
      }
      else if (this.canvasSelectButtons.Length != 0)
      {
        foreach (GameObject canvasSelectButton in this.canvasSelectButtons)
        {
          if ( canvasSelectButton)
            canvasSelectButton.SetActive(false);
        }
        foreach (GameObject canvasExitButton in this.canvasExitButtons)
        {
          if ( canvasExitButton)
            canvasExitButton.SetActive(false);
        }
      }
    }

    public void ReturnToPick() => this.pickedMyDrone = false;

    public void Select()
    {
      this.ourDrone = this.dronesToControl[this.counterToControl].gameObject;
      this.pickedMyDrone = true;
    }

    public void PressedLeft()
    {
      if (this.counterToControl >= 1)
        --this.counterToControl;
      else
        this.counterToControl = this.dronesToControl.Length - 1;
    }

    public void PressedRight()
    {
      if (this.counterToControl < this.dronesToControl.Length - 1)
        ++this.counterToControl;
      else
        this.counterToControl = 0;
    }

    public void ScrollMath()
    {
      if ((double) Input.GetAxis("Mouse ScrollWheel") == 0.0)
        return;
      this.zScrollValue += Input.GetAxis("Mouse ScrollWheel") * this.zScrollAmountSensitivity;
      this.yScrollValue += Input.GetAxis("Mouse ScrollWheel") * this.yScrollAmountSensitivity;
    }

    private IEnumerator KeepTryingToFindOurDrone()
    {
      while ( this.ourDrone== null)
      {
        yield return (object) null;
        try
        {
          this.ourDrone = GameObject.FindGameObjectWithTag("Player").gameObject;
          if ( this.ourDrone)
            this.pickedMyDrone = true;
        }
        catch (Exception ex)
        {
        }
      }
    }
  }
}
