using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject Player;
    public Image Hp;
    public float HpNum;
    public Image EXP;
    public float EXPNum;
    public Text gear;
    public Text Lv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HpNum = Player.GetComponent<Play>().Hp;
        Hp.rectTransform.localScale = new Vector3(HpNum / Player.GetComponent<Play>().Hp_max, 1f, 1f);
        EXPNum = Player.GetComponent<Play>().EXP;
        EXP.rectTransform.localScale = new Vector3(EXPNum / Player.GetComponent<Play>().EXP_max, 1f, 1f);
        Lv.text = Player.GetComponent<Play>().Level.ToString();
        gear.text = Player.GetComponent<Play>().Component.ToString();
    }
}
