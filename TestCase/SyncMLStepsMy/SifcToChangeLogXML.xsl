<?xml version="1.0" encoding="UTF-8"?>
<!--Convert SIF-C XML into fields of a contact in the Change Log XML.

The following fields are not implemented:
CallbackTelephoneNumber, CarTelephoneNumber, Categories, Companies, ComputerNetworkName, Importance,
Initials, Language, Mileage, OrganizationalIDNumber, Revision, RadioTelephoneNumber, Sensitivity,
Subject, TelexNumber, Timezone, Uid, YomiCompanyName, YomiFirstName.

There was not technical difficulty to implement these fields, and they can be added later for specific projects,
as long as the device can support these fields.

References: Sync4j SyncServer Developer's Guide (January 2005)
-->
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" omit-xml-declaration="yes" encoding="UTF-8" />

  <xsl:template match="contact">

    <Field Name="Surname" Section="">
      <xsl:value-of select="LastName"/>
    </Field>
    <Field Name="GivenName" Section="">
      <xsl:value-of select="FirstName"/>
    </Field>
    <Field Name="MidName" Section="">
      <xsl:value-of select="MiddleName"/>
    </Field>
    <Field Name="Title" Section="">
      <xsl:value-of select="Title"/>
    </Field>
    <Field Name="Notes" Section="">
      <xsl:value-of select="Body"/>
    </Field>
    <Field Name="Nickname" Section="Personal">
      <xsl:value-of select="NickName"/>
    </Field>
    <Field Name="Mobile" Section="Personal">
      <xsl:value-of select="MobileTelephoneNumber"/>
    </Field>
    <Field Name="Email" Section="Personal">
      <xsl:value-of select="Email1Address"/>
    </Field>
    <Field Name="Email2" Section="Personal">
      <xsl:value-of select="Email2Address"/>
    </Field>
    <Field Name="Email3" Section="Personal">
      <xsl:value-of select="Email3Address"/>
    </Field>
    <Field Name="Primary Phone" Section="Personal">
      <xsl:value-of select="PrimaryTelephoneNumber"/>
    </Field>
    <Field Name="Phone" Section="Personal">
      <xsl:value-of select="HomeTelephoneNumber"/>
    </Field>
    <Field Name="Phone2" Section="Personal">
      <xsl:value-of select="Home2TelephoneNumber"/>
    </Field>
    <Field Name="IM" Section="Personal">
      <xsl:value-of select="IMAddress"/>
    </Field>
    <Field Name="PO Box" Section="Personal">
      <xsl:value-of select="HomeAddressPostOfficeBox"/>
    </Field>
    <Field Name="Fax" Section="Personal">
      <xsl:value-of select="HomeFaxNumber"/>
    </Field>
    <Field Name="Pager" Section="Personal">
      <xsl:value-of select="PagerNumber"/>
    </Field>
    <Field Name="Web" Section="Personal">
      <xsl:value-of select="HomeWebPage"/>
    </Field>
    <Field Name="Web" Section="Personal">
      <xsl:value-of select="WebPage"/>
    </Field>
    <Field Name="Street" Section="Personal">
      <xsl:value-of select="HomeAddressStreet"/>
    </Field>
    <Field Name="City" Section="Personal">
      <xsl:value-of select="HomeAddressCity"/>
    </Field>
    <Field Name="State" Section="Personal">
      <xsl:value-of select="HomeAddressState"/>
    </Field>
    <Field Name="Country" Section="Personal">
      <xsl:value-of select="HomeAddressCountry"/>
    </Field>
    <Field Name="Postcode" Section="Personal">
      <xsl:value-of select="HomeAddressPostalCode"/>
    </Field>
    <Field Name="Suffix" Section="Personal">
      <xsl:value-of select="Suffix"/>
    </Field>
    <Field Name="Birthday" Section="Personal">
      <xsl:value-of select="Birthday"/>
    </Field>
    <Field Name="Spouse" Section="Personal">
      <xsl:value-of select="Spouse"/>
    </Field>
    <Field Name="Anniversary" Section="Personal">
      <xsl:value-of select="Anniversary"/>
    </Field>
    <Field Name="Children" Section="Personal">
      <xsl:value-of select="Children"/>
    </Field>
    <Field Name="Assistant" Section="Personal">
      <xsl:value-of select="AssistantName"/>
    </Field>
    <Field Name="Assistant Phone" Section="Personal">
      <xsl:value-of select="AssistantTelephoneNumber"/>
    </Field>
    <Field Name="Gender" Section="Personal">
      <xsl:value-of select="Gender"/>
    </Field>
    <Field Name="Hobby" Section="Personal">
      <xsl:value-of select="Hobby"/>
    </Field>


    <Field Name="Primary Phone" Section="Work">
      <xsl:value-of select="CompanyMainTelephoneNumber"/>
    </Field>
    <Field Name="Phone" Section="Work">
      <xsl:value-of select="BusinessTelephoneNumber"/>
    </Field>
    <Field Name="Phone2" Section="Work">
      <xsl:value-of select="Business2TelephoneNumber"/>
    </Field>
    <Field Name="Fax" Section="Work">
      <xsl:value-of select="BusinessFaxNumber"/>
    </Field>
    <Field Name="PO Box" Section="Work">
      <xsl:value-of select="BusinessAddressPostOfficeBox"/>
    </Field>
    <Field Name="Office Location" Section="Work">
      <xsl:value-of select="OfficeLocation"/>
    </Field>
    <Field Name="Street" Section="Work">
      <xsl:value-of select="BusinessAddressStreet"/>
    </Field>
    <Field Name="City" Section="Work">
      <xsl:value-of select="BusinessAddressCity"/>
    </Field>
    <Field Name="State" Section="Work">
      <xsl:value-of select="BusinessAddressState"/>
    </Field>
    <Field Name="Country" Section="Work">
      <xsl:value-of select="BusinessAddressCountry"/>
    </Field>
    <Field Name="Postcode" Section="Work">
      <xsl:value-of select="BusinessAddressPostalCode"/>
    </Field>
    <Field Name="Title" Section="Work">
      <xsl:value-of select="JobTitle"/>
    </Field>
    <Field Name="Company" Section="Work">
      <xsl:value-of select="CompanyName"/>
    </Field>
    <Field Name="Department" Section="Work">
      <xsl:value-of select="Department"/>
    </Field>
    <Field Name="Profession" Section="Work">
      <xsl:value-of select="Profession"/>
    </Field>
    <Field Name="Manager" Section="Work">
      <xsl:value-of select="ManagerName"/>
    </Field>
    <Field Name="Web" Section="Work">
      <xsl:value-of select="BusinessWebPage"/>
    </Field>

    
    <Field Name="Phone" Section="Other">
      <xsl:value-of select="OtherTelephoneNumber"/>
    </Field>
    <Field Name="Fax" Section="Other">
      <xsl:value-of select="OtherFaxNumber"/>
    </Field>
    <Field Name="PO Box" Section="Other">
      <xsl:value-of select="OtherAddressPostOfficeBox"/>
    </Field>
    <Field Name="Street" Section="Other">
      <xsl:value-of select="OtherAddressStreet"/>
    </Field>
    <Field Name="City" Section="Other">
      <xsl:value-of select="OtherAddressCity"/>
    </Field>
    <Field Name="State" Section="Other">
      <xsl:value-of select="OtherAddressState"/>
    </Field>
    <Field Name="Country" Section="Other">
      <xsl:value-of select="OtherAddressCountry"/>
    </Field>
    <Field Name="Postcode" Section="Other">
      <xsl:value-of select="OtherAddressPostalCode"/>
    </Field>

  </xsl:template>

</xsl:stylesheet>