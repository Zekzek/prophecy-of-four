using System;
using System.Collections.Generic;
using UnityEngine;

public class InputRouter : MonoBehaviour
{
    private const float DEAD_ZONE = 0.1f;
    public enum InputAxis { Horizontal, Vertical, Jump }

    [SerializeField]
    private CharacterBehaviour characterController;

    [SerializeField]
    private Character testAttacker;

    [SerializeField]
    private Thing testDefender;
    //public ResourceFountain testWaterFountain;

    private Dictionary<InputAxis, float> lastInput = new Dictionary<InputAxis, float>();
    private Dictionary<InputAxis, float> currentInput = new Dictionary<InputAxis, float>();

    void Start()
    {

    }

    void Update()
    {
        foreach (InputAxis inputAxis in Enum.GetValues(typeof(InputAxis)))
            UpdateInputValue(inputAxis);

        if (IsFirstDown(InputAxis.Jump))
            characterController.UseAttack(FileManager.GetAbilityData("slap"), testAttacker, testDefender);
        //if (testAttacker.Thirst != SurvivalResource.Satiation.Content)
        //    testAttacker.AddResource(Resource.ResourceType.Water, testWaterFountain.Draw());

        characterController.Move(currentInput[InputAxis.Horizontal], currentInput[InputAxis.Vertical]);
    }

    private void UpdateInputValue(InputAxis inputAxis)
    {
        float value = Input.GetAxis(Enum.GetName(typeof(InputAxis), inputAxis));

        if (value < DEAD_ZONE && value > -DEAD_ZONE)
            value = 0;

        lastInput[inputAxis] = lastInput.ContainsKey(inputAxis) ? currentInput[inputAxis] : 0;
        currentInput[inputAxis] = value;
    }

    private bool IsFirstDown(InputAxis inputAxis)
    {
        return lastInput[inputAxis] == 0 && currentInput[inputAxis] != 0;
    }

    private bool IsFirstUp(InputAxis inputAxis)
    {
        return lastInput[inputAxis] != 0 && currentInput[inputAxis] == 0;
    }
}
