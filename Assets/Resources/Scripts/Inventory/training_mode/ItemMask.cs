using UnityEngine;
using System.Collections;

public class ItemMask : MonoBehaviour
{
    public TrainingController TC;
    // Use this for initialization
    void Start()
    {
        TC = GameObject.Find("Interface/SystemTools").GetComponent<TrainingController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Click()
    {
        TC.IsActive();
    }
}
