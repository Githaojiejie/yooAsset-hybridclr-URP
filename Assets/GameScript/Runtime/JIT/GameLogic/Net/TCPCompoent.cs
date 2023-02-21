using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Game.JIT;
using UnityEngine;

public class TCPCompoent : MonoBehaviour
{
    private AService Service;
    private int channel_id;
    private string address = "127.0.0.1:10007";

    // Start is called before the first frame update
    void Start()
    {
        Service = new TService(ThreadSynchronizationContext.Instance, ServiceType.Outer);
        Service.ErrorCallback += (channelId, error) => OnError(channelId, error);
        Service.ReadCallback += (channelId, Message) => OnRead(channelId, Message);
        Service.GetOrCreate(channel_id, NetworkHelper.ToIPEndPoint(address));
    }

    public void OnRead(long channelId, object message)
    {
        // session.LastRecvTime = TimeHelper.ClientNow();
        // SessionStreamDispatcher.Instance.Dispatch(session, memoryStream);
        Debug.LogError(message);
    }

    public void OnError(long channelId, int error)
    {
        Service.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        ThreadSynchronizationContext.Instance.Update();
        Service.Update();
    }

    void OnGUI()
    {
        GUI.color = Color.black;
        if (GUI.Button(new Rect(65, 240, 60, 20), "释放"))
        {
            Service.Dispose();
        }

        if (GUI.Button(new Rect(65, 280, 60, 20), "发送数据"))
        {
            MemoryStream stream = new MemoryStream();
            HeartMsg HeartMsg = new HeartMsg()
            {
                Time = 100,
                Name = @" name ",
            };
            MemoryStream memoryStream = Service.GetMemoryStream(HeartMsg);

            Service.SendStream(channel_id, channel_id,  memoryStream);
        }
    }
}