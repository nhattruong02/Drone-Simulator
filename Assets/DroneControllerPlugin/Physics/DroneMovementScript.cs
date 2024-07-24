// Decompiled with JetBrains decompiler
// Type: DroneController.Physics.DroneMovementScript
// Assembly: DroneControllerPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7F0498C7-D6E3-4EA5-898B-1106F4644D80
// Assembly location: C:\Project Unity\New Unity Project\New Unity Project\Assets\DroneControllerPlugin.dll

using DroneController.CameraMovement;
using DroneController.CustomGUI;
using DroneController.Profiles;
using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DroneController.Physics
{
    public class DroneMovementScript : MonoBehaviour
    {
           public Logic logic;
        public int _profileIndex;
       
        public Profile[] profiles = new Profile[3];
      
        public int inputEditorSelection;
       
        public bool idle = true;
        [Tooltip("Part of drone hierarchy that is animated, and will move drone according to the animations you made or default that you got with the package.")]
        public Animator animatedGameObject;
        [Header("Input INFO")]
        [Tooltip("Check this if you want to get phone contorls on screen")]
       
        public bool mobile_turned_on = false;
        [Tooltip("Check this if you want to use your joystick. (DON'T FORGET TO ADJUST THE INPUT SETTINGS!!)")]
        [HideInInspector]
        public bool joystick_turned_on = false;
        
        public CameraScript mainCamera;
        [Tooltip("Part of the drone hierarchy that is used to 'tilt'.")]

        public Transform droneObject;
        [Tooltip("Just a reading of current velocity")]
        
        public float velocity;
        [Header("MAX SPEEDS")]
        [Tooltip("Max forward speed")]
        
        public float maxForwardSpeed = 10f;
        [Tooltip("Max sideward speed")]
        
        public float maxSidewaySpeed = 5f;
        [Header("Drone slowdown")]
        [Range(0.0f, 2f)]
        [Tooltip("The value that will tell how fast does our drone slowdown after letting go of controls. (Lower value faster it slows down)")]
        
        public float slowDownTime = 0.95f;
        
        public float droneSoundAmplifier = 1f;
        [Tooltip("Upforce that will hold drone in place if i.e. idle, below add the force options for going upwards/downwards.")]
        
        public float upForce;
        [Header("Up & Down Forces")]
        [Tooltip("Value that will be applied when pressed the 'I' key(going upwards)")]
        
        public float forceUpHover = 450f;
        [Tooltip("Value that will be applied when pressed the 'K' key(going downwards)")]
        
        public float forceDownHover = -200f;
        [Header("Front & Side Movement Forces")]
        [Tooltip("Force that will be applied when moving to the sides.")]
        
        public float sideMovementAmount = 300f;
        [Tooltip("Force that will be applied when moving forwards/backwards.")]
        
        public float movementForwardSpeed = 500f;
        
        public float wantedSideTilt = 20f;
        
        public float currentYRotation;
        [Header("Rotation Amount Mulitplier.(When pressing J/L keys)")]
        [Tooltip("The multiplier of how fast our drone will rotate(higher the value, the faster it rotates)")]
        
        public float rotationAmount = 2.5f;
        [Header("Movement Tilt Speed")]
        [Range(0.0f, 1f)]
        [Tooltip("How fast will drone tilt to the side when moving. (The lower value the faster it will snap to the side)")]
        
        public float tiltMovementSpeed = 0.1f;
        [Range(0.0f, 1f)]
        [Tooltip("How fast will drone tilt to the original position when idle. (The lower value the faster it will snap to the side)")]
        
        public float tiltNoMovementSpeed = 0.3f;
        
        public float wantedForwardTilt = 20f;
        [Header("JOYSTICK AXIS INPUT")]
        [HideInInspector]
        public string left_analog_x = "Horizontal";
        [HideInInspector]
        public string left_analog_y = "Vertical";
        [HideInInspector]
        public string right_analog_x = "Horizontal_Right";
        [HideInInspector]
        public string right_analog_y = "Horizontal_UpDown";
        [HideInInspector]
        public KeyCode downButton = (KeyCode)343;
        [HideInInspector]
        public KeyCode upButton = (KeyCode)344;
       
        public bool analogUpDownMovement = false;
        
        public JoystickDrivingAxis left_analog_y_movement = JoystickDrivingAxis.forward;
        
        public JoystickDrivingAxis left_analog_x_movement = JoystickDrivingAxis.sideward;
       
        public JoystickDrivingAxis right_analog_y_movement = JoystickDrivingAxis.upward;
        
        public JoystickDrivingAxis right_analog_x_movement = JoystickDrivingAxis.rotation;
        [Header("INPUT TRANSLATED FOR MOBILE CONTROLS")]
        [HideInInspector]
        public bool W;
        [HideInInspector]
        public bool S;
        [HideInInspector]
        public bool A;
        [HideInInspector]
        public bool D;
        [HideInInspector]
        public bool I;
        [HideInInspector]
        public bool K;
        [HideInInspector]
        public bool J;
        [HideInInspector]
        public bool L;
       
        public Texture forward_button_texture;
       
        public Texture backward_button_texture;
       
        public Texture leftward_button_texture;
       
        public Texture rightward_button_texture;
        
        public Texture upwards_button_texture;
        
        public Texture downwards_button_texture;
        
        public Texture rotation_left_button_texture;
        
        public Texture rotation_right_button_texture;
        [Header("Keyboard Inputs")]
        [HideInInspector]
        public KeyCode forward = (KeyCode)119;
        [HideInInspector]
        public KeyCode backward = (KeyCode)115;
        [HideInInspector]
        public KeyCode rightward = (KeyCode)100;
        [HideInInspector]
        public KeyCode leftward = (KeyCode)97;
        [HideInInspector]
        public KeyCode upward = (KeyCode)105;
        [HideInInspector]
        public KeyCode downward = (KeyCode)107;
        [HideInInspector]
        public KeyCode rotateRightward = (KeyCode)108;
        [HideInInspector]
        public KeyCode rotateLeftward = (KeyCode)106;
        [Header("Tricks keyboard input")]
        [HideInInspector]
        public KeyCode barrelRollRight = (KeyCode)111;
        [HideInInspector]
        public KeyCode barrelRollLeft = (KeyCode)117;
        [HideInInspector]
        public KeyCode swingLeft = (KeyCode)113;
        [HideInInspector]
        public KeyCode swingRight = (KeyCode)101;
        [HideInInspector]
        public KeyCode joystick_barrelRollRight = (KeyCode)331;
        [HideInInspector]
        public KeyCode joystick_barrelRollLeft = (KeyCode)333;
        [HideInInspector]
        public KeyCode joystick_swingLeft = (KeyCode)332;
        [HideInInspector]
        public KeyCode joystick_swingRight = (KeyCode)330;
        
        public float customFeed_forward;
        
        public float customFeed_backward;
        
        public float customFeed_leftward;
        
        public float customFeed_rightward;
        
        public float customFeed_upward;
        
        public float customFeed_downward;
        [HideInInspector]
        public float customFeed_rotateLeft;
        [HideInInspector]
        public float customFeed_rotateRight;
        [HideInInspector]
        public bool customFeed;

        private Rigidbody ourDrone;

        private AudioSource droneSound;
        private Vector3 velocityToSmoothDampToZero;
        private float mouseScrollWheelAmount;
        private float wantedHeight;
        private float currentHeight;
        private float heightVelocity;
        private float tiltAmountSideways = 0.0f;
        private float tiltVelocitySideways;
        private float wantedYRotation;
        private float rotationYVelocity;
        private float tiltAmountForward = 0.0f;
        private float tiltVelocityForward;
        private float Vertical_W = 0.0f;
        private float Vertical_S = 0.0f;
        private float Horizontal_A = 0.0f;
        private float Horizontal_D = 0.0f;
        private float Vertical_I = 0.0f;
        private float Vertical_K = 0.0f;
        private float Horizontal_J = 0.0f;
        private float Horizontal_L = 0.0f;
        private Rect wRect = new Rect(10f, 55f, 14f, 20f);
        private Rect sRect = new Rect(10f, 80f, 14f, 20f);
        private Rect aRect = new Rect(0.0f, 67.5f, 14f, 20f);
        private Rect dRect = new Rect(20f, 67.5f, 14f, 20f);
        private Rect iRect = new Rect(76f, 55f, 14f, 20f);
        private Rect jRect = new Rect(66f, 67.5f, 14f, 20f);
        private Rect kRect = new Rect(76f, 80f, 14f, 20f);
        private Rect lRect = new Rect(86f, 67.5f, 14f, 20f);
        private Vector2[] myTouchPosition = new Vector2[2]
        {
      new Vector2(0.0f, 0.0f),
      new Vector2(0.0f, 0.0f)
        };

        public virtual void Awake()
        {
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
           
            this.ourDrone = ((Component)this).GetComponent<Rigidbody>();
            this.StartCoroutine(this.FindMainCamera());
            this.SetStartingRotation();
            this.FindDroneSoundComponent();
        }

        private void OnGUI()
        {
            if (!mobile_turned_on)
                return;
            DrawGUI.DrawTexture(((Rect)this.wRect).x, ((Rect) this.wRect).y, ((Rect) this.wRect).width, ((Rect) this.wRect).height, this.forward_button_texture);
            DrawGUI.DrawTexture(((Rect)this.sRect).x, ((Rect) this.sRect).y, ((Rect)this.sRect).width, ((Rect) this.sRect).height, this.backward_button_texture);
            DrawGUI.DrawTexture(((Rect) this.aRect).x, ((Rect) this.aRect).y, ((Rect) this.aRect).width, ((Rect) this.aRect).height, this.leftward_button_texture);
            DrawGUI.DrawTexture(((Rect) this.dRect).x, ((Rect) this.dRect).y, ((Rect) this.dRect).width, ((Rect)this.dRect).height, this.rightward_button_texture);
            DrawGUI.DrawTexture(((Rect)this.iRect).x, ((Rect) this.iRect).y, ((Rect) this.iRect).width, ((Rect) this.iRect).height, this.upwards_button_texture);
            DrawGUI.DrawTexture(((Rect) this.jRect).x, ((Rect) this.jRect).y, ((Rect) this.jRect).width, ((Rect)this.jRect).height, this.rotation_left_button_texture);
            DrawGUI.DrawTexture(((Rect) this.kRect).x, ((Rect) this.kRect).y, ((Rect) this.kRect).width, ((Rect) this.kRect).height, this.downwards_button_texture);
            DrawGUI.DrawTexture(((Rect) this.lRect).x, ((Rect) this.lRect).y, ((Rect) this.lRect).width, ((Rect) this.lRect).height, this.rotation_right_button_texture);
        }

        private void SetStartingRotation()
        {
            this.wantedYRotation = ((Component)this).transform.eulerAngles.y;
            this.currentYRotation = ((Component)this).transform.eulerAngles.y;
        }

        private void FindDroneSoundComponent()
        {
            try
            {
                if (((Component)this).gameObject.transform.Find("drone_sound")?.GetComponent<AudioSource>())
                    this.droneSound = ((Component)((Component)this).gameObject.transform.Find("drone_sound")).GetComponent<AudioSource>();
                else
                    MonoBehaviour.print((object)"Found drone_sound but it has no AudioSource component.");
            }
            catch (Exception ex)
            {
                MonoBehaviour.print((object)("No Sound Child GameObject ->" + ex.StackTrace.ToString()));
            }
        }

        private void Left_Analog_Y_Translation()
        {

            if (this.left_analog_y_movement == JoystickDrivingAxis.forward)
            {
                this.W = (double)Input.GetAxisRaw(this.left_analog_y) > 0.0;
                this.S = (double)Input.GetAxisRaw(this.left_analog_y) < 0.0;
            }
            else if (this.left_analog_y_movement == JoystickDrivingAxis.sideward)
            {
                this.D = (double)Input.GetAxisRaw(this.left_analog_y) > 0.0;
                this.A = (double)Input.GetAxisRaw(this.left_analog_y) < 0.0;
            }
            else if (this.left_analog_y_movement == JoystickDrivingAxis.upward)
            {
                if (!this.analogUpDownMovement)
                    return;
                this.I = (double)Input.GetAxisRaw(this.left_analog_y) > 0.0;
                this.K = (double)Input.GetAxisRaw(this.left_analog_y) < 0.0;
            }
            else
            {
                if (this.left_analog_y_movement != JoystickDrivingAxis.rotation)
                    return;
                this.J = -(double)Input.GetAxisRaw(this.left_analog_y) > 0.0;
                this.L = -(double)Input.GetAxisRaw(this.left_analog_y) < 0.0;
            }
        }

        private void Left_Analog_X_Translation()
        {
            if (this.left_analog_x_movement == JoystickDrivingAxis.forward)
            {
                this.W = (double)Input.GetAxisRaw(this.left_analog_x) > 0.0;
                this.S = (double)Input.GetAxisRaw(this.left_analog_x) < 0.0;
            }
            else if (this.left_analog_x_movement == JoystickDrivingAxis.sideward)
            {
                this.D = (double)Input.GetAxisRaw(this.left_analog_x) > 0.0;
                this.A = (double)Input.GetAxisRaw(this.left_analog_x) < 0.0;
            }
            else if (this.left_analog_x_movement == JoystickDrivingAxis.upward)
            {
                if (!this.analogUpDownMovement)
                    return;
                this.I = (double)Input.GetAxisRaw(this.left_analog_x) > 0.0;
                this.K = (double)Input.GetAxisRaw(this.left_analog_x) < 0.0;
            }
            else
            {
                if (this.left_analog_x_movement != JoystickDrivingAxis.rotation)
                    return;
                this.J = -(double)Input.GetAxisRaw(this.left_analog_x) > 0.0;
                this.L = -(double)Input.GetAxisRaw(this.left_analog_x) < 0.0;
            }
        }

        private void Right_Analog_Y_Translation()
        {
            if (this.right_analog_y_movement == JoystickDrivingAxis.forward)
            {
                this.W = -(double)Input.GetAxisRaw(this.right_analog_y) > 0.0;
                this.S = -(double)Input.GetAxisRaw(this.right_analog_y) < 0.0;
            }
            else if (this.right_analog_y_movement == JoystickDrivingAxis.sideward)
            {
                this.D = (double)Input.GetAxisRaw(this.right_analog_y) > 0.0;
                this.A = (double)Input.GetAxisRaw(this.right_analog_y) < 0.0;
            }
            else if (this.right_analog_y_movement == JoystickDrivingAxis.upward)
            {
                if (!this.analogUpDownMovement)
                    return;
                this.I = -(double)Input.GetAxisRaw(this.right_analog_y) > 0.0;
                this.K = -(double)Input.GetAxisRaw(this.right_analog_y) < 0.0;
            }
            else
            {
                if (this.right_analog_y_movement != JoystickDrivingAxis.rotation)
                    return;
                this.J = -(double)Input.GetAxisRaw(this.right_analog_y) > 0.0;
                this.L = -(double)Input.GetAxisRaw(this.right_analog_y) < 0.0;
            }
        }

        private void Right_Analog_X_Translation()
        {
            if (this.right_analog_x_movement == JoystickDrivingAxis.forward)
            {
                this.W = -(double)Input.GetAxisRaw(this.right_analog_x) > 0.0;
                this.S = -(double)Input.GetAxisRaw(this.right_analog_x) < 0.0;
            }
            else if (this.right_analog_x_movement == JoystickDrivingAxis.sideward)
            {
                this.D = (double)Input.GetAxisRaw(this.right_analog_x) > 0.0;
                this.A = (double)Input.GetAxisRaw(this.right_analog_x) < 0.0;
            }
            else if (this.right_analog_x_movement == JoystickDrivingAxis.upward)
            {
                if (!this.analogUpDownMovement)
                    return;
                this.I = (double)Input.GetAxisRaw(this.right_analog_x) > 0.0;
                this.K = (double)Input.GetAxisRaw(this.right_analog_x) < 0.0;
            }
            else
            {
                if (this.right_analog_x_movement != JoystickDrivingAxis.rotation)
                    return;
                this.J = -(double)Input.GetAxisRaw(this.right_analog_x) > 0.0;
                this.L = -(double)Input.GetAxisRaw(this.right_analog_x) < 0.0;
            }
        }

        private void Input_Mobile_Sensitvity_Calculation()
        {
            this.Vertical_W = !this.W ? Mathf.LerpUnclamped(this.Vertical_W, 0.0f, Time.deltaTime * 10f) : Mathf.LerpUnclamped(this.Vertical_W, 1f, Time.deltaTime * 10f);
            this.Vertical_S = !this.S ? Mathf.LerpUnclamped(this.Vertical_S, 0.0f, Time.deltaTime * 10f) : Mathf.LerpUnclamped(this.Vertical_S, -1f, Time.deltaTime * 10f);
            this.Horizontal_A = !this.A ? Mathf.LerpUnclamped(this.Horizontal_A, 0.0f, Time.deltaTime * 10f) : Mathf.LerpUnclamped(this.Horizontal_A, -1f, Time.deltaTime * 10f);
            this.Horizontal_D = !this.D ? Mathf.LerpUnclamped(this.Horizontal_D, 0.0f, Time.deltaTime * 10f) : Mathf.LerpUnclamped(this.Horizontal_D, 1f, Time.deltaTime * 10f);
            this.Vertical_I = !this.I ? Mathf.LerpUnclamped(this.Vertical_I, 0.0f, Time.deltaTime * 10f) : Mathf.LerpUnclamped(this.Vertical_I, 1f, Time.deltaTime * 10f);
            this.Vertical_K = !this.K ? Mathf.LerpUnclamped(this.Vertical_K, 0.0f, Time.deltaTime * 10f) : Mathf.LerpUnclamped(this.Vertical_K, -1f, Time.deltaTime * 10f);
            this.Horizontal_J = !this.J ? Mathf.LerpUnclamped(this.Horizontal_J, 0.0f, Time.deltaTime * 10f) : Mathf.LerpUnclamped(this.Horizontal_J, 1f, Time.deltaTime * 10f);
            if (this.L)
                this.Horizontal_L = Mathf.LerpUnclamped(this.Horizontal_L, -1f, Time.deltaTime * 10f);
            else
                this.Horizontal_L = Mathf.LerpUnclamped(this.Horizontal_L, 0.0f, Time.deltaTime * 10f);
        }

        private void Joystick_Input_Sensitivity_Calculation()
        {
            if (!this.analogUpDownMovement)
            {
                this.Vertical_I = !this.I ? Mathf.LerpUnclamped(this.Vertical_I, 0.0f, Time.deltaTime * 10f) : Mathf.LerpUnclamped(this.Vertical_I, 1f, Time.deltaTime * 10f);
                this.Vertical_K = !this.K ? Mathf.LerpUnclamped(this.Vertical_K, 0.0f, Time.deltaTime * 10f) : Mathf.LerpUnclamped(this.Vertical_K, -1f, Time.deltaTime * 10f);
            }
            this.Left_Analog_Y_Movement();
            this.Left_Analog_X_Movement();
            this.Right_Analog_Y_Movement();
            this.Right_Analog_X_Movement();
        }

        private void Left_Analog_Y_Movement()
        {
            if (this.left_analog_y_movement == JoystickDrivingAxis.forward)
            {
                this.Vertical_W = Input.GetAxis(this.left_analog_y);
                this.Vertical_S = Input.GetAxis(this.left_analog_y);
            }
            else if (this.left_analog_y_movement == JoystickDrivingAxis.sideward)
            {
                this.Horizontal_D = Input.GetAxis(this.left_analog_y);
                this.Horizontal_A = Input.GetAxis(this.left_analog_y);
            }
            else if (this.left_analog_y_movement == JoystickDrivingAxis.upward)
            {
                if (!this.analogUpDownMovement)
                    return;
                this.Vertical_I = Input.GetAxis(this.left_analog_y);
                this.Vertical_K = Input.GetAxis(this.left_analog_y);
            }
            else
            {
                if (this.left_analog_y_movement != JoystickDrivingAxis.rotation)
                    return;
                this.Horizontal_J = Input.GetAxis(this.left_analog_y);
                this.Horizontal_L = Input.GetAxis(this.left_analog_y);
            }
        }

        private void Left_Analog_X_Movement()
        {
            if (this.left_analog_x_movement == JoystickDrivingAxis.forward)
            {
                this.Vertical_W = Input.GetAxis(this.left_analog_x);
                this.Vertical_S = Input.GetAxis(this.left_analog_x);
            }
            else if (this.left_analog_x_movement == JoystickDrivingAxis.sideward)
            {
                this.Horizontal_D = Input.GetAxis(this.left_analog_x);
                this.Horizontal_A = Input.GetAxis(this.left_analog_x);
            }
            else if (this.left_analog_x_movement == JoystickDrivingAxis.upward)
            {
                if (!this.analogUpDownMovement)
                    return;
                this.Vertical_I = Input.GetAxis(this.left_analog_x);
                this.Vertical_K = Input.GetAxis(this.left_analog_x);
            }
            else
            {
                if (this.left_analog_x_movement != JoystickDrivingAxis.rotation)
                    return;
                this.Horizontal_J = Input.GetAxis(this.left_analog_x);
                this.Horizontal_L = Input.GetAxis(this.left_analog_x);
            }
        }

        private void Right_Analog_Y_Movement()
        {
            if (this.right_analog_y_movement == JoystickDrivingAxis.forward)
            {
                this.Vertical_W = -Input.GetAxis(this.right_analog_y);
                this.Vertical_S = -Input.GetAxis(this.right_analog_y);
            }
            else if (this.right_analog_y_movement == JoystickDrivingAxis.sideward)
            {
                this.Horizontal_D = Input.GetAxis(this.right_analog_y);
                this.Horizontal_A = Input.GetAxis(this.right_analog_y);
            }
            else if (this.right_analog_y_movement == JoystickDrivingAxis.upward)
            {
                if (!this.analogUpDownMovement)
                    return;
                this.Vertical_I = -Input.GetAxis(this.right_analog_y);
                this.Vertical_K = -Input.GetAxis(this.right_analog_y);
            }
            else
            {
                if (this.right_analog_y_movement != JoystickDrivingAxis.rotation)
                    return;
                this.Horizontal_J = Input.GetAxis(this.right_analog_y);
                this.Horizontal_L = Input.GetAxis(this.right_analog_y);
            }
        }

        private void Right_Analog_X_Movement()
        {
            if (this.right_analog_x_movement == JoystickDrivingAxis.forward)
            {
                this.Vertical_W = -Input.GetAxis(this.right_analog_x);
                this.Vertical_S = -Input.GetAxis(this.right_analog_x);
            }
            else if (this.right_analog_x_movement == JoystickDrivingAxis.sideward)
            {
                this.Horizontal_D = Input.GetAxis(this.right_analog_x);
                this.Horizontal_A = Input.GetAxis(this.right_analog_x);
            }
            else if (this.right_analog_x_movement == JoystickDrivingAxis.upward)
            {
                if (!this.analogUpDownMovement)
                    return;
                this.Vertical_I = Input.GetAxis(this.right_analog_x);
                this.Vertical_K = Input.GetAxis(this.right_analog_x);
            }
            else
            {
                if (this.right_analog_x_movement != JoystickDrivingAxis.rotation)
                    return;
                this.Horizontal_J = Input.GetAxis(this.right_analog_x);
                this.Horizontal_L = Input.GetAxis(this.right_analog_x);
            }
        }

        private void CheckingIfInside()
        {
            this.W = ((Rect) this.wRect).Contains(this.myTouchPosition[0]) || ((Rect) this.wRect).Contains(this.myTouchPosition[1]);
            this.S = ((Rect) this.sRect).Contains(this.myTouchPosition[0]) || ((Rect) this.sRect).Contains(this.myTouchPosition[1]);
            this.A = ((Rect) this.aRect).Contains(this.myTouchPosition[0]) || ((Rect) this.aRect).Contains(this.myTouchPosition[1]);
            this.D = ((Rect) this.dRect).Contains(this.myTouchPosition[0]) || ((Rect)this.dRect).Contains(this.myTouchPosition[1]);
            this.I = ((Rect) this.iRect).Contains(this.myTouchPosition[0]) || ((Rect) this.iRect).Contains(this.myTouchPosition[1]);
            this.J = ((Rect) this.jRect).Contains(this.myTouchPosition[0]) || ((Rect) this.jRect).Contains(this.myTouchPosition[1]);
            this.K = ((Rect) this.kRect).Contains(this.myTouchPosition[0]) || ((Rect) this.kRect).Contains(this.myTouchPosition[1]);
            this.L = ((Rect) this.lRect).Contains(this.myTouchPosition[0]) || ((Rect) this.lRect).Contains(this.myTouchPosition[1]);
        }

        private void TouchCalculations()
        {
            for (int index1 = 0; index1 < Input.touches.Length; ++index1)
            {
                Vector2[] touchPosition = this.myTouchPosition;
                int index2 = index1;
                Touch touch = Input.GetTouch(index1);
                double x = (double)((Touch) touch).position.x;
                double height = (double)Screen.height;
                touch = Input.GetTouch(index1);
                double y = (double)((Touch) touch).position.y;
                double num = height - y;
                Vector2 vector2 = DrawGUI.Percentages(new Vector2((float)x, (float)num));
                touchPosition[index2] = vector2;
            }
            if (Input.touchCount == 0)
            {
                for (int index = 0; index < 2; ++index)
                    this.myTouchPosition[index] = Vector2.zero;
            }
            if (Input.touchCount != 1)
                return;
            this.myTouchPosition[1] = Vector2.zero;
        }

        public void GetVelocity()
        {
            Vector3 velocity = this.ourDrone.velocity;
            this.velocity = ((Vector3) velocity).magnitude;
        }

        public void BasicDroneHoverAndRotation()
        {
            try
            {
                this.ourDrone.AddRelativeForce(Vector3.up * this.upForce);
                this.ourDrone.rotation = Quaternion.Euler(new Vector3(0.0f, this.currentYRotation, 0.0f));
                this.ourDrone.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
                this.droneObject.localRotation = Quaternion.Euler(new Vector3(this.tiltAmountForward, 0.0f, this.tiltAmountSideways));
            }catch (UnassignedReferenceException)
            {

            }
        }

        public void ResetDroneObjectRotation()
        {
            this.tiltAmountForward = 0.0f;
            this.currentYRotation = 0.0f;
            this.wantedYRotation = 0.0f;
            this.tiltAmountSideways = 0.0f;
        }

        public void ResetDroneObjectRotation(float _startingYRotation)
        {
            this.tiltAmountForward = 0.0f;
            this.currentYRotation = _startingYRotation;
            this.wantedYRotation = _startingYRotation;
            this.tiltAmountSideways = 0.0f;
        }

        public void SettingControllerToInputSettings()
        {
            if (!this.customFeed)
            {
                if (!this.joystick_turned_on)
                    this.Input_Mobile_Sensitvity_Calculation();
                else
                    this.Joystick_Input_Sensitivity_Calculation();
                if (!mobile_turned_on)
                    return;
                this.TouchCalculations();
                this.CheckingIfInside();
            }
            else
                this.CustomInputFeed();
        }

        private void CustomInputFeed()
        {
            this.Vertical_W = this.customFeed_forward;
            this.W = (double)this.customFeed_forward > 0.0;
            this.Vertical_S = -this.customFeed_backward;
            this.S = (double)this.customFeed_backward > 0.0;
            this.Horizontal_A = -this.customFeed_leftward;
            this.A = (double)this.customFeed_leftward > 0.0;
            this.Horizontal_D = this.customFeed_rightward;
            this.D = (double)this.customFeed_rightward > 0.0;
            this.Vertical_I = this.customFeed_upward;
            this.I = (double)this.customFeed_upward > 0.0;
            this.Vertical_K = -this.customFeed_downward;
            this.K = (double)this.customFeed_downward > 0.0;
            this.Horizontal_J = -this.customFeed_rotateLeft;
            this.J = (double)this.customFeed_rotateLeft > 0.0;
            this.Horizontal_L = this.customFeed_rotateRight;
            if ((double)this.customFeed_rotateRight > 0.0)
                this.L = true;
            else
                this.L = false;
        }

        public void RotationUpdateLoop_TrickRotation()
        {
            if (!this.mainCamera.pickedMyDrone || !Object.ReferenceEquals(this.mainCamera.ourDrone.transform, ((Component)this).transform))
                return;
            if ((Input.GetKeyDown(this.barrelRollLeft) || Input.GetKeyDown(this.joystick_barrelRollLeft)) && !this.idle)
                this.wantedYRotation -= 100f;
            if ((Input.GetKeyDown(this.barrelRollRight) || Input.GetKeyDown(this.joystick_barrelRollRight)) && !this.idle)
                this.wantedYRotation += 100f;
        }

        public void CameraCorrectPickAndTranslatingInputToWSAD()
        {
            if (this.customFeed || !this.mainCamera.pickedMyDrone || this.mainCamera.ourDrone.transform != ((Component)this).transform)
                return;
            if (!mobile_turned_on && !this.joystick_turned_on)
            {
                this.W = Input.GetKey(this.forward);
                this.S = Input.GetKey(this.backward);
                this.A = Input.GetKey(this.leftward);
                this.D = Input.GetKey(this.rightward);
                this.I = Input.GetKey(this.upward);
                this.J = Input.GetKey(this.rotateLeftward);
                this.K = Input.GetKey(this.downward);
                this.L = Input.GetKey(this.rotateRightward);
            }
            if (!mobile_turned_on && this.joystick_turned_on)
            {
                if (!this.analogUpDownMovement)
                {
                    this.K = Input.GetKey(this.downButton);
                    this.I = Input.GetKey(this.upButton);
                }
                this.Left_Analog_Y_Translation();
                this.Left_Analog_X_Translation();
                this.Right_Analog_Y_Translation();
                this.Right_Analog_X_Translation();
            }
        }

        public void Animations()
        {
            try { 
            if (this.animatedGameObject != null)
                return;
            this.animatedGameObject.SetBool("idle", this.idle);
            if (Input.GetKeyDown(this.barrelRollLeft) || Input.GetKeyDown(this.joystick_barrelRollLeft) && !this.idle)
                this.StartCoroutine("Twirl_left_Method");
            if (Input.GetKeyDown(this.barrelRollRight) || Input.GetKeyDown(this.joystick_barrelRollRight) && !this.idle)
                this.StartCoroutine("Twirl_right_Method");
            if (Input.GetKeyDown(this.swingLeft) || Input.GetKeyDown(this.joystick_swingLeft) && !this.idle)
                this.StartCoroutine("Left_Passage");
            if (Input.GetKeyDown(this.swingRight) || Input.GetKeyDown(this.joystick_swingRight) && !this.idle)
                this.StartCoroutine("Right_Passage");
            }catch (UnassignedReferenceException)
            {

            }
    }

        public void ClampingSpeedValues()
        {
            Vector3 velocity1;
            if ((this.W || this.S) && (this.A || this.D))
            {
                Rigidbody drone = this.ourDrone;
                Vector3 velocity2 = this.ourDrone.velocity;
                velocity1 = this.ourDrone.velocity;
                double num = (double)Mathf.Lerp(((Vector3) velocity1).magnitude, this.maxForwardSpeed, Time.deltaTime * 5f);
                Vector3 vector3 = Vector3.ClampMagnitude(velocity2, (float)num);
                drone.velocity = vector3;
            }
            if ((this.W || this.S) && !this.A && !this.D)
            {
                Rigidbody drone = this.ourDrone;
                Vector3 velocity3 = this.ourDrone.velocity;
                velocity1 = this.ourDrone.velocity;
                double num = (double)Mathf.Lerp(((Vector3) velocity1).magnitude, this.maxForwardSpeed, Time.deltaTime * 5f);
                Vector3 vector3 = Vector3.ClampMagnitude(velocity3, (float)num);
                drone.velocity = vector3;
            }
            if (!this.W && !this.S && (this.A || this.D))
            {
                Rigidbody drone = this.ourDrone;
                Vector3 velocity4 = this.ourDrone.velocity;
                velocity1 = this.ourDrone.velocity;
                double num = (double)Mathf.Lerp(((Vector3) velocity1).magnitude, this.maxSidewaySpeed, Time.deltaTime * 5f);
                Vector3 vector3 = Vector3.ClampMagnitude(velocity4, (float)num);
                drone.velocity = vector3;
            }
            if (this.W || this.S || this.A || this.D)
                return;
            this.ourDrone.velocity = Vector3.SmoothDamp(this.ourDrone.velocity, Vector3.zero, ref this.velocityToSmoothDampToZero, this.slowDownTime);
        }

        public void DroneSound()
        {
            if (this.droneSound == null)
                return;
            this.droneSound.pitch = (float)(1.0 + (double)this.velocity / 100.0 * (double)this.droneSoundAmplifier);
        }

        public void MovementUpDown()
        {
            if (this.W || this.S || this.A || this.D)
            {
                this.idle = false;
                if (this.I || this.K)
                    this.ourDrone.velocity = this.ourDrone.velocity;
                if (!this.I && !this.K && !this.J && !this.L)
                {
                    this.ourDrone.velocity = new Vector3(this.ourDrone.velocity.x, Mathf.Lerp(this.ourDrone.velocity.y, 0.0f, Time.deltaTime * 5f), this.ourDrone.velocity.z);
                    this.upForce = this.ourDrone.mass * 9.81001f;
                }
                if (!this.I && !this.K && (this.J || this.L))
                {
                    this.ourDrone.velocity = new Vector3(this.ourDrone.velocity.x, Mathf.Lerp(this.ourDrone.velocity.y, 0.0f, Time.deltaTime * 5f), this.ourDrone.velocity.z);
                    this.upForce = this.ourDrone.mass * 9.81005f;
                }
            }
            if ((!this.W || !this.S) && (this.A || this.D))
            {
                this.idle = false;
                this.upForce = this.ourDrone.mass * 9.81002f;
            }
            if ((this.W || this.S) && (this.A || this.D))
            {
                this.idle = false;
                if (this.J || this.L)
                    this.upForce = this.ourDrone.mass * 9.81003f;
            }
            if (this.I)
            {
                this.idle = false;
                this.upForce = (float)((double)this.forceUpHover * (double)this.Vertical_I + (1.0 - (double)this.Vertical_I) * (double)this.ourDrone.mass * 9.8100299835205078);
            }
            else if (this.K)
            {
                this.idle = false;
                this.upForce = this.forceDownHover * -this.Vertical_K;
            }
            else
            {
                if (this.I || this.K || this.W || this.S || this.A || this.D)
                    return;
                this.upForce = this.ourDrone.mass * 9.81f;
                this.idle = true;
            }
        }

        public void MovementLeftRight()
        {
            if (this.A)
            {
                this.ourDrone.AddRelativeForce(Vector3.right * this.Horizontal_A * this.sideMovementAmount);
                this.tiltAmountSideways = Mathf.SmoothDamp(this.tiltAmountSideways, -this.wantedSideTilt * this.Horizontal_A, ref this.tiltVelocitySideways, this.tiltMovementSpeed);
            }
            if (this.D)
            {
                this.ourDrone.AddRelativeForce(Vector3.right * this.Horizontal_D * this.sideMovementAmount);
                this.tiltAmountSideways = Mathf.SmoothDamp(this.tiltAmountSideways, -this.wantedSideTilt * this.Horizontal_D, ref this.tiltVelocitySideways, this.tiltMovementSpeed);
            }
            if (this.A || this.D)
                return;
            this.tiltAmountSideways = Mathf.SmoothDamp(this.tiltAmountSideways, 0.0f, ref this.tiltVelocitySideways, this.tiltNoMovementSpeed);
        }

        public void Rotation()
        {
            if (!this.customFeed)
            {
                if (!this.joystick_turned_on)
                {
                    if (this.J)
                        this.wantedYRotation -= this.rotationAmount;
                    if (this.L)
                        this.wantedYRotation += this.rotationAmount;
                }
                else
                    this.wantedYRotation += this.rotationAmount * this.Horizontal_J;
            }
            else
            {
                if (this.J)
                    this.wantedYRotation += this.Horizontal_J * this.rotationAmount;
                if (this.L)
                    this.wantedYRotation += this.Horizontal_L * this.rotationAmount;
            }
            this.currentYRotation = Mathf.SmoothDamp(this.currentYRotation, this.wantedYRotation, ref this.rotationYVelocity, 0.25f);
        }

        public void MovementForward()
        {
            if (this.W)
            {
                this.ourDrone.AddRelativeForce(Vector3.forward * this.Vertical_W * this.movementForwardSpeed);
                this.tiltAmountForward = Mathf.SmoothDamp(this.tiltAmountForward, this.wantedForwardTilt * this.Vertical_W, ref this.tiltVelocityForward, this.tiltMovementSpeed);
            }
            if (this.S)
            {
                this.ourDrone.AddRelativeForce(Vector3.forward * this.Vertical_S * this.movementForwardSpeed);
                this.tiltAmountForward = Mathf.SmoothDamp(this.tiltAmountForward, this.wantedForwardTilt * this.Vertical_S, ref this.tiltVelocityForward, this.tiltMovementSpeed);
            }
            if (this.W || this.S)
                return;
            this.tiltAmountForward = Mathf.SmoothDamp(this.tiltAmountForward, 0.0f, ref this.tiltVelocityForward, this.tiltNoMovementSpeed);
        }

        private IEnumerator FindMainCamera()
        {
            while (this.mainCamera == null)
            {
                try
                {
                    this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
                }
                catch (Exception ex)
                {
                    MonoBehaviour.print((object)("<color=red>Missing main camera! check the tags!</color> -> " + (object)ex));
                }
                yield return (object)new WaitForEndOfFrame();
            }
        }

        private IEnumerator Left_Passage()
        {
            this.animatedGameObject.SetBool("left_passage", true);
            yield return (object)new WaitForSeconds(0.5f);
            this.animatedGameObject.SetBool("left_passage", false);
        }

        private IEnumerator Right_Passage()
        {
            this.animatedGameObject.SetBool("right_passage", true);
            yield return (object)new WaitForSeconds(0.5f);
            this.animatedGameObject.SetBool("right_passage", false);
        }

        private IEnumerator Twirl_left_Method()
        {
            this.animatedGameObject.SetBool("twirl_left", true);
            yield return (object)new WaitForSeconds(0.5f);
            this.animatedGameObject.SetBool("twirl_left", false);
        }

        private IEnumerator Twirl_right_Method()
        {
            this.animatedGameObject.SetBool("twirl_right", true);
            yield return (object)new WaitForSeconds(0.5f);
            this.animatedGameObject.SetBool("twirl_right", false);
        }
    }
}
