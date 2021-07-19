using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectUI : MonoBehaviour
{
	[SerializeField] GameObject infoPanel;
	[SerializeField] TMP_Text infoLabel;

	// Start is called before the first frame update
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void Show (string infoText)
	{
		infoPanel.SetActive (true);
		infoLabel.text = infoText;
	}

	public void Hide ()
	{
		infoPanel.SetActive (false);
		infoLabel.text = "";
	}
}
