using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//needed for using the steamVR functionality (e.g. actions/actionset) 
using Valve.VR;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
//takes in inputs and converts to actions
public class VR_Movement: MonoBehaviour
{
    //variables relating to movement
    #region Movement Variables
    [Header("Movement")]

    //stores the current gravity of the player
    public float m_Gravity = 1.0f;
    //the sensitivity that determines how far the finger needs to be placed to move
    public float m_Sensitivity = 0.5f;
    //the max speed for the player to travel (keep low unless incorperating VR Tunneling)
    public float m_MaxSpeed = 1.5f;

    //a boolean that stores the input for a movement button being pressed
    public SteamVR_Action_Boolean m_MovePress = null;
    //a vector2 that stores the input for the position/direction for movement
    public SteamVR_Action_Vector2 m_MoveValue = null;

    //a boolean for classic vr movement style (always move towards head direction)
    public bool m_ClassicMovement => !m_AlternateMovement;
    //a boolean for alternate vr movement style (changes direction when movement is reset)
    public bool m_AlternateMovement => !m_ClassicMovement;


    //stores the current speed of the player (forward and back)
    private float m_Speed = 0.0f;
    //stores the current strafe speed of the player (left and right)
    private float m_Strafe = 0.0f;
    
    //the offset value for the collision (moving it away from the player)
    [SerializeField] private Vector3 m_OffsetAmount = Vector3.zero;
    
    
    //the orientation of the player (defaults to quaternian.identity / (0,0,0,0) )
    private Quaternion m_Orientation = Quaternion.identity;
    //stores the desired center offset to apply to the capsule collider (default to vector3.zero / (0,0,0) )  (this takes in the real world position of the player Into the games collision detection)
    private Vector3 m_Center = Vector3.zero;

    //the offset of the player collision (defaults to vector3.zero / (0,0,0) )
    private Vector3 m_Offset = Vector3.zero;



    #region Unused variables
    //the button for rotating the snap rotating the player 
    //public SteamVR_Action_Boolean m_RotatePress = null;
    //the direction for snap rotating the player
    //public SteamVR_Action_Vector2 m_RotateDirection = null;

    //the amount to rotate when a button is pressed (degrees)
    //public float m_RotateIncrement = 10;
    #endregion


    #endregion

    //component variables that are referenced for data
    #region component variables
    [Header("Components")]
    //the character controller (helps with dealing with slopes and collision calculation)
    private CharacterController m_CharacterController = null;
    //the character collider (used for activating triggers in the scene)
    private CapsuleCollider m_CharacterCollider = null;
    //the camera rig (needed for calculating the position and rotation of the player) 
    private Transform m_CameraRig = null;
    //the head of the player (needed for calculating/adjusting the height of the player) 
    private Transform m_Head = null;
    #endregion

    //this contains the functions relating to gameplay e.g. start and update
    #region Core Functions
    //before the game start
    private void Awake()
    {
        //store the current character controller on this object (its located on this scripts holder)
        m_CharacterController = GetComponentInChildren<CharacterController>();
        //store the current character collider on this object (its located on this scripts holder)
        m_CharacterCollider = GetComponentInChildren<CapsuleCollider>();
    }

    //on the first frame
    private void Start()
    {
        //set the camera rig by looking for the top (currently rendering) origin.
        m_CameraRig = SteamVR_Render.Top().origin;
        //set the head by looking for the top (currently rendering) head.
        m_Head = SteamVR_Render.Top().head;
    }

    //every frame
    private void Update()
    {
        //set the height of the player
        HandleHeight();
        //calculate and apply the movement on the character controller
        CalculateMovement();


        //check and snap the rotation of the player when necessary
        //SnapRotation();
    }
    #endregion

    //this contains the functions relating to movement
    #region Movement

