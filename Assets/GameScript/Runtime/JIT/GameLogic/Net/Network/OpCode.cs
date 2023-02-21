using System.Collections.Generic;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ProtoBuf;
using UnityEngine;
using UniFramework.Event;
using UniFramework.Machine;
using UniFramework.Module;
using UniFramework.Window;
using YooAsset;

namespace Game.JIT
{
    public class OpCode
    {
        public static OpCode Instance { get; } = new OpCode();
     
        public OpCode()
        {
            HashSet<Type> types = EventSystem.Instance.GetTypes(typeof (ProtoContractAttribute));
            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof (ProtoContractAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }

                ProtoContractAttribute messageAttribute = attrs[0] as ProtoContractAttribute;
                if (messageAttribute == null)
                {
                    continue;
                }
                
                var value = (ushort)EnumHelper.EnumIndex<msgId>(messageAttribute.Name);

                if (value < 0)
                {
                    Debug.LogError($"枚举值出错");
                    continue;
                }

                this.typeOpcode.Add(type, value);
            }
        }

        #region 线程安全
        
        // 初始化后不变，所以主线程，网络线程都可以读
        private readonly DoubleMap<Type, ushort> typeOpcode = new DoubleMap<Type, ushort>();

        public ushort GetOpcode(Type type)
        {
            return this.typeOpcode.GetValueByKey(type);
        }

        public Type GetType(ushort opcode)
        {
            return this.typeOpcode.GetKeyByValue(opcode);
        }

        #endregion
    }
}