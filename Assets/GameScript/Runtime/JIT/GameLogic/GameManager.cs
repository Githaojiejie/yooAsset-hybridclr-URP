using System.Collections;
using System.Collections.Generic;
using System;
using Game.JIT;
using UnityEngine;
using UniFramework.Event;
using UniFramework.Machine;
using UniFramework.Module;
using UniFramework.Window;
using YooAsset;

public class GameManager : ModuleSingleton<GameManager>, IModule
{
	private bool _isRun = false;
	private EventGroup _eventGroup = new EventGroup();
	private StateMachine _machine;

	public Dictionary<string, Type> types;

	void IModule.OnCreate(object createParam)
	{
		types = AssemblyHelper.GetAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies());
		EventSystem.Instance.Add(types);
	}
	
	
	
	void IModule.OnDestroy()
	{
		_eventGroup.RemoveAllListener();
	}
	void IModule.OnUpdate()
	{
		if (_machine != null)
			_machine.Update();
	}

	public void Run()
	{
		if (_isRun == false)
		{
			_isRun = true;

			// 注册监听事件
			_eventGroup.AddListener<SceneEventDefine.ChangeToLoginScene>(OnHandleEventMessage);
			_eventGroup.AddListener<SceneEventDefine.ChangeToMainScene>(OnHandleEventMessage);
			
			_machine = new StateMachine(this);
			_machine.AddNode<FsmInitGame>();
			_machine.AddNode<FsmSceneLogin>();
			_machine.AddNode<FsmSceneMain>();
			_machine.Run<FsmInitGame>();
		}
		else
		{
			Debug.LogWarning("补丁更新已经正在进行中!");
		}
	}

	/// <summary>
	/// 接收事件
	/// </summary>
	private void OnHandleEventMessage(IEventMessage message)
	{
		if(message is SceneEventDefine.ChangeToLoginScene)
		{
			_machine.ChangeState<FsmSceneLogin>();
		}
		else if (message is SceneEventDefine.ChangeToMainScene)
		{
			_machine.ChangeState<FsmSceneMain>();
		}
	}
}