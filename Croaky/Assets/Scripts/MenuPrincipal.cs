using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena
using UnityEngine.Audio;

public class MenuPrincipal : MonoBehaviour
{
    public AudioMixer masterMixer; // Arrastra aquí tu MasterMixer
    private bool estaSilenciado = false;

    // Esta variable guardará la referencia a tu panel
    public GameObject Instrucciones; 

    // Función para mostrar el tutorial
    public void AbrirInstrucciones()
    {
        Instrucciones.SetActive(true);
    }

    // Función para ocultar el tutorial
    public void CerrarInstrucciones()
    {
        Instrucciones.SetActive(false);
    }


    public void AlternarSonido()
    {
        estaSilenciado = !estaSilenciado;

        if (estaSilenciado)
        {
            // -80 decibelios es silencio total en Unity
            masterMixer.SetFloat("MyExposedVolume", -80f);
        }
        else
        {
            // 0 decibelios es el volumen normal
            masterMixer.SetFloat("MyExposedVolume", 0f);
        }
    }

    public void CambiarCalidad(int indice)
{
    // Cambia el nivel de calidad del motor gráfico
    QualitySettings.SetQualityLevel(indice);
    
    Debug.Log("Calidad cambiada a nivel: " + indice);
}


    public void Jugar()
    {
        // Carga la escena de tu juego. Asegúrate de que el nombre coincida.
        // En tu caso, según image_ea5f61.png, tu escena se llama "Croaky".
        SceneManager.LoadScene("Croaky");
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit(); // Esto funciona en el juego exportado (.exe)
    }
}