    //determines the height of the player and edits the collision/height of the player controller to the players height
    private void HandleHeight()
    {
        //calculate the heads current height (limited between 1 and 2 meters) (this is so it doesn't look/act weird if the player is too short/too tall)
        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        //set the character controllers height to the headheight
        m_CharacterController.height = headHeight;
        //set the characters collider height to the headheight
        m_CharacterCollider.height = headHeight;

        //set the offset (that will be applied) based on the players orientation and inputted offset 
        m_Offset = m_Orientation * m_OffsetAmount;

        //the center y value should be half of the character controllers height (to make sure it doesnt go through the ground) + offset (default = 0)
        m_Center.y = m_CharacterController.height / 2 + m_Offset.y;
        //add the skinwidth height to the equation (skinwidth is like an extra thickness/outline to help avoid precision issues)
        m_Center.y += m_CharacterController.skinWidth;
        //set the newcenters x value to the heads current local x position (this moves the collision capsule based on the players real world position) (also applies the offset) 
        m_Center.x = m_Head.localPosition.x + m_Offset.x;
        //set the newcenters z value to the heads current local z position (this moves the collision capsule based on the players real world position) (also applies the offset)
        m_Center.z = m_Head.localPosition.z + m_Offset.z;

        //apply the character controllers center value as the new center
        m_CharacterController.center = m_Center;
        //apply the character colliders center value as the new center
        m_CharacterCollider.center = m_Center;
    }


    //calculates and moves the player based on input and direction
    private void CalculateMovement()
    {
        //set the current y rotation as the current euler (calculate direction we are facing) ... perhaps test with x and z
        Vector3 orientationEuler = new Vector3(0, m_Head.eulerAngles.y, 0);
        //set up a movement vector (defaults to vector3.zero / (0,0,0) )
        Vector3 movement = Vector3.zero;
        //update the orientation of the player (updates direction to move)
        m_Orientation = Quaternion.Euler(orientationEuler);

        //if the Move Input reaches past a certain sensitivity (for the y axis)
        if (m_MoveValue.axis.y >= m_Sensitivity || m_MoveValue.axis.y <= -m_Sensitivity)
        {
            //sets the speed to the y input of the touchpad/analog (forward and backward)
            m_Speed = m_MoveValue.axis.y * m_MaxSpeed;
        }
        //if the move input does not reach past the sensitivity value
        else
        {
            //set the speed of the player to 0 
            m_Speed = 0;
        }
        //if the Move Input reaches past a certain sensitivity (for the x axis)
        if (m_MoveValue.axis.x >= m_Sensitivity || m_MoveValue.axis.x <= -m_Sensitivity)
        {
            //sets the speed to the x input of the touchpad/analog (Left and Right)
            m_Strafe = m_MoveValue.axis.x * m_MaxSpeed / 2;
        }
        else
        {
            //set the strafe speed of the player to 0
            m_Strafe = 0;
        }

        //clamp (lock) the speeds value between the minimum speed (-maxspeed / 2 for slower backwards walking) and max speed (forward and back)
        m_Speed = Mathf.Clamp(m_Speed, -m_MaxSpeed / 2, m_MaxSpeed);
        //clamp (lock) the strafes value of speed (this simply limits both sides to the max speed and nothing more)
        m_Strafe = Mathf.Clamp(m_Strafe, -m_MaxSpeed, m_MaxSpeed);

        //move forward based on the players last head direction overtime (this only returns the movement calculation... needs to be applied) 
        movement = m_Orientation * new Vector3(m_Strafe, 0, m_Speed);
        //apply gravity to the y axis of movement (note that gravity is multiplied with delta time twice to be applied as an acceleration(ms^-2)) ... (higher = faster fall)
        movement.y -= (9.81f * m_Gravity) * Time.deltaTime;

        //move the character controller based on the movement calculation)
        m_CharacterController.Move(movement * Time.deltaTime);
    }

    #region Unused Functions
   /*
    //snaps the players rotation when a button is pressed
    private void SnapRotation()
    {
        //the rotation value/angle to snap the player (default 0)
        float snapValue = 0.0f;

        //if the left controller snap rotation button was pressed
        if (m_RotatePress.GetStateDown(SteamVR_Input_Sources.Any))
        {
            //if the rotation direction input is towards the right
            if (m_RotateDirection.axis.x <= 0.1f)
            {
                //set the snap value to the positive rotation of the rotation angle
                snapValue = -Mathf.Abs(m_RotateIncrement);
            }
            //if the rotation direction input is towards the left
            else if(m_RotateDirection.axis.x >= -0.1f)
            {
                //set the snap value to the negative rotation of the rotation angle
                snapValue = Mathf.Abs(m_RotateIncrement);
            }
        }

        //rotate the transform around the y axis at the snap value angle
        transform.RotateAround(m_Head.position, Vector3.up, snapValue);
    }
    */

    #endregion

    #endregion
}
