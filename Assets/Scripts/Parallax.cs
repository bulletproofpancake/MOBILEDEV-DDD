using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private float offset;

    [SerializeField]
    private Vector3 startposition;
    [SerializeField]
    private float newXposition;


    // Start is called before the first frame update
    void Start()
    {
        startposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newXposition = Mathf.Repeat(Time.time * -moveSpeed, offset);

        transform.position = startposition + Vector3.right * newXposition;

    }
}
