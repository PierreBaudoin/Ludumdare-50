using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    //public Room myRoom;
    public Material[] materialsWorkInProgress;
    public Material[] materialsNotWorking;
    public Material offMaterial;
    public MeshRenderer screenMesh;

    private Animator animator;
    private bool isInteracted = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetNotWorking()
    {
        Material mat = materialsNotWorking[Random.Range(0, materialsNotWorking.Length-1)];
        screenMesh.material = mat;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            SetWorking();
        }
        if (Input.GetKey(KeyCode.B))
        {
            SetNotWorking();
        }
        if(Input.GetKey(KeyCode.C))
        {
            CharacterLeave();
        }
    }

    public void SetWorking()
    {
        Material mat = materialsWorkInProgress[Random.Range(0, materialsNotWorking.Length - 1)];
        screenMesh.material = mat;
    }

    public void CharacterLeave()
    {
        Material mat = offMaterial;
        screenMesh.material = mat;
    }

    private void OnMouseDown()
    {
        animator.SetBool("ComputerTurned", true);
    }

    private void OnMouseUp()
    {
        animator.SetBool("ComputerTurned", false);
    }

    private void OnMouseOver()
    {
        animator.SetBool("MouseOver", true);
    }

    private void OnMouseExit()
    {
        animator.SetBool("MouseOver", false);
    }
}
