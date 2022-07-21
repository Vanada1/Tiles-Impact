using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMoving : MonoBehaviour
{
    private float _offset;

    private Material _material;

	[Range(-1f, 1f)]
    public float ScrollSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        var render = GetComponent<Image>();
        _material = render.material;
    }

    // Update is called once per frame
    void Update()
    {
        _offset += (Time.deltaTime * ScrollSpeed) / 10f;
        _material.SetTextureOffset("_MainTex", new Vector2(_offset, 0));
    }
}
