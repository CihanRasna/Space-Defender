using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scroolSpeed = 0.2f;
    [SerializeField] private Material myMaterial;
    private Vector2 offset;
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0 , scroolSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
