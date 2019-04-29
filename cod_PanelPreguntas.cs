/*
Titulo: "SimuladorDeRiesgos"
Hecho en el año:2019 
-----
Title: "SimuladorDeRiesgos"
Made in the year: 2019
*/
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cod_PanelPreguntas : cod_PanelBase
{
    public bool[] completado;
    public cod_Enumeradores.CarpetasRiesgos tipoDeRiesgo;
    public int cantidadDePreguntas;
    public Image botonAceptar;
    public GameObject respuestaIncorrecta;

    #region variables privadas
    cod_PreguntasDatos[] _datosPreguntas;
    TextMeshProUGUI[] _textos;
    int _preguntasIndice;
    #endregion

    void Start()
    {
        AsignarVariables();
        AsignarValoresIniciales();
        AsignarLugarDePreguntas(cod_Enumeradores.CarpetasRiesgos.Preg_Inicio, 1);
    }

    /// <summary>
    /// Realiza todas las configuraciones necesarias para cambiar de un tipo de preguntas a otro.
    /// </summary>
    /// <param name="tipo_ar"></param>
    /// <param name="cantidad_ar"></param>
    public void AsignarLugarDePreguntas(cod_Enumeradores.CarpetasRiesgos tipo_ar, int cantidad_ar)
    {
        tipoDeRiesgo = tipo_ar;
        cantidadDePreguntas = cantidad_ar;

        _datosPanelPreguntas = Resources.Load("Datos/scr_PanelPreguntas") as cod_PanelPreguntasDatos;
        if (_datosPanelPreguntas == null)
            Debug.LogError("Datos panel preguntas no encontrado o fue renombrado");

        _datosPreguntas = new cod_PreguntasDatos[cantidadDePreguntas];
        for (int i = 0; i < _datosPreguntas.Length; i++)
        {
            _datosPreguntas[i] = Resources.Load("Datos/" + tipoDeRiesgo + "/Pregunta_" + i) as cod_PreguntasDatos;
        }
        if (_datosPreguntas[0] == null)
            Debug.LogError("Datos preguntas no encontrado o fue renombrado");

        _textos = new TextMeshProUGUI[7];
        for (int i = 1; i < 7; i++)
        {
            _textos[i] = panelesDeSeleccion[i - 1].transform.parent.gameObject.GetComponent<TextMeshProUGUI>();
        }
        _textos[0] = _textos[1].transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>();

        ResetearSeleccion();
        _numSeleccionActual = 0;
        _preguntasIndice = 0;
        CambiarSeleccionPanel(0);
        AsignarValoresPreguntas();
    }

    /// <summary>
    /// Asigna las variables que requiere el codigo para funcionar
    /// </summary>
    protected override void AsignarVariables()
    {
        base.AsignarVariables();

        _datosPreguntas = new cod_PreguntasDatos[cantidadDePreguntas];
        for (int i = 0; i < _datosPreguntas.Length; i++)
        {
            _datosPreguntas[i] = Resources.Load("Datos/" + tipoDeRiesgo + "/Pregunta_" + i) as cod_PreguntasDatos;
        }
        if (_datosPreguntas[0] == null)
            Debug.LogError("Datos preguntas no encontrado o fue renombrado");
    }

    /// <summary>
    /// Asigna los valores iniciales que requiere el codigo para funcionar
    /// </summary>
    protected override void AsignarValoresIniciales()
    {
        base.AsignarValoresIniciales();

        _textos = new TextMeshProUGUI[7];
        for (int i = 1; i < 7; i++)
        {
            _textos[i] = panelesDeSeleccion[i - 1].transform.parent.gameObject.GetComponent<TextMeshProUGUI>();
        }
        _textos[0] = _textos[1].transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Asigna los textos segun los datos de la pregunta escogida.
    /// </summary>
    /// <param name="eleccion"></param>
    void AsignarValoresPreguntas()
    {
        if (_preguntasIndice < cantidadDePreguntas)
        {
            _textos[0].text = _datosPreguntas[_preguntasIndice].pregunta;
            _textos[1].text = _datosPreguntas[_preguntasIndice].opcionA;
            _textos[2].text = _datosPreguntas[_preguntasIndice].opcionB;
            _textos[3].text = _datosPreguntas[_preguntasIndice].opcionC;
            _textos[4].text = _datosPreguntas[_preguntasIndice].opcionD;
           
            _preguntasIndice++;
            ResetearSeleccion();
        }
        else
        {
            //aca es cuando no hay mas preguntas
            cod_PreguntasInteraccion personaje = FindObjectOfType<cod_PreguntasInteraccion>();
            personaje.ComenzarCambiandoColor();
            switch (tipoDeRiesgo)
            {
                case cod_Enumeradores.CarpetasRiesgos.Preg_Altura:
                    completado[0] = true;
                    break;
                case cod_Enumeradores.CarpetasRiesgos.Preg_Caliente:
                    completado[1] = true;
                    break;

                case cod_Enumeradores.CarpetasRiesgos.Preg_Confinamiento:
                    completado[2] = true;
                    break;
                case cod_Enumeradores.CarpetasRiesgos.Preg_Electrico:
                    completado[3] = true;
                    break;
            }
        }
    }

    /// <summary>
    /// Resetea las selecciones cuando pasas a una nueva pregunta.
    /// </summary>
    public override void ResetearSeleccion()
    {
        base.ResetearSeleccion();
        botonAceptar.sprite = _datosPanelPreguntas.botonDeseleccionado;
    }

    void Update()
    {
        ComprobarEntrada();
    }

    /// <summary>
    /// Comprueba la entrada de los mandos para el cambio de seleccion
    /// </summary>
    protected override void ComprobarEntrada(bool horizontal=false)
    {
        base.ComprobarEntrada();
    }


    /// <summary>
    /// Forma de incrementar o decrementar la seleccion.
    /// </summary>
    /// <param name="incremento"></param>
    protected override void ElegirSeleccionActual(bool incremento)
    {
        if (incremento)
        {
            if (_numSeleccionActual < panelesDeSeleccion.Length)
                _numSeleccionActual++;
            else
                _numSeleccionActual = 0;
        }
        else
        {
            if (_numSeleccionActual >= 0)
                _numSeleccionActual--;
            else
                _numSeleccionActual = panelesDeSeleccion.Length - 1;
        }
    }

    /// <summary>
    /// Comprueba si el panel ya fue seleccionado, o si esta siento seleccionado.
    /// </summary>
    /// <param name="eleccion"></param>
    protected override void CambiarSeleccionPanel(int eleccion)
    {
        if (_datosPanelPreguntas == null)
        {
            Debug.LogError("Fatal error");
        }

        if (_numSeleccionActual == panelesDeSeleccion.Length ||
            _numSeleccionActual == -1)
        {
            botonAceptar.sprite = _datosPanelPreguntas.botonSeleccionado;
        }
        else
        {
            botonAceptar.sprite = _datosPanelPreguntas.botonDeseleccionado;
        }
    }

    /// <summary>
    /// Selecciona o deselecciona un panel.
    /// </summary>
    /// <param name="eleccion"></param> 
    protected override void SeleccionarOpcion(int eleccion)
    {
        if (eleccion == panelesDeSeleccion.Length ||
           _numSeleccionActual == -1)
        {
            botonAceptar.sprite = _datosPanelPreguntas.botonPulsado;
            ComprobarRespuesta();
            return;
        }
    }

    /// <summary>
    /// Comprobar las respuestas correctas.
    /// </summary>
    void ComprobarRespuesta()
    {
        int respuestasCorrectas=0, respuestas=0;
        for (int i = 0; i < panelesDeSeleccion.Length; i++)
        {
            if (panelesDeSeleccion[i].estado == cod_Enumeradores.Seleccion.SobreDeseleccionado)
            {
                respuestas++;

                if (_textos[i + 1].text == _datosPreguntas[_preguntasIndice-1].respuestaCorrecta)
                    respuestasCorrectas++;
                
            }
        }
        if (respuestasCorrectas == 1 &&
            respuestas == respuestasCorrectas)
        {
            AsignarValoresPreguntas();
        }
        else
        {
            MostrarRespuestaIncorrecta();
            ResetearSeleccion();
        }
    }

    /// <summary>
    /// Mostrar cartel del respuesta incorrecta
    /// </summary>
    void MostrarRespuestaIncorrecta()
    {
        respuestaIncorrecta.SetActive(true);
        Invoke("OcultarRespuestaIncorrecta", 1.2f);
    }

    /// <summary>
    /// Ocultar cartel de respuestas incorrectas.
    /// </summary>
    void OcultarRespuestaIncorrecta()
    {
        respuestaIncorrecta.SetActive(false);
    }
}
