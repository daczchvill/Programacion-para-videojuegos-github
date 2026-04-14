using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private VidaRana vidaRana;
    private float vidaMaxima;

    void Start()
    {
        vidaRana = GameObject.Find("frog").GetComponent<VidaRana>();
        vidaMaxima = vidaRana.ObtenerVidaMaxima();

    }

    void Update()
    {
        if (vidaRana == null) return;

        rellenoBarraVida.fillAmount = vidaRana.ObtenerVidaActual() / vidaMaxima;
    }
}