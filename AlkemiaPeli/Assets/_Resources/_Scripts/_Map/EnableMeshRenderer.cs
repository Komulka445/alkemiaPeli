using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMeshRenderer : MonoBehaviour
{
    public MeshRenderer _renderer;
    public bool isActive;

    private void Start() {
        isActive = true;
    }

    private void Update() {
        if (isActive == true)
        {
            _renderer.enabled = true;
        }
        else {_renderer.enabled = false; }
    }
}
