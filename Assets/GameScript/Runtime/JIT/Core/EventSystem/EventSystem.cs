using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace Game.JIT
{
    public enum InstanceQueueIndex
    {
        None = -1,
        Update,
        LateUpdate,
        Load,
        Max,
    }
    
    public class EventSystem
    {
        public static EventSystem Instance { get; } = new();

        private readonly Dictionary<string, Type> allTypes = new();

        private readonly UnOrderMultiMapSet<Type, Type> types = new();

        private readonly Queue<long>[] queues = new Queue<long>[(int)InstanceQueueIndex.Max];

        public EventSystem()
        {
            for (int i = 0; i < this.queues.Length; i++)
            {
                this.queues[i] = new Queue<long>();
            }
        }

        public void Add(Dictionary<string, Type> addTypes)
        {
            this.allTypes.Clear();
            this.types.Clear();

            foreach ((string fullName, Type type) in addTypes)
            {
                this.allTypes[fullName] = type;

                if (type.IsAbstract)
                {
                    continue;
                }

                object[] objects = type.GetCustomAttributes(typeof(ProtoContractAttribute), true);

                foreach (object o in objects)
                {
                    this.types.Add(o.GetType(), type);
                }
            }
        }

        public HashSet<Type> GetTypes(Type systemAttributeType)
        {
            if (!this.types.ContainsKey(systemAttributeType))
            {
                return new HashSet<Type>();
            }

            return this.types[systemAttributeType];
        }

        public Dictionary<string, Type> GetTypes()
        {
            return allTypes;
        }

        public Type GetType(string typeName)
        {
            return this.allTypes[typeName];
        }
    }
}