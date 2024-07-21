using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slingshot : MonoBehaviour
{
    public Demo demo;
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center, idlePosition;
    public Vector3 currentpositin, Hello;
    bool isMouseDown;
    public float maxLength, BallPositionoffset, Force, BottomBoubry;
    public GameObject BallPrefab, RestartPage, WinningPage, MenuPage;
    Rigidbody2D Ball;
    Collider2D Collider;
    public static bool Checking;
    public GameObject BallCircelPath;
    int i = 4;

    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        CreateBall();
    }

    void CreateBall()
    {
        Ball = Instantiate(BallPrefab).GetComponent<Rigidbody2D>();
        Collider = Ball.GetComponent<Collider2D>();
        Collider.enabled = false;

        Ball.isKinematic = true;
    }

    void Update()
    {
        if (Ball != null)
        {
            if (isMouseDown)
            {
                Vector3 mousepositon = Input.mousePosition;
                mousepositon.z = 10;

                currentpositin = Camera.main.ScreenToWorldPoint(mousepositon);
                currentpositin = center.position + Vector3.ClampMagnitude(currentpositin - center.position, maxLength);

                currentpositin = CheckBoundry(currentpositin);

                SetStrips(currentpositin);
                demo.CalculateTrajectory(currentpositin,(currentpositin - center.position) * -1, Force);

                if (Collider)
                {
                    Collider.enabled = true;
                }
            }
            else
            {
                ResetStrips();
                demo.ClearTrajectory();

            }
        }

        var BallCheck = GameObject.FindGameObjectsWithTag("Ball");
        var BallChecker = GameObject.FindGameObjectsWithTag("BallChecking");
        var Bottle = GameObject.FindGameObjectsWithTag("Bottle");

        if (Checking && BallCheck.Length == 0 && i > 0)
        {
            CreateBall();
            Destroy(BallChecker[i - 1]);
            Checking = false;
            i--;
        }

        //For Restart Page :---

        if (BallCheck.Length == 0 && BallChecker.Length == 0 && Bottle.Length > 0)
        {
            RestartPage.SetActive(true);
        }

        // For Winning Page :---

        if (BallCheck.Length == 1 && BallChecker.Length >= 0 && Bottle.Length == 0)
        {
            WinningPage.SetActive(true);
            int CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
            PlayerPrefs.SetString("status" + CurrentLevel, "Complete");
        }

    }

    private void OnMouseDown()
    {
        if (GameObject.FindGameObjectsWithTag("Ball").Length == 1)
        {
            isMouseDown = true;
        }

    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
        demo.ClearTrajectory();
        currentpositin = idlePosition.position;
    }

    void Shoot()
    {
        try
        {
            Ball.isKinematic = false;
            Vector3 BallForce = (currentpositin - center.position) * Force * -1;
            Ball.velocity = BallForce;
        }
        catch (NullReferenceException) { }

        Ball = null;
        Collider = null;
        ResetStrips();
    }

    void ResetStrips()
    {
        currentpositin = idlePosition.position;
        SetStrips(currentpositin);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (Ball)
        {
            Vector3 dir = position - center.position;
            Ball.transform.position = position + dir.normalized * BallPositionoffset;
            //Ball.transform.right = -dir.normalized;
        }
    }

    // For Maping Boundry 
    Vector3 CheckBoundry(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, BottomBoubry, 1000);

        return vector;
    }

    string I;

    public void Restart()
    {
        GetComponent<AudioSource>().Play();
        I = "Restart";
        Invoke("LoadScene", 0.10f);
    }

    public void Next()
    {
        GetComponent<AudioSource>().Play();
        I = "Next";
        Invoke("LoadScene", 0.10f);
    }

    public void MenuOn()
    {
        GetComponent<AudioSource>().Play();
        MenuPage.SetActive(true);
    }

    public void MenuOff()
    {
        GetComponent<AudioSource>().Play();
        MenuPage.SetActive(false);
    }
    
    public void AllLevels()
    {
        //Going To Menu Page For :--
        GetComponent<AudioSource>().Play();
        I = "AllLevels";
        Invoke("LoadScene", 0.10f);
    }

    void LoadScene()
    {
        if(I == "Restart")
        {
            int CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
            SceneManager.LoadScene("Level " + CurrentLevel);
        }
        else if (I == "AllLevels")
        {
            SceneManager.LoadScene("Levels");
        }
        else if(I == "Next")
        {
            int CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
            PlayerPrefs.SetInt("CurrentLevel", CurrentLevel + 1);

            PlayerPrefs.SetString("status" + (CurrentLevel + 1), "Complete");
            SceneManager.LoadScene("Level " + (CurrentLevel + 1));
        }
    }
}
