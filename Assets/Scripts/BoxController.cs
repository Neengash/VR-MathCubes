using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    MeshRenderer meshRenderer;
    [SerializeField] Material[] materials;
    public int currentMaterial { get; private set; }

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        SetRandomMaterial();
    }

    public void ChangeMaterial() {
        NextMaterial();
        UpdateMaterial();
        CheckWinCondition();
    }

    public void SetMaterial(int i) {
        currentMaterial = i;
        UpdateMaterial();
    }

    public void SetRandomMaterial() {
        currentMaterial = Random.Range(0, materials.Length);
        UpdateMaterial();
    }

    private void CheckWinCondition() {
        if (GameManager.instance.CheckWinCondition()) {
            GameManager.instance.LevelComplete();
        }
    }

    private void UpdateMaterial() {
        meshRenderer.material = materials[currentMaterial];
    }

    private void NextMaterial(){
        currentMaterial++;
        currentMaterial %= materials.Length;
    }
}
