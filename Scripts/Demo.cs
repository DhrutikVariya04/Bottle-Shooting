using System;
using UnityEngine;

public class Demo : MonoBehaviour
{

    public int numPoints; // Number of points to predict
    public float timeBetweenPoints; // Time interval between points
    public GameObject trajectoryPointPrefab; // Prefab for trajectory point visualization
    public bool isDrawLine;

    private GameObject[] trajectoryPoints;
    private LineRenderer lineRenderers;

    private void Start()
    {
        lineRenderers = GetComponent<LineRenderer>();
        lineRenderers.positionCount = numPoints;

        trajectoryPoints = new GameObject[numPoints];
        var scale = new Vector3(.12f, .12f, .12f);
        for (int i = 0; i < numPoints; i++)
        {
            //trajectoryPoints[i] = Instantiate(trajectoryPointPrefab, transform.position, Quaternion.identity);
            trajectoryPoints[i] = Instantiate(trajectoryPointPrefab, transform);
            trajectoryPoints[i].transform.localScale = (numPoints - i) * scale;
        }
    }

    public void CalculateTrajectory(Vector2 currentPosition, Vector2 direction, float launchForce)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < numPoints; i++)
        {
            float time = i * timeBetweenPoints;
            Vector2 position = (Vector2)currentPosition + (direction * launchForce * time) + 0.5f * Physics2D.gravity * (time * time);
            trajectoryPoints[i].transform.position = position;
        }

        if (isDrawLine)
        {
            lineRenderers.SetPositions(Array.ConvertAll(trajectoryPoints, point => point.transform.position));
        }
    }

    public void ClearTrajectory()
    {
        gameObject.SetActive(false);
    }

}
