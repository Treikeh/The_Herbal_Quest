using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteract
{
    string interactPrompt {get;}
    void Interact();
}

public interface ITakeDamage
{
    void TakeDamage(float damageAmount);
}