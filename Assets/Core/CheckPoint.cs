using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public event Action OnEnter;

    [SerializeField] private Material _material;
    private Material _lastMaterial;

    private MeshRenderer _meshRenderer;

    private void Awake() => _meshRenderer = GetComponent<MeshRenderer>();

    private void OnTriggerEnter(Collider other)
    {
        if (OnEnter?.GetInvocationList().Length > 0)
        {
            Debug.Log(name);
            transform.localScale = Vector3.one * 1.1f;
            OnEnter?.Invoke();
            _lastMaterial = _meshRenderer.material; 
            _meshRenderer.material = _material;
            _material = _lastMaterial;
        }
    }
}