using Unity.VisualScripting;
using UnityEngine;

public class QuestionblockAnim : MonoBehaviour
{
    private Material _material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _material = GetComponent<MeshRenderer>().GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        //_material.SetTextureOffset(0.2f);
    }
}
