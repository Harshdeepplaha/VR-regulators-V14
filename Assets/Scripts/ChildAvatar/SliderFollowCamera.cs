using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SliderFollowCamera : MonoBehaviour
{
    private Transform mainCameraTrans;
    private GameObject[] attention_tantrum_Canvas; // each child object have a "Canvas" so we need to get all of them
    private GameObject speakPanel;
    private GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {
        mainCameraTrans = GameObject.Find("Main Camera").transform;
        attention_tantrum_Canvas = GameObject.FindGameObjectsWithTag("ChildTantrumAttentionCanvas");
        speakPanel = GameObject.Find("SpeakPanel");
        playerObject = GameObject.Find("PlayerObject");
    }

    // Update is called once per frame
    void Update()
    {
        /*foreach (GameObject canvas in attention_tantrum_Canvas)
        {
            canvas.transform.position = mainCameraTrans.position + mainCameraTrans.rotation * new Vector3(-0.7f, 0, 2) + new Vector3(0, -0.2f, 0.3f);
            //canvas.transform.rotation = mainCameraTrans.rotation;
            canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - mainCameraTrans.position);
        }
        speakPanel.transform.position = mainCameraTrans.position + mainCameraTrans.rotation * new Vector3(0.6f, 0, 2) + new Vector3(0, -0.5f, 0.3f);
        //speakPanel.transform.rotation = mainCameraTrans.rotation;
        //speakPanel.transform.LookAt(mainCameraTrans);
        speakPanel.transform.rotation = Quaternion.LookRotation(speakPanel.transform.position - mainCameraTrans.position);*/


        foreach (GameObject canvas in attention_tantrum_Canvas)
        {
            canvas.transform.position = mainCameraTrans.position + mainCameraTrans.rotation * new Vector3(-0.7f, 0, 2) + new Vector3(0, 0.2f, 0.3f);
            canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - mainCameraTrans.position);
            if (canvas.transform.position.y < -0.5f)
                canvas.transform.position = new Vector3(canvas.transform.position.x, -0.5f, canvas.transform.position.z);
            else if (canvas.transform.position.y > 0.5f)
                canvas.transform.position = new Vector3(canvas.transform.position.x, 0.5f, canvas.transform.position.z);
        }
        speakPanel.transform.position = mainCameraTrans.position + mainCameraTrans.rotation * new Vector3(0.6f, 0, 2) + new Vector3(0, -0.5f, 0.3f);
        speakPanel.transform.rotation = Quaternion.LookRotation(speakPanel.transform.position - (playerObject.transform.position + mainCameraTrans.position) / 2f);
        if (speakPanel.transform.position.y < -1.1f)
            speakPanel.transform.position = new Vector3(speakPanel.transform.position.x, -1.1f, speakPanel.transform.position.z);
        else if (speakPanel.transform.position.y > 0.1f)
            speakPanel.transform.position = new Vector3(speakPanel.transform.position.x, 0.1f, speakPanel.transform.position.z);
    }
}
