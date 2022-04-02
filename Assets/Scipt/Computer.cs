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


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetWorking();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SetNotWorking();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CharacterLeave();
        }
    }

    public void SetNotWorking()
    {
        Material mat = materialsNotWorking[Random.Range(0, materialsNotWorking.Length-1)];
        screenMesh.material = mat;
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
}