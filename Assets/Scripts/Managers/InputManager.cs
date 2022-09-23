/*
 * Author: Cristion Dominguez
 * Date: ???
 */

using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoSingleton<InputManager>
{
    private static PlayerInput playerInput;
    private static PlayerInputActionAsset playerInputActionAsset;

    protected override void Awake()
    {
        base.Awake();
        if (!GameObject.FindGameObjectWithTag("Player").TryGetComponent(out playerInput))
            Debug.LogError("PlayerInput component was not found.");
        
        playerInputActionAsset = new PlayerInputActionAsset();
        playerInputActionAsset.Player.Enable();
    }

    public static PlayerInputActionAsset.PlayerActions PlayerActions => playerInputActionAsset.Player;

    public static string CurrentControlScheme => playerInput.currentControlScheme;
}

