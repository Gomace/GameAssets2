using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    [SerializeField] private float Speed = 1f;
    [Header("Rotation Configs")]
    [SerializeField] private float RotationAmount = 90f;
    [SerializeField] private bool ForwardDirection = false;

    private Vector3 StartRotation;

    private Coroutine AnimationCoroutine;

    private Animation anim;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        anim = transform.GetChild(1).GetComponent<Animation>();
        anim["HandleAnim"].speed = 0;
    }

    public void OpenClose()
    {
        if (AnimationCoroutine != null)
            StopCoroutine(AnimationCoroutine);
        
        AnimationCoroutine = StartCoroutine(IsOpen ? DoRotationClose() : DoRotationOpen());
    }

    private IEnumerator DoRotationOpen()
    {
        anim["HandleAnim"].speed = 1;
        anim.Play();

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (ForwardDirection)
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + RotationAmount, 0));
        else
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));

        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }    
}