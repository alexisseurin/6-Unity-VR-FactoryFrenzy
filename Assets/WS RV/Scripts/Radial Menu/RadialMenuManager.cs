using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadialMenuManager : MonoBehaviour
{


    public InputActionReference activateRef;
    public InputActionReference touchPositionRef;
    public InputActionReference selectRef;

    private InputAction activate;
    private InputAction touchPosition;
    private InputAction select;

    public RadialMenu radialMenu;

    [SerializeField]
    private List<GameObject> objectToDisable;

    private void Awake()
    {
        activate = activateRef.action;
        touchPosition = touchPositionRef.action;
        select = selectRef.action;


        activate.Enable();
        touchPosition.Enable();
        select.Enable();

        activate.performed += Activate;
        touchPosition.performed += TouchPosition;
        select.performed += Select;
    }

    private void OnDestroy()
    {
        activate.performed -= Activate;
        touchPosition.performed -= TouchPosition;
        select.performed -= Select;
    }

    private void Select(InputAction.CallbackContext obj)
    {
        radialMenu.ActivateHighlightedSection();
    }

    private void TouchPosition(InputAction.CallbackContext obj)
    {
        radialMenu.SetTouchPosition(obj.ReadValue<Vector2>());
    }

    private void Activate(InputAction.CallbackContext obj)
    {
        radialMenu.Show(obj.ReadValueAsButton());
        foreach(GameObject go in objectToDisable)
        {
            go.SetActive(false);
        }
    }

    public void CloseMenu()
    {
        foreach (GameObject go in objectToDisable)
        {
            go.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
