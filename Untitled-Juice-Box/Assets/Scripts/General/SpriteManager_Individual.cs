using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteManager_Individual : MonoBehaviour
{

    public static Vector3 SetRotation = new Vector3(65, 0, 0);

    [SerializeField] bool Hover;
    [SerializeField] AnimationCurve m_Curve;
    [SerializeField] float HoverDistance;
    public float time;
    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(SetRotation);

        if (Hover)
        {
            transform.localPosition = Vector3.Lerp(new Vector3(0, HoverDistance, 0), new Vector3(0, -HoverDistance, 0), m_Curve.Evaluate(time));
            time += Time.deltaTime;
        }

    }
}
