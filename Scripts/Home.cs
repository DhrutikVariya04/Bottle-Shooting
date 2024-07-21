using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Home : MonoBehaviour
{
    public new AudioListener audio;
    public AudioSource audioSource;
    public GameObject On, off;
    string subject = "Hey I am playing this awesome new game called Bottle Shooter,do give it try and enjoy \n";
    string body = "https://mega.nz/file/ULEUBTaa#qxbAHrfHYEwlaU1qqIYgCcZadbSSeCpIBeIRz1L0nP8";
    string Titel = "Bottle Shooter";

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.Play();
        Invoke("LoadScene", 0.10f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Levels");
    }

    public void SoundOn()
    {
        audio.enabled = false;
        On.SetActive(false);
        off.SetActive(true);
    }

    public void SoundOff()
    {
        audio.enabled = true;
        On.SetActive(true);
        off.SetActive(false);
    }

    public void Share()
    {
        audioSource.Play();
        //FindObjectOfType<AudioManager>().Play("Enter");
        StartCoroutine(ShareAndroidText());
    }

    IEnumerator ShareAndroidText()
    {
        yield return new WaitForEndOfFrame();

        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), Titel);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);

        //start the activity by sending the intent data
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");

        currentActivity.Call("startActivity", jChooser);
    }
}
