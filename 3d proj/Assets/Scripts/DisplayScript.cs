using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    [SerializeField]
    private GameObject character;

    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private TMPro.TextMeshProUGUI textMeshPro;

    private float red;
    private float blue;

    void Start()
    {
        red = 255;
        blue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(
            character.transform.position,
        coin.transform.position);

        textMeshPro.text = distance.ToString("0.0");
        if (red >= 0)
        {
            red -= distance * 2;
        }
        if (blue <= 255) { 
            blue += distance * 2; 
        }
        textMeshPro.color = new Color(red, 0, blue);
    }
}
