using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinsCanvas : MonoBehaviour {

	public GameObject m_oTextObject {get; set;}
	public UnityEngine.UI.Text m_nameText{get; set; }

	// Use this for initialization
	void Start () {
//		CreateUI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void CreateUI(float x, float y, float z, string nameObj)
		{
			m_oTextObject = new GameObject();
			m_oTextObject.name = nameObj;
			m_oTextObject.transform.parent = GameObject.Find( "Canvas" ).transform;
			m_nameText = m_oTextObject.AddComponent< UnityEngine.UI.Text >();
//			m_nameText.text = "Coins :";
			m_oTextObject.GetComponent<RectTransform> ().localPosition = new Vector3 (x, y, z);
			m_oTextObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
			m_oTextObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);
//			m_nameText.alignment = TextAnchor.MiddleRight;
			m_nameText.horizontalOverflow = HorizontalWrapMode.Overflow;
			m_nameText.verticalOverflow = VerticalWrapMode.Overflow;
			Font ArialFont = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
			m_nameText.font = ArialFont;
			m_nameText.fontSize = 40;
			m_nameText.enabled = true;
			m_nameText.color = Color.white;
		}
	
}
