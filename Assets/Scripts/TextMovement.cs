using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMovement : MonoBehaviour
{
    public Vector3 direction;
    public float minShrinkSpeed = 2;
    public float maxShrinkSpeed = 4;
    public float minSpeed = 2;
    public float maxSpeed = 4;
    private Vector3 scaleChange;
    private TextMeshPro textMeshComponent;

    // Update is called once per frame
    private void Start() {
        scaleChange = new Vector3(2.0f, 1.0f, 2.0f);
        textMeshComponent = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.Translate(direction * Random.Range(minSpeed, maxSpeed) * Time.deltaTime);
        textMeshComponent.fontSize -= Random.Range(minShrinkSpeed, maxShrinkSpeed) * Time.deltaTime;
        if(textMeshComponent.fontSize <= 0) {
            Destroy(this.gameObject);
        }
    }
}
