using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Toolkit.Serialization;

namespace Fleet.Model
{
    [DataContract]
    [TDataSerialize(TryAll = true)]
    [TSerialize("DetailItem", TryAll = true)]
    [TScriptSerialize(TryAll=true)]
    [Serializable]
    public class NameIdDescTypeInfo 
	{
        [DataMember]
        [TSerialize("Id", ElementType.AttributeOrElement)]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        [TSerialize("Description", ElementType.CData)]
        [DataMember(EmitDefaultValue = false, IsRequired = false)]
        public string Description { get; set; }
	}

    [DataContract]
    [TDataSerialize(TryAll = true)]
    [TSerialize(TryAll = true)]
    [Serializable]
    public class NameIdTypeInfo
    {
        [DataMember]
        [TSerialize("Id", ElementType.AttributeOrElement)]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    [TDataSerialize(TryAll = true)]
    [TSerialize(TryAll = true)]
    [Serializable]
    public class IdTypeInfo
    {
        [DataMember]
        [TSerialize("Id", ElementType.AttributeOrElement)]
        public int Id { get; set; }
    }

    [DataContract]
     [Serializable]
    public class NameIdDefaultTypeInfo : NameIdDescTypeInfo
    {
        [DataMember]
        public int IsDefault { get; set; }
    }


    /// <summary>
    /// USE IN REPORT PAGE
    /// id needed as string
    /// </summary>
    [DataContract]
    [TDataSerialize(TryAll = true)]
    [TSerialize(TryAll = true)]
    [Serializable]
    public class NameIdStrInfo
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
    }

    /// <summary>
    /// USE IN REPORT PAGE
    /// id needed as string
    /// </summary>
    [DataContract]
    [TDataSerialize(TryAll = true)]
    [TSerialize(TryAll = true)]
    [Serializable]
    public class NameValueListInfo
    {
        public string Name { get; set; }
        public string[] Value { get; set; }
        public override string ToString()
        {
            if (Value == null) return null;
            int len = Value.Length;
            StringBuilder str = new StringBuilder("");
            for (int i = 0; i < len; i++)
            {
                str.Append(Value[i]);
                if (i < len - 1)
                    str.Append(",");
            }
            return str.ToString();
        }
    }

}
