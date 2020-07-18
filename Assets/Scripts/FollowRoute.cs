using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{

    [SerializeField]
    private Transform[] routes;
    public float speedModifier;
    public float routeSize;
    public Color routeColor;

    private int routeToGo;

    private float tParam;

    private Vector2 bloodPos;

    private bool coroutineAllowed;

    private Vector2 p0;
    private Vector2 p1;
    private Vector2 p2;
    private Vector2 p3;
    private Vector3 markerPos;


    // Init
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        //speedModifier = 0.5f;
        coroutineAllowed = true;
        p0 = routes[routeToGo].GetChild(0).position;
        p1 = routes[routeToGo].GetChild(1).position;
        p2 = routes[routeToGo].GetChild(2).position;
        p3 = routes[routeToGo].GetChild(3).position;
        drawPath();
    }


    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute(routeToGo));
    }
    private void drawPath()
    {
        for (float t = 0; t <= 1.05; t += 0.005f)
        {
            markerPos = Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 +
                3 * (1 - t) * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = markerPos;
            sphere.transform.localScale *= routeSize;
            Renderer renderer = sphere.GetComponent<Renderer>();
            renderer.material.color = routeColor;
        }
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            bloodPos = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2)
                * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = bloodPos;
            yield return new WaitForEndOfFrame();

        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;

        }
        coroutineAllowed = true;
    }

  
}
