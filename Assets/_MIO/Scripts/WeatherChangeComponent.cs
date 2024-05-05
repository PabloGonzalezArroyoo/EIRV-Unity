using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeatherChangeComponent : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    [SerializeField] private Material SkyboxDay;
    [SerializeField] private Material SkyboxRain;
    [SerializeField] private Light directionalLight;
    [SerializeField] private ParticleSystem rainParticles1;
    [SerializeField] private ParticleSystem rainParticles2;
    [SerializeField] private ParticleSystem windParticles1;
    [SerializeField] private ParticleSystem windParticles2;
    
    [SerializeField] private AudioSource ambient;
    [SerializeField] private AudioClip natureClip;
    [SerializeField] private AudioClip rainClip;

    [SerializeField] private GameObject lightsParetn;

    bool rain = false;
    bool holding = false;

    private void changeWeather()
    {
        RenderSettings.skybox = rain ? SkyboxRain : SkyboxDay;
        directionalLight.intensity = rain ? 0.1f : 1f;

        var particles = rainParticles1.emission;
        particles.enabled = rain;
        particles = rainParticles2.emission;
        particles.enabled = rain;
        particles = windParticles1.emission;
        particles.enabled = !rain;
        particles = windParticles2.emission;
        particles.enabled = !rain;

        ambient.clip = rain ? rainClip : natureClip;
        ambient.Play();

        List<GameObject> list = new List<GameObject>();
        lightsParetn.GetChildGameObjects(list);
        foreach (GameObject g in list)
        {
            g.SetActive(rain);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        List<GameObject> list = new List<GameObject>();
        lightsParetn.GetChildGameObjects(list);
        foreach (GameObject g in list)
        {
            g.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grabInteractable.isSelected)
        {
            if (!holding) {
                rain = !rain;
                changeWeather();
                holding = true;
            }
        }
        else
        {
            holding = false;
        }
    }
}
