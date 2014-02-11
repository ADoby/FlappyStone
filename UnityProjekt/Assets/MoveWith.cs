using UnityEngine;
using System.Collections;

public class MoveWith : MonoBehaviour {

    public Transform target;

    public Vector3 diffPosition;

    public bool smooth = false;
    public float speed;

    public bool ignoreY;
    private float startY;
	// Use this for initialization
	void Start () {
        diffPosition = transform.position - target.position;

        startY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = target.position + diffPosition;
        if (ignoreY)
            pos.y = startY;

        if(smooth)
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
        else
            transform.position = pos;
	}
}
