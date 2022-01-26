using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bullet;
    private float shootTimer = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    
        
        if (Input.GetMouseButtonDown(0))
        {
            if(shootTimer<0)
            {
                shootTimer = 0.5f;
                Instantiate(bullet, (Vector2)transform.position+ (Vector2)(transform.rotation * (new Vector2(1.09f, 0))), transform.rotation*=Quaternion.Euler(0, 0, -90));
            }
        }
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
         if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.up * Time.deltaTime * 2, Space.World);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime * 2, Space.World);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.down * Time.deltaTime * 2, Space.World);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * 2, Space.World);

        shootTimer-=Time.deltaTime;

    }
}
