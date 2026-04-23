using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Rigidbody2D rb;
    private LineRenderer lr;

    private Transform pivot;
    private DistanceJoint2D dj;

    [SerializeField] private float gunForce = 150;

    private int ammo = 3;

    Vector2 mousePos;
    Vector2 lookDir;
    float angle;

    [SerializeField] LayerMask layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dj = GetComponent<DistanceJoint2D>();
        lr = GetComponent<LineRenderer>();
        pivot = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {   
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - (Vector2)pivot.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        pivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        if(Input.GetKeyDown(KeyCode.Mouse0) && ammo > 0)
        {
            rb.AddForce(gunForce * -pivot.right, ForceMode2D.Impulse);
            ammo--;
            P4GameManager.Instance.UpdateAmmoText(ammo);
        }

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(pivot.position, pivot.right, 80, layerMask);

            if (hitInfo)
            {
                dj.enabled = true;
                dj.connectedAnchor = hitInfo.point;
            }
        }
        else if(Input.GetKey(KeyCode.Mouse1))
        {

            if (dj.enabled)
            {
                lr.enabled = true;

                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, dj.connectedAnchor);
            }
        }
        else
        {
            dj.enabled = false;
            lr.enabled = false;
        }
    }
}
