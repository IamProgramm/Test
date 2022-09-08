
using UnityEngine;
using TMPro;

public class StartManager : MonoBehaviour
{
    [SerializeField] 
    public TMP_Text PlayText;
    public GameObject UI;
    void Start()
    {
        Time.timeScale = 0.0f;
        UI.SetActive(true);
        PlayText.text = "TAP TO PLAY !!!";
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Input.GetTouch(0);
            Time.timeScale = 1.0f;
            UI.SetActive(false);
        }
    }

}
