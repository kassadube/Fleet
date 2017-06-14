using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Toolkit.Serialization;

namespace Fleet.Model
{
    [TDataSerialize(TryAll = true)]
    [TScriptSerialize(TryAll = true)]
    [TSerialize("AccountInfo", TryAll = true)]
    [Serializable]
    [DataContract(Name="Account")]
    public class NIAccountInfo
    {
        [DataMember]
        public int AccountId { get; set; }
        [DataMember]
        public int ExternalAccountId { get; set; }
        [DataMember]
        public string AccountName { get; set; }
        public int AccountType { get; set; }
        public int ParentAccount { get; set; }
        public int Level { get; set; }
        public int VehicleIconMechanism { get; set; }
        public int VehicleUniqueId { get; set; }
    }
}
