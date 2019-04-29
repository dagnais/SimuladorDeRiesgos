/*
Titulo: "SimuladorDeRiesgos"
Hecho en el año:2019 
-----
Title: "SimuladorDeRiesgos"
Made in the year: 2019
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cod_IAJames : MonoBehaviour
{
    public Transform indicador;
    public Transform[] puntos;
    Transform _objetivo;
    int _index;
    bool _lerp;
    float _rotacion;
    
    void Start()
    {
        _index = 0;
        _objetivo = puntos[_index];
    }

    void Update()
    {
        MirarObjetivo();
        ComprobarPuntos();
    }

    void MirarObjetivo()
    {
        indicador.transform.LookAt(_objetivo);
        if (_lerp)
            Lerp();
    }

    void Lerp()
    {
        _rotacion += 0.01f * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, indicador.rotation, _rotacion);
        if (_rotacion >= .5f)
        {
            Debug.Log("Rot: " + _rotacion);
            _lerp = false;
            _rotacion = 0;
        }   
    }

    void ComprobarPuntos()
    {
        float distancia = Vector3.Distance(transform.position, _objetivo.position);
        if (distancia < 1)
        {
            _lerp = true;
            _rotacion = 0;
            _index++;
            if (_index == puntos.Length)
                _index = 0;

            _objetivo = puntos[_index];
        }
    }
}
