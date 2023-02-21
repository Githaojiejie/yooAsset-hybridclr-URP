using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniFramework.Window;
using UniFramework.Event;
using UniFramework.Machine;
using UniFramework.Module;
using YooAsset;

internal class FsmSceneMain : IStateNode
{
	void IStateNode.OnCreate(StateMachine machine)
	{	
	}
	void IStateNode.OnEnter()
	{
		UniModule.StartCoroutine(Prepare());
	}
	void IStateNode.OnUpdate()
	{
		
	}
	void IStateNode.OnExit()
	{
		
	}

	private IEnumerator Prepare()
	{
		yield return UniWindow.OpenWindowAsync<UILoadingWindow>("UILoading");
		yield return YooAssets.LoadSceneAsync("scene_main");

		// 等所有数据准备完毕后，关闭加载界面。
		UniWindow.CloseWindow<UILoadingWindow>();


		YooAssets.LoadAssetSync<GameObject>("Cube").InstantiateSync().transform.position = Vector3.one;
	}
}