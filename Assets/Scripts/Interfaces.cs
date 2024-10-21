using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: See if there's a better way of doing this

public interface IInteract
{
    string interactPrompt {get;}
    void Interact();
}

public interface ITakeDamage
{
    void TakeDamage(float damageAmount);
}