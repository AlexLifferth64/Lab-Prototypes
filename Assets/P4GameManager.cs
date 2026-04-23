using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P4GameManager : MonoBehaviour
{
    public static P4GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI victoryText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        P4GameManager.Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }

    public void UpdateAmmoText(int a)
    {
        ammoText.text = "Ammo: " + a;
    }

    public void Victory()
    {
        victoryText.gameObject.SetActive(true);
    }
}
