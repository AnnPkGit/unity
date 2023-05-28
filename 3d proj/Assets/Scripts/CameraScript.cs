using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject _character;
    private Vector3 _offset;
    private float _angleX;
    private float _angleY;
    private float _sensX = 150;
    private float _sensY = 100;

    void Start()
    {
        _character = GameObject.Find("Character");
        _offset = this.transform.position - _character.transform.position;
        _angleX = 0;
        _angleY = 0;
        _angleY = this.transform.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        _angleX += mx * Time.deltaTime * _sensX;
        var yPostion = my * Time.deltaTime * _sensY;
        if ((_angleY - yPostion) < 45 && (_angleY - yPostion) > -45)
        {
            _angleY -= yPostion;
        }
    }

    private void LateUpdate()
    {
        this.transform.position = _character.transform.position +
            Quaternion.Euler(0, _angleX, 0) * _offset ;
        
        this.transform.eulerAngles = new Vector3(_angleY, _angleX, 0);
        
        if(! Input.GetMouseButton(0))
        {
            _character.transform.eulerAngles = new Vector3(0, _angleX, 0);
        }
    }
}
