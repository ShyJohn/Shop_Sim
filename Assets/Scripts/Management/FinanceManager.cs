using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinanceManager : MonoBehaviour
{
    private static FinanceManager singleton;

    public float balance = 0.0f;
    Text balanceText;
    private void Awake()
	{
		if (singleton != null && singleton != this)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			singleton = this;
			DontDestroyOnLoad(gameObject);
		}

	}

    public static FinanceManager Instance
	{
		get
		{
			if (singleton == null)
			{
				Debug.LogError("[FinanceManager]: Instance does not exist");
				return null;
			}
			return singleton;
		}
	}

	private void Start()
	{
		balanceText = GameObject.FindGameObjectWithTag("UI_Balance").GetComponent<Text>();
		if (balanceText == null)
		{
			Debug.LogError("[FinanceManager]: Couldn't find balance UI text gameobject or text component");
		}
	}

	public void AddToBalance(float addition)
	{
		balance += addition;
		balanceText.text = "£" + balance.ToString("F2");
	}
}
