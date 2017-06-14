using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleet.Model
{
    public enum PORT_TYPE
    {
        DI = 1,     // Discrete Inputs
        DO = 2,     // Discrete Outputs
        AI = 3,      // Analog Inputs
        FC = 4,      // Freq Counter Inputs
        GI = 5       // Genegal Inputs
    }

    public enum GROUP_TYPE
    {
        VEHICLE_GROUP = 1,
        DRIVER_GROUP = 2
    }

    public enum VehicleOnlineDataLabel
    {
        AccountId = 1,
        RMUId = 2,
        X = 3,
        Y = 4,
        ItemId = 5,
        groupId = 6,
        CTime = 7,
        RTime = 8,
        VN = 9,
        Name = 10,
        S = 11,
        EOn = 12,
        Addr = 13,
        SA = 14,
        AlarmSystem = 15,
        GF = 16,
        TR = 17,
        GPRS = 18,
        InputV = 19,
        FuelLevel = 20,
        EstDrivingDistance = 21,
        GP3 = 22,
        InputA = 23,
        HRNW = 24,
        I = 25,
        MDT = 26,
        MileCounter = 27,
        MdtGeneralStatus = 28,
        MDTType = 29,
        RMUTypeId = 30,
        TS1 = 31,
        TS2 = 32,
        VID = 33,
        Remarks = 34,
        Direction = 35,
        HourMeter = 36,
        HardwareType = 37,
        TaskName = 38,
        TaskComment = 39,
        TaskStatus = 40,
        TaskETA = 41,
        TaskId = 42,
        RouteStatus = 43,
        RouteProgress = 44,
        AccountName = 45,
        AlarmStatus = 46,
        Feedback = 47,
        AlarmId = 48,
        AlarmOwner = 49,
        Severity = 50,
        AlarmName = 51,
        LP = 52

    }

    public enum ActionType
    {
        CREATE,
        UPDATE,
        GET,
        DELETE
    }

    public enum RenderFormat
    {
        XML,
        CSV,
        IMAGE,
        PDF,
        MHTML,
        EXCEL
    }

    public enum EmailPriority
    {
        HIGH,
        NORMAL,
        LOW
    }

    public enum IconType
    {
        CAR = 1,
        LOGO,
        CSS
    }


    public enum IconDimensions
    {
        Height = 16,
        Width = 26
    }

    public enum LogoDimensions
    {
        Height = 44,
        Width = 135
    }

    public enum AccountSwitch
    {
        //DefaultValue = -1,
        AddressSearch = 0,
        Routehistory,
        GeoFencingfunctionality,
        ReportGeneration,
        Messages,
        ImmobilizationTriggering,
        TransmissionRateChangeCommanding,
        DriversManagement,
        MDTSupport,
        PointsOfInterest,
        WEBLocation,
        SMSOut,
        ResourceConfiguration,
        ScheduledReports,
        AdministratorCapabilities,
        MDTCommands,
        Fleet360,
        MobileEnabled,
        ViewerAccount,
        ViewerDrivers,
        DisableOdometerUpdate,
        Hourmeter,
        SafetyAccount,
        TaskManagement,
        SubaccountHierarch,
        FollowTheLeader,
        MultiLevelGroup,
        AlarmManagement,
        RiskManagement,
        Dashboard,
        RssEnabled        
    }


    public enum TspPermission
    {
        TSPLevelAlarms = 0,
        AssignAlarm,
        UnassignAlarm,
        CloseAlarm,
        SendCommands
    }

    public enum TSPAccount
    {
        MM = -22,
        MA = -23
    }

    public enum TSPUser
    {
        MMUser = 22,
        MAUser
    }

    public enum UserType
    {
        VehicleSharing = 25
    }
    public enum REPORT_PATH
    {
        Default = 0,
        EventActivity = 1,
        ActivitySummary = 2,
        DailyActivity = 3,
        IdlingSessions = 4,
        AnalogInputs = 5,
        AreaActivity = 6,
        Landmarks = 7,
        SpeedViolation = 8,
        DrivingSessions = 9,
        Events = 10,
        Notifications = 11,
        ParkingPeriods = 12,
        Locations = 13,
        AlertsBreadcrumbs = 14,
        FuelEvents = 15,
        CostOfIdling =16
    }

}

