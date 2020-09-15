using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] AbilityLoadout _abilityLoadout;
    [SerializeField] Ability _startingAbility;
    [SerializeField] Ability _newAbilityToTest;

    [SerializeField] Transform _testTarget = null;

    public Transform CurrentTarget { get; private set; }

    private void Awake()
    {
        if(_startingAbility != null)
        {
            _abilityLoadout?.EquipAbility(_startingAbility);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        CurrentTarget = newTarget;
    }

    void Update()
    {
        //TODO in reality, Inputs would be detected elsewhere,
        //and passed into the Player class. We're doing it here
        //for simplification of example
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _abilityLoadout.UseEquippedAbility(CurrentTarget);
        }
        //equip new weapon
        if(Input.GetMouseButtonDown(0))
        {
            _abilityLoadout.EquipAbility(_newAbilityToTest);
        }

        //set a target, for testing
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //target ourselves in this case
            SetTarget(_testTarget);
        }
    }
}
