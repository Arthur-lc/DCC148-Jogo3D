using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField] private string cena = "Scenes/A";
    public void Jogar(){
        SceneManager.LoadScene(cena);
    }
    public void Sair(){
        Debug.Log("Saindo do jogo");
        Application.Quit();
    }
}
