﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputs inputs;

    [SerializeField] PlayerController playerController;

    private void Awake() => inputs = new PlayerInputs();
    private void OnDestroy() => inputs.Disable();
    private void OnDisable() => inputs.Disable();
    private void OnEnable() => inputs.Enable();

    // Update is called once per frame
    void Update()
    {
        playerController.playerMove = inputs.Player.Move.ReadValue<Vector2>();
    }
}