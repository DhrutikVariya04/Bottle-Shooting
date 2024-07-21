using UnityEngine;
using UnityEngine.SceneManagement;

public class AllLevels : MonoBehaviour
{
    public GameObject All,Forest, Desert, Snowy;
    public AudioSource audioSource;
    string I;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Back()
    {
        audioSource.Play();
        I = "Back";
        Invoke("LoadScene", 0.10f);
    }


    public void Forforest()
    {
        audioSource.Play();
        I = "Forforest";
        Invoke("LoadScene", 0.10f);
    }

    public void Fordesert()
    {
        audioSource.Play();
        I = "Fordesert";
        Invoke("LoadScene", 0.10f);
    }

    public void Forsnowy()
    {
        audioSource.Play();
        I = "Forsnowy";
        Invoke("LoadScene", 0.10f);
    }

    void LoadScene()
    {
        if (I == "Back")
        {
            SceneManager.LoadScene("Home");
        }
        else if (I == "Forforest")
        {
            All.SetActive(false);
            Forest.SetActive(true);
        }
        else if (I == "Fordesert")
        {
            All.SetActive(false);
            Desert.SetActive(true);
        }
        else if(I == "Forsnowy")
        {
            All.SetActive(false);
            Snowy.SetActive(true);
        }
    }
}
