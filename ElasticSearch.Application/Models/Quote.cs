using System;
using System.Collections.Generic;

namespace ElasticSearch.Application.Models
{
    public class Quote
    {
        public string RequestReferenceNo { get; set; }
        public int InsuranceCompanyCode { get; set; }
        public int InsuranceTypeID { get; set; }
        public Details Details { get; set; }
    }

    public class Details
    {
        public int PolicyholderIdentityTypeCode { get; set; }
        public int PolicyHolderID { get; set; }
        public string PolicyholderIDExpiry { get; set; }
        public int PurposeofVehicleUseID { get; set; }
        public int QuoteRequestSourceID { get; set; }
        public string DateOfBirthG { get; set; }
        public string DateOfBirthH { get; set; }
        public int PolicyholderNationalityID { get; set; }
        public object FullName { get; set; }
        public object ArabicFirstName { get; set; }
        public object ArabicMiddleName { get; set; }
        public object ArabicLastName { get; set; }
        public string EnglishFirstName { get; set; }
        public string EnglishMiddleName { get; set; }
        public string EnglishLastName { get; set; }
        public int Cylinders { get; set; }
        public object VehicleCapacity { get; set; }
        public int VehicleUniqueTypeID { get; set; }
        public int VehicleSequenceNumber { get; set; }
        public object VehicleCustomID { get; set; }
        public int PolicyholderGender { get; set; }
        public string Occupation { get; set; }
        public string Education { get; set; }
        public int MaritalStatus { get; set; }
        public int ChildrenBelow16 { get; set; }
        public string WorkCompanyName { get; set; }
        public int WorkCityID { get; set; }
        public int VehicleDriveRegionID { get; set; }
        public int VehicleDriveCityID { get; set; }
        public int VehiclePlateTypeID { get; set; }
        public int VehiclePlateNumber { get; set; }
        public int FirstPlateLetterID { get; set; }
        public int SecondPlateLetterID { get; set; }
        public int ThirdPlateLetterID { get; set; }
        public int VehicleMakeCodeNajm { get; set; }
        public int VehicleMakeCodeNIC { get; set; }
        public string VehicleMakeTextNIC { get; set; }
        public int VehicleMakeCodeTameeni { get; set; }
        public int VehicleModelCodeNajm { get; set; }
        public int VehicleModelCodeNIC { get; set; }
        public string VehicleModelTextNIC { get; set; }
        public int VehicleModelCodeTameeni { get; set; }
        public int ManufactureYear { get; set; }
        public int VehicleColorCode { get; set; }
        public object VehicleWeight { get; set; }
        public object VehicleBodyCode { get; set; }
        public object VehicleRegistrationCityCode { get; set; }
        public string VehicleVIN { get; set; }
        public string VehicleRegistrationExpiryDate { get; set; }
        public object VehicleMileage { get; set; }
        public object VehicleExpectedMileageYear { get; set; }
        public object VehicleEngineSizeCC { get; set; }
        public int VehicleTransmission { get; set; }
        public int VehicleNightParking { get; set; }
        public object VehicleAntitheftAlarm { get; set; }
        public object VehicleABS { get; set; }
        public object VehicleAutoBraking { get; set; }
        public object VehicleCruiseControl { get; set; }
        public object VehicleAdaptiveCruiseControl { get; set; }
        public object VehicleRearSensors { get; set; }
        public object VehicleFrontSensors { get; set; }
        public object VehicleRearCamera { get; set; }
        public object VehicleFrontCamera { get; set; }
        public object Vehicle360Camera { get; set; }
        public object VehicleFireExtinguisher { get; set; }
        public object VehicleModifications { get; set; }
        public object VehicleAxleWeight { get; set; }
        public int CoverAgeLimitID { get; set; }
        public List<DriverDetail> DriverDetails { get; set; }
        public string MobileNo { get; set; }
        public int BuildingNumber { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int AdditionalNumber { get; set; }
        public string PolicyEffectiveDate { get; set; }
        public string PolicyholderNCDCode { get; set; }
        public string PolicyholderNCDReference { get; set; }
        public bool IsRenewal { get; set; }
        public bool IsScheme { get; set; }
        public List<SchemeDetail> SchemeDetails { get; set; }
        public List<CustomizedParameter> CustomizedParameter { get; set; }
        public double AddressLatitude { get; set; }
        public double AddressLongitude { get; set; }
    }

    public class DriverDetail
    {
        public int DriverID { get; set; }
        public string DriverName { get; set; }
        public bool IsPolicyHolder { get; set; }
        public bool IsUser { get; set; }
        public int DriverRelation { get; set; }
        public int VehicleUsagePercentage { get; set; }
        public string DriverDateOfBirthG { get; set; }
        public string DriverDateOfBirthH { get; set; }
        public int DriverGender { get; set; }
        public string DriverOccupation { get; set; }
        public string DriverEducation { get; set; }
        public int DriverMaritalStatus { get; set; }
        public int DriverChildrenBelow16 { get; set; }
        public string DriverWorkCompanyName { get; set; }
        public int DriverWorkCityID { get; set; }
        public string DriverHomeAddressCity { get; set; }
        public bool IsSamePolicyholderAddress { get; set; }
        public string DriverHomeAddress { get; set; }
        public int DriverHomeBuildingNumber { get; set; }
        public string DriverHomeStreet { get; set; }
        public string DriverHomeDistrict { get; set; }
        public int DriverHomeZipCode { get; set; }
        public int DriverHomeAdditionalNumber { get; set; }
        public int DriverLicenseType { get; set; }
        public int DriverLicenseOwnYears { get; set; }
        public List<CountriesValidDrivingLicense> CountriesValidDrivingLicense { get; set; }
        public string DriverNCDCode { get; set; }
        public object DriverNCDReference { get; set; }
        public int DriverNoOfAccidents { get; set; }
        public List<NajmCaseDetail> NajmCaseDetails { get; set; }
        public int DriverNoOfClaims { get; set; }
        public string DriverTrafficViolationsCode { get; set; }
        public string DriverHealthConditionsCode { get; set; }
    }

    public class SchemeDetail
    {
        public string SchemeRef { get; set; }
        public string IcSchemeRef { get; set; }
        public int SchemeTypeID { get; set; }
        public int PositionNameCode { get; set; }
        public object Value1 { get; set; }
        public object Value2 { get; set; }
        public object Value3 { get; set; }
        public object Value4 { get; set; }
    }

    public class CustomizedParameter
    {
        public string Key { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Value4 { get; set; }
    }

    public class CountriesValidDrivingLicense
    {
        public int DrivingLicenseCountryID { get; set; }
        public int DriverLicenseYears { get; set; }
    }

    public class NajmCaseDetail
    {
        public string CaseNumber { get; set; }
        public DateTime AccidentDate { get; set; }
        public string Liability { get; set; }
        public string DriverAge { get; set; }
        public string CarModel { get; set; }
        public string CarType { get; set; }
        public string DriverID { get; set; }
        public string SequenceNumber { get; set; }
        public string OwnerID { get; set; }
        public string EstimatedAmount { get; set; }
        public string DamageParts { get; set; }
        public string CauseOfAccident { get; set; }
    }
}