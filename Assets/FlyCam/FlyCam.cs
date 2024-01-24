// This script was automatically created by script templates stored at C:\Program Files\Unity\Hub\Editor\{UnityVersion}\Editor\Data\Resources\ScriptTemplates
// Script created by LINEACT CESI
// Author: FlyCam

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace intervales.utils
{
    public class FlyCam : MonoBehaviour
    {
        #region own property, use camelLowerCase for private property, use CamelUpperCase for public property 
        private Vector2 look;
        private Vector2 move;
        private InputAction fire2Action, fire3Action, moveFastAction;

        public Camera cam;

        public float RotXSensitivity = 1.0f;
        public float RotYSensitivity = 1.0f;

        public float MoveXSensitivity = 1.0f;
        public float MoveYSensitivity = 1.0f;


        public float AltXSensitivity = 1.0f;
        public float AltYSensitivity = 1.0f;
        public float MoveFastMultiplier = 2.0f;

        public InputActionAsset inputActions;
        #endregion



        #region Unity Events
        // Start is called before the first frame update
        void Start()
        {
            if (inputActions == null) inputActions = GetComponent<PlayerInput>().actions;
            look = Vector2.zero;
            cam = transform.Find("Camera").gameObject.GetComponent<Camera>();


            fire2Action = inputActions.FindAction("Fire2");
            fire3Action = inputActions.FindAction("Fire3");
            moveFastAction = inputActions.FindAction("MoveFast");

        }

        //var action = inputActions.FindAction("Fire");
        //// Have it run your code when the Action is triggered.
        //action.performed += Fire_performed;
        //// Start listening for control changes.
        //action.Enable();
        //private void Fire_performed(InputAction.CallbackContext obj)
        //{
        //    Debug.Log($"Fire {obj}");
        //}

        

        // Update is called once per frame
        void Update()
        {
            
        }

        void FixedUpdate()
        {
            //Debug.Log($"Fire {inputActions.FindAction("Fire").IsPressed()}");
            //if(cam != null && look != Vector2.zero && Mouse.current.rightButton.isPressed)
            float multiplier = moveFastAction.IsPressed() ? MoveFastMultiplier : 1;
            if (cam != null && look != Vector2.zero && fire2Action.IsPressed())
            {
                var v = Vector3.zero;
                var xAngle = -look.y * RotXSensitivity *multiplier *  Time.fixedDeltaTime;
                var yAngle = look.x * RotYSensitivity * multiplier * Time.fixedDeltaTime;

                // Free Look
                transform.Rotate(0, yAngle, 0, Space.Self);
                cam.transform.Rotate(xAngle, 0, 0, Space.Self);
                //cam.transform.rotation *= Quaternion.Euler(v);
                //cam.transform.eulerAngles += v;

                /*transform.rotation *= Quaternion.FromToRotation(cam.transform.forward, (cam.transform.forward +
                    cam.transform.right * look.x * RotYSensitivity * Time.fixedDeltaTime + 
                    cam.transform.up * look.y * RotXSensitivity * Time.fixedDeltaTime).normalized );*/
            }
            
            

            if (cam != null && look != Vector2.zero && fire3Action.IsPressed())
            {
                var v = Vector3.zero;

                v.x = -look.x * AltXSensitivity* multiplier * Time.fixedDeltaTime;
                v.y = -look.y * AltYSensitivity * multiplier * Time.fixedDeltaTime;

                v = cam.transform.TransformDirection(v);
                transform.position += v;
            }

            if (cam != null && move != Vector2.zero)
            {
                var v = Vector3.zero;
                
                v.x = move.x * MoveXSensitivity * multiplier * Time.fixedDeltaTime;
                v.z = move.y * MoveYSensitivity * multiplier * Time.fixedDeltaTime;
                
                v = cam.transform.TransformDirection(v);
                transform.position += v;
            }
        }
        #endregion Unity Events


        #region own methods, use camelLowerCase for private method name, use CamelUpperCase for public method name 

        #endregion

        #region own events
        public void OnLook(InputValue value)
        {
            // Give the delta between previous value
            look = value.Get<Vector2>();
            //Debug.Log(string.Format("OnLook {0}", look));
        }

        public void OnMove(InputValue value)
        {
            move = value.Get<Vector2>();
            //Debug.Log(string.Format($"OnMove {move}"));
        }

        #endregion
    }
}