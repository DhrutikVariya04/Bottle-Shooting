using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpScript : MonoBehaviour
{
    public GameObject Dot, Location;
    int i = 0;
    void Start()
    {
        InvokeRepeating("Spawan", 2f, 0.1f);
        i++;
    }

    void Spawan()
    {
        if (i <= 25)
        {
            Instantiate(Dot, Location.transform);
            i++;
        }
        else
        {
            SceneManager.LoadScene("Home");
        }
    }
}
