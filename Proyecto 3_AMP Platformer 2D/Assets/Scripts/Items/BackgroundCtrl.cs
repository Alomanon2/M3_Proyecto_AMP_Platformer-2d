using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCtrl : MonoBehaviour
{
    private GameObject mainCamera, backgroundTile; 
    public GameObject backgroundTile2;
    private float offsetX; //Offset should be the same size as the positionX of the next background tile
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        backgroundTile = this.gameObject;
        offsetX=backgroundTile2.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.transform.position.x - backgroundTile.transform.position.x > 2*offsetX)
        {
            backgroundTile.transform.Translate(new Vector2(3*offsetX,0));
        }
        if (backgroundTile.transform.position.x - mainCamera.transform.position.x > 2*offsetX)
        {
            backgroundTile.transform.Translate(new Vector2(-3 * offsetX, 0));
        }
    }
}
