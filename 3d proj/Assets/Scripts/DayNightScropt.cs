using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightScropt : MonoBehaviour
{
    [SerializeField]
    private Material daySkybox;
    [SerializeField]
    private Material nightSkybox;

    private Light _sun;    // Directional light 1
    private Light _moon;   // Directional light 0.5

    const float _fullDayTime = 30f;   // ������ �� �������� ����
    const float _deltaAngle = -360 / _fullDayTime;   // ���� �������� ����� �� 1 �

    float _dayTime;   // ������� �����
    float _dayPhase;  // ���� ����� ����������� � ��������� [0..1]

    private AudioSource _daySound;
    private AudioSource _nightSound;

    void Start()
    {
        _sun = GameObject.Find("SunLight").GetComponent<Light>();
        _moon = GameObject.Find("MoonLight").GetComponent<Light>();
        AudioSource[] audioSources = this.GetComponents<AudioSource>();
        _daySound = audioSources[0];
        _nightSound = audioSources[1];
    }

    void LateUpdate()
    {
        _dayTime += Time.deltaTime;
        _dayTime %= _fullDayTime;
        _dayPhase = _dayTime / _fullDayTime;

        _sun.transform.Rotate(_deltaAngle * Time.deltaTime, 0, 0);
        _moon.transform.Rotate(_deltaAngle * Time.deltaTime, 0, 0);

        float koef = Mathf.Abs(Mathf.Cos(_dayPhase * 2f * Mathf.PI));
        // ����. �������� �������� ������� � ���������

        if (_dayPhase > 0.25f && _dayPhase < 0.75f)  // ������ ����
        {
            if(!_nightSound.isPlaying)
            {
                _nightSound.Play();
                _daySound.Stop();
            }

            if (RenderSettings.skybox != nightSkybox)
            {
                RenderSettings.skybox = nightSkybox;
            }
            RenderSettings.ambientIntensity = koef / 2f;     // ���� ���� (��������������)
            RenderSettings.skybox.SetFloat("_Exposure", 0.1f + koef * 0.9f);  // ������� (���������) ����� ��������
        }
        else  // ������� ����
        {
            if (!_daySound.isPlaying)
            {
                _daySound.Play();
                _nightSound.Stop();
            }

            if (RenderSettings.skybox != daySkybox)
            {
                RenderSettings.skybox = daySkybox;
            }
            RenderSettings.ambientIntensity = koef;
            RenderSettings.skybox.SetFloat("_Exposure", 0.1f + koef * 0.9f);
            _daySound.mute = GameSettings.IsMuted;
            _nightSound.mute = GameSettings.IsMuted;
            _daySound.volume = _nightSound.volume = GameSettings.BackgroundVolume;
        }
    }
}
