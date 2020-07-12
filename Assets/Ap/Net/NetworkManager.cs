using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Ap.Net;
using Ap.Core;
using Ap.Game;

namespace Ap.Net
{
    public class NetworkManager : ManagerBase<NetworkManager>
    {
        public delegate void OnReceivedMessageHandle(int id, ByteBuffer data);
        public OnReceivedMessageHandle OnReceivedMessage;

        private SocketClient m_Socket;
        protected Queue<KeyValuePair<int, ByteBuffer>> m_Events = new Queue<KeyValuePair<int, ByteBuffer>>();

        SocketClient SocketClient
        {
            get
            {
                if (m_Socket == null)
                {
                    m_Socket = new SocketClient();
                    m_Socket.OnConnectEvent += OnConnect;
                    m_Socket.OnDisconnectedEvent += OnDisconnected;
                    m_Socket.OnReceivedMessageEvent += Socket_OnReceivedMessage;
                }
                return m_Socket;
            }
        }

        public void Awake()
        {
            SocketClient.OnRegister();
            //LuaManager.Instance.CallFunction("NetworkManager.Start");
        }

        protected override void Init()
        {


        }
        ///------------------------------------------------------------------------------------
        protected void AddEvent(int _event, ByteBuffer data)
        {
            m_Events.Enqueue(new KeyValuePair<int, ByteBuffer>(_event, data));
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        protected void Update()
        {
            if (m_Events.Count > 0)
            {
                while (m_Events.Count > 0)
                {
                    KeyValuePair<int, ByteBuffer> e = m_Events.Dequeue();
                    if (OnReceivedMessage != null)
                        OnReceivedMessage.Invoke(e.Key, e.Value);
                    //LuaManager.Instance.CallFunction("NetworkManager.OnMessage", e.Key, e.Value);
                }
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public void SendConnect()
        {
            SocketClient.SendConnect(Context.Instance.ServerIp, Context.Instance.ServerPort);
        }

        /// <summary>
        /// ����SOCKET��Ϣ
        /// </summary>
        public void SendMessage(ByteBuffer buffer)
        {
            SocketClient.SendMessage(buffer);
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="protocal"></param>
        /// <param name="data"></param>
        protected void OnConnect(int protocal, ByteBuffer data)
        {
            if (OnReceivedMessage != null)
                OnReceivedMessage.Invoke(protocal, data);
            //LuaManager.Instance.CallFunction("NetworkManager.OnConnect", protocal, data);
        }
        /// <summary>
        /// ��ʧ����
        /// </summary>
        /// <param name="protocal"></param>
        /// <param name="data"></param>
        protected void OnDisconnected(int protocal, ByteBuffer data)
        {
            if (OnReceivedMessage != null)
                OnReceivedMessage.Invoke(protocal, data);
            //LuaManager.Instance.CallFunction("NetworkManager.OnDisconnect", protocal, data);
        }
        /// <summary>
        /// ��ȡ�µ�����
        /// </summary>
        /// <param name="protocal"></param>
        /// <param name="data"></param>
        protected void Socket_OnReceivedMessage(int protocal, ByteBuffer data)
        {
            AddEvent(protocal, data);
        }

        /// <summary>
        /// ��������
        /// </summary>
        void OnDestroy()
        {
            SocketClient.OnRemove();
            //LuaManager.Instance.CallFunction("NetworkManager.Unload");
            Debug.Log("~NetworkManager was destroy");
        }
    }
}