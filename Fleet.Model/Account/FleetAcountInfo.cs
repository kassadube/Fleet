using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolkit.Serialization;

namespace Fleet.Model
{
    [TDataSerialize(TryAll = true)]
    [TScriptSerialize(TryAll = true)]
    [TSerialize( "P",TryAll=true)]
    [Serializable]
    public class FleetAccountInfo
    {
        [TSerialize("ACCOUNT_ID")]
        public int? AccountId { get; set; }

        [TSerialize("EXT")]
        public int? ExternalAccountId { get; set; }

        [TSerialize("NAME")]
        public string Name { get; set; }

        [TSerialize("ADDR")]
        public string Address { get; set; }

        [TSerialize("CNAME", DefaultValue = "")]
        public string ContactPerson { get; set; }

        [TSerialize("PHONE", DefaultValue = "")]
        public string PhoneNumber { get; set; }
        public string WebsiteURL { get; set; }
        public string LinkURL { get; set; }

        [TSerialize("STATUS")]
        public short Status { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        [TSerialize("REMARKS", DefaultValue = "")]
        public string Remarks { get; set; }

        [TSerialize("LOGO")]
        public int? LogoFileId { get; set; }

        [TSerialize("CSS")]
        public int? CSSFileId { get; set; }

        [TSerialize("PA", DefaultValue="0")]
        public int ParentAccount { get; set; }

        [TSerialize("SW")]
        public long? Switches { get; set; }

        [TSerialize("SMS", DefaultValue = "")]
        public string SMS { get; set; }

        [TSerialize("EMAIL", DefaultValue = "")]
        public string Email { get; set; }

        [TSerialize("MS")]
        public short? MapServerId { get; set; }

        public short? LocationType { get; set; }

        [TSerialize("IDL")]
        public short? Idling { get; set; }

        [TSerialize("ES", DefaultValue = "")]
        public string EmailSubject { get; set; }

        [TSerialize("DLANG")]
        public byte? DefaultLanguage { get; set; }

        [TSerialize("MF", DefaultValue = "")]
        public string MailFrom { get; set; }

        [TSerialize("FAI")]
        public short? FuelAnomalyIndicator { get; set; }

        [TSerialize("FC")]
        public byte? FuelConsumption { get; set; }

        [TSerialize("RHD")]
        public byte? RouteHistoryDiff { get; set; }

        [TSerialize("RHMD")]
        public byte? RouteHistoryMaxDiff { get; set; }

        [TSerialize("AH")]
        public int? AlertsHistory { get; set; }

        [TSerialize("MDTH")]
        public int? MDTHistory { get; set; }

        [TSerialize("XYAR")]
        public int? XYAliasRadius { get; set; }

        [TSerialize("SE", DefaultValue = "")]
        public string State { get; set; }

        [TSerialize("ST", DefaultValue="")]
        public string Street { get; set; }

        [TSerialize("CT", DefaultValue = "")]
        public string City { get; set; }

        [TSerialize("ZP", DefaultValue = "")]
        public string Zip { get; set; }

        [TSerialize("CO", DefaultValue = "")]
        public string Country { get; set; }

        [TSerialize("USER_NAME")]
        public string UserName { get; set; }

        [TDataSerialize("PASSWORD")]
        [TSerialize("LOGIN")]
        public string Password { get; set; }

        // MapServerParam 

        [TSerialize("ZOOM")]
        public int? Zoom { get; set; }

        [TSerialize("X")]
        public decimal? InitX { get; set; }

        [TSerialize("Y")]
        public decimal? InitY { get; set; }

        [TSerialize("DIV_X")]
        public int? DividerX { get; set; }

        [TSerialize("DIV_Y")]
        public int? DividerY { get; set; }

        [TSerialize("Cost")]
        public decimal? UnitCost { get; set; }

        [TSerialize("Crnc")]
        public int? CurrCode { get; set; }

        [TSerialize("CrType")]
        public int? CreditType { get; set; }

        [TSerialize("CrSubType")]
        public int? CreditSubType { get; set; }

        [TSerialize("Cr")]
        public int? CreditAmount { get; set; }

        [TSerialize("VTI")]
        public int? ValidationTypeId { get; set; }
        [TSerialize("MSId")]
        public int MeasurementSystemId { get; set; }
        
        public int? ParentAccountSwitches { get; set; }

        [TSerialize("AccountUserType")]
        public int AccountType { get; set; }

        [TSerialize(DefaultValue = "0")]
        public int TaskAutomaticVerification { get; set; }

        [TSerialize(DefaultValue = "0")]
        public int TaskMonitoring { get; set; }

        [TSerialize(DefaultValue = "0")]
        public int VehicleIconMechanism { get; set; }

        [TSerialize(DefaultValue = "0")]
        public int VehicleUniqueId { get; set; }

        [TSerialize(DefaultValue = "")]
        public string PolygonColor { get; set; }

    }
}
