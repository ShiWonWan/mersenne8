using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriteEffect : MonoBehaviour
{
    public static TypeWriteEffect instance;
    public float showSpeed = 0.05f;
    public Text textoAlien;
    public bool doomed = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Show Alien Text
    // @param none
    // @return IEnumerator
    public IEnumerator showText()
    {
        if (!doomed)
        {
            textoAlien.color = new Color(textoAlien.color.r, textoAlien.color.g, textoAlien.color.b, Mathf.MoveTowards(textoAlien.color.a, 1f, 5f * Time.deltaTime));
            yield return new WaitForSeconds(2f);
            textoAlien.color = new Color(textoAlien.color.r, textoAlien.color.g, textoAlien.color.b, Mathf.MoveTowards(textoAlien.color.a, 0f, 5f * Time.deltaTime));
            yield return new WaitForSeconds(2f);
            HideThyself();
        }
    }

    // Manage hide text
    // @param self
    // @return vooid
    private void HideThyself()
    {
        if (!doomed)
        {
            doomed = true;
            gameObject.SetActive(false);
        }
    }
}
