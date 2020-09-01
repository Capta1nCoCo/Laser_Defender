using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offSet;

    void Start()
    {
        myMaterial = GetComponent<MeshRenderer>().material;
        offSet = new Vector2(0, backgroundScrollSpeed);
    }

    void Update()
    {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
