using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : InteractableBase
{
    public MeshRenderer renderer;
    public Material AfterInteractMaterial;
    public Material AfterEnterMaterial;

    private Material BeforeInteractMaterial;

    private void OnEnable()
    {
        BeforeInteractMaterial = renderer.material;
    }
    public override void OnPlayerEnter()
    {
        renderer.material = AfterEnterMaterial;
        base.OnPlayerEnter();
    }
    public override void OnPlayerExit()
    {
        renderer.material = BeforeInteractMaterial;
        base.OnPlayerExit();
    }
    public override void Interact()
    {
        base.Interact();
        renderer.material = AfterInteractMaterial;



    }
}
