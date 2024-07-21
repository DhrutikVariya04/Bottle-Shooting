using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsScript : MonoBehaviour
{
    public GameObject LevelsBottons, SnowyButton, All, forest, desert, snowy;
    public Sprite Lock;
    public Transform Grid;
    Button b;
    public AudioSource audioSource;

    [System.Obsolete]
    void Start()
    {
        audioSource = GetComponent <AudioSource>();
        GameObject button = snowy.active ? SnowyButton : LevelsBottons;

        for (int i = 1; i <= 10; i++)
        {
            b = Instantiate(button, Grid.transform).GetComponent<Button>();

            int level = 0;
            if (forest.active)
            {
                level = i;
            }
            else if (desert.active)
            {
                level = i + 10;
            }
            else if (snowy.active)
            {
                level = i + 20;
            }

            string status = PlayerPrefs.GetString("status" + level, "panding");

            if (i == 1 || status == "Complete" && i != 1)
            {
                b.GetComponentInChildren<Text>().text = i.ToString();
            }
            else
            {
                b.GetComponentInChildren<Image>().sprite = Lock; // Lock is sprite variable
                b.GetComponentInChildren<Text>().text = "";
            }

            b.onClick.AddListener(() =>
            {
                if (status == "Complete" || level == 1 || level == 11 || level == 21)
                {
                    SceneManager.LoadScene("Level " + level);
                    PlayerPrefs.SetInt("CurrentLevel", level);
                }
            });
        }

    }

    public void Restart()
    {
        audioSource.Play();
        Invoke("LoadScene", 0.10f);
    }
    void LoadScene()
    {
        All.SetActive(true);
        forest.SetActive(false);
        desert.SetActive(false);
        snowy.SetActive(false);
    }
}
