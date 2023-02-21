using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniFramework.Window;

[WindowAttribute(100, false)]
public class UILoginWindow : UIWindow
{
	public override void OnCreate()
	{
		var loginBtn = this.transform.Find("Button").GetComponent<Button>();
		loginBtn.onClick.AddListener(OnClickLoginBtn);
	}
	public override void OnDestroy()
	{
	}
	public override void OnRefresh()
	{
		
	}
	public override void OnUpdate()
	{
	}

	private void OnClickLoginBtn()
	{
		SceneEventDefine.ChangeToMainScene.SendEventMessage();
	}
}