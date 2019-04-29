/*
Titulo: "SimuladorDeRiesgos"
Hecho en el año:2019 
-----
Title: "SimuladorDeRiesgos"
Made in the year: 2019
*/
using System.Collections.Generic;
using UnityEngine;

public class cod_CalidadEscenario : MonoBehaviour
{
    public cod_Enumeradores.CalidadGrafica calidad;
    public bool editorAltaCalidad, autoCalidad=true;

    #region Variables privadas
    float _deltaTime = 0.0f;
    List<float> ratio;
    bool guardarRatio = true;
    float fpsActual = 30;
    #endregion

    void Awake()
    {
        ratio = new List<float>();
    #if UNITY_EDITOR
        if(!editorAltaCalidad)
            QualitySettings.SetQualityLevel(0);
        else
            QualitySettings.SetQualityLevel((int)calidad);
    #else
         QualitySettings.SetQualityLevel((int)calidad);
    #endif
        InvokeRepeating("GuardandoDatos", 1, 5);
    }

    void Update()
    {
        if (!autoCalidad)
            enabled = false;

        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        float fps = 1.0f / _deltaTime;
      
        if (guardarRatio)
            ratio.Add(fps);
    }

    void GuardandoDatos()
    {
        guardarRatio = false;
        int cantidad = ratio.Count;
        float suma=0,promedio=0;
        for (int i = 0; i < cantidad; i++)
        {
            suma += ratio[i];
        }
        promedio = suma / cantidad;
        ratio.Clear();
        ComprobarCalidad(promedio);
        guardarRatio = true;
    }

    void ComprobarCalidad(float promedio)
    {
        if (fpsActual < 30)
        {
            if (promedio >= 45)
            {
                if(QualitySettings.GetQualityLevel() != 2)
                    QualitySettings.SetQualityLevel(2);
                fpsActual = 45;
            }
        }
        else
        {
            if (fpsActual >= 30 && fpsActual < 60)
            {
                if (promedio < 30)
                {
                    if (QualitySettings.GetQualityLevel() != 0)
                        QualitySettings.SetQualityLevel(0);
                    fpsActual = 15;
                }
                if(promedio>=90)
                {
                    if (QualitySettings.GetQualityLevel() != 5)
                        QualitySettings.SetQualityLevel(5);
                    fpsActual = 90;
                }
            }
            else
            {
                if (promedio < 60)
                {
                    if (QualitySettings.GetQualityLevel() != 2)
                        QualitySettings.SetQualityLevel(2);
                    fpsActual = 45;
                }
            }
        }
    }
}

 


