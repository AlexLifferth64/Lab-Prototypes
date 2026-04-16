using System.Collections;
using TMPro;
using UnityEngine;

public class TycoonGameManager : MonoBehaviour
{
    int squares = 0;
    int triangles = 0;
    int circles = 0;
    int population = 0;


    [SerializeField] private TextMeshProUGUI squaresText;
    [SerializeField] private TextMeshProUGUI trianglesText;
    [SerializeField] private TextMeshProUGUI circlesText;

    [SerializeField] private TextMeshProUGUI populationText;

    [SerializeField] LayerMask defaultLayer;

    [SerializeField] private GameObject house;
    [SerializeField] private GameObject neighborhood;
    [SerializeField] private GameObject building;
    [SerializeField] private GameObject castle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos, defaultLayer);

            if (hitCollider != null)
            {
                if (hitCollider.name == "Square")
                {
                    squares++;
                }
                if (hitCollider.name == "Triangle")
                {
                    if(TryPurchase(10, 0, 0))
                        triangles++;
                }
                if (hitCollider.name == "Circle")
                {
                    if (TryPurchase(0, 100, 0))
                        circles++;
                }
                if(hitCollider.name == "Square Worker")
                {
                    if(TryPurchase(10,0,0))
                    {
                        PurchaseSquareWorker();
                    }
                }
                if (hitCollider.name == "Triangle Worker")
                {
                    if (TryPurchase(1000, 0, 0))
                    {
                        PurchaseTriangleWorker();
                    }
                    else if (TryPurchase(0, 100, 0))
                    {
                        PurchaseTriangleWorker();
                    }
                }
                if (hitCollider.name == "Circle Worker")
                {
                    if (TryPurchase(0, 10000, 0))
                    {
                        PurchaseTriangleWorker();
                    }
                    if (TryPurchase(0, 0, 1000))
                    {
                        PurchaseCircleWorker();
                    }
                }

            }

        }

        if (population == 10)
        {
            house.SetActive(true);
        }
        else if (population == 100)
        {
            neighborhood.SetActive(true);
        }
        else if (population == 1000)
        {
            building.SetActive(true);
        }
        else if (population == 10000)
        {
            castle.SetActive(true);
        }

        UpdateText();
    }

    private IEnumerator SquareWorker()
    {
        yield return new WaitForSeconds(1.5f);
        squares++;
        StartCoroutine(SquareWorker());
    }

    private void PurchaseSquareWorker()
    {
        population++;
        StartCoroutine(SquareWorker());
    }

    private IEnumerator TriangleWorker()
    {
        yield return new WaitForSeconds(3f);
        triangles++;
        StartCoroutine(TriangleWorker());
    }

    private void PurchaseTriangleWorker()
    {
        population++;
        StartCoroutine(TriangleWorker());
    }

    private IEnumerator CircleWorker()
    {
        yield return new WaitForSeconds(5);
        triangles++;
        StartCoroutine(CircleWorker());
    }

    private void PurchaseCircleWorker()
    {
        population++;
        StartCoroutine(CircleWorker());
    }

    private bool TryPurchase(int s, int t, int c) // the parameters are the amount
    {
        if(s <= squares)
        {
            squares -= s;

            if(t <= triangles)
            {
                triangles -= t;

                if(c <= circles)
                {
                    circles -= c;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        
    }

    private void SubtractTriangle()
    {
        triangles--;
    }
    private void SubtractCircle()
    {
        circles--;
    }

    private void UpdateText()
    {
        squaresText.text = squares + "";
        trianglesText.text = triangles + "";
        circlesText.text = circles + "";
        populationText.text = "Population: " + population;
    }

}
