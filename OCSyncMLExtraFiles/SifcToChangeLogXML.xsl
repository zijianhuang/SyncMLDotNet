<?xml version="1.0" encoding="UTF-8"?>
<!--Convert SIFC Change Log XML into OC Change Log XML.

Thouse SIFC without FirstName and LastName will be regarded as Company.

The following fields are not implemented:
CallbackTelephoneNumber, CarTelephoneNumber, Categories, Companies, ComputerNetworkName, Importance,
Initials, Language, Mileage, OrganizationalIDNumber, Revision, RadioTelephoneNumber, Sensitivity,
Subject, TelexNumber, Timezone, Uid, YomiCompanyName, YomiFirstName.

There was not technical difficulty to implement these fields, and they can be added later for specific projects,
as long as the device can support these fields.

Please read the following comments for modifying the mapping between Open Contacts and Sif-C.

References: Sync4j SyncServer Developer's Guide (January 2005)
-->
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" omit-xml-declaration="yes" encoding="UTF-8" />


  <xsl:template match="/SIFCChangeLog">
    <OpenContactsChangeLog>
      <LastAnchor>
        <xsl:value-of select="LastAnchor"/>
      </LastAnchor>
      <Source>
        <xsl:text>Open Contacts SyncML Client</xsl:text>
      </Source>
      <Changes>
        <xsl:for-each select ="Changes/New/C">
          <New>
            <xsl:apply-templates select="."/>
          </New>
        </xsl:for-each>

        <xsl:for-each select ="Changes/Update/C">
          <Update>
            <xsl:apply-templates select="."/>
          </Update>
        </xsl:for-each>

        <xsl:for-each select="Changes/Delete/D">
          <Delete>
            <D>
              <xsl:attribute name="ID">
                <xsl:value-of select="@ID"/>
              </xsl:attribute>
            </D>
          </Delete>
        </xsl:for-each>

      </Changes>
    </OpenContactsChangeLog>
  </xsl:template>

  <xsl:template match="C">
    <xsl:choose>
      <xsl:when test="contact/FirstName!='' or contact/LastName!=''">
        <xsl:call-template name="Person">
          <xsl:with-param name="string" select="." />
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:call-template name="Company">
          <xsl:with-param name="string" select="." />
        </xsl:call-template>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- There are 2 templates for personal contacts and organizational contacts.-->

  <!--Template for converting person-->
  <xsl:template name="Person">
    <xsl:param name="string" />

    <C>
      <xsl:attribute name="ID">
        <xsl:value-of select="$string/@ID"/>
      </xsl:attribute>
      <xsl:attribute name="IsCompany">
        <xsl:text>0</xsl:text>
      </xsl:attribute>

      <!--FileAs of Sif-C is mapped to FullName of OC.-->
      <Field Name="FullName" Section="">
        <xsl:value-of select="$string/contact/FileAs"/>
      </Field>
      <Field Name="Surname" Section="">
        <xsl:value-of select="$string/contact/LastName"/>
      </Field>
      <Field Name="GivenName" Section="">
        <xsl:value-of select="$string/contact/FirstName"/>
      </Field>
      <Field Name="MidName" Section="">
        <xsl:value-of select="$string/contact/MiddleName"/>
      </Field>
      <Field Name="Title" Section="">
        <xsl:value-of select="$string/contact/Title"/>
      </Field>
      <Field Name="Notes" Section="">
        <xsl:value-of select="$string/contact/Body"/>
      </Field>
      <Field Name="Nickname" Section="Personal">
        <xsl:value-of select="$string/contact/NickName"/>
      </Field>
      <!--MobileTelephoneNumber of Sif-C is mapped to Mobile of the Personal section of OC.-->
      <Field Name="Mobile" Section="Personal">
        <xsl:value-of select="$string/contact/MobileTelephoneNumber"/>
      </Field>
      <Field Name="Email" Section="Personal">
        <xsl:value-of select="$string/contact/Email1Address"/>
      </Field>
      <Field Name="Email2" Section="Personal">
        <xsl:value-of select="$string/contact/Email2Address"/>
      </Field>
      <Field Name="Email3" Section="Personal">
        <xsl:value-of select="$string/contact/Email3Address"/>
      </Field>
      <Field Name="Primary Phone" Section="Personal">
        <xsl:value-of select="$string/contact/PrimaryTelephoneNumber"/>
      </Field>
      <Field Name="Phone" Section="Personal">
        <xsl:value-of select="$string/contact/HomeTelephoneNumber"/>
      </Field>
      <Field Name="Phone2" Section="Personal">
        <xsl:value-of select="$string/contact/Home2TelephoneNumber"/>
      </Field>
      <Field Name="IM" Section="Personal">
        <xsl:value-of select="$string/contact/IMAddress"/>
      </Field>
      <Field Name="PO Box" Section="Personal">
        <xsl:value-of select="$string/contact/HomeAddressPostOfficeBox"/>
      </Field>
      <Field Name="Fax" Section="Personal">
        <xsl:value-of select="$string/contact/HomeFaxNumber"/>
      </Field>
      <Field Name="Pager" Section="Personal">
        <xsl:value-of select="$string/contact/PagerNumber"/>
      </Field>
      <Field Name="Web" Section="Personal">
        <xsl:value-of select="$string/contact/HomeWebPage"/>
      </Field>
      <Field Name="Street" Section="Personal">
        <xsl:value-of select="$string/contact/HomeAddressStreet"/>
      </Field>
      <Field Name="City" Section="Personal">
        <xsl:value-of select="$string/contact/HomeAddressCity"/>
      </Field>
      <Field Name="State" Section="Personal">
        <xsl:value-of select="$string/contact/HomeAddressState"/>
      </Field>
      <Field Name="Country" Section="Personal">
        <xsl:value-of select="$string/contact/HomeAddressCountry"/>
      </Field>
      <Field Name="Postcode" Section="Personal">
        <xsl:value-of select="$string/contact/HomeAddressPostalCode"/>
      </Field>
      <Field Name="Suffix" Section="Personal">
        <xsl:value-of select="$string/contact/Suffix"/>
      </Field>
      <Field Name="Birthday" Section="Personal">
        <xsl:value-of select="$string/contact/Birthday"/>
      </Field>
      <Field Name="Spouse" Section="Personal">
        <xsl:value-of select="$string/contact/Spouse"/>
      </Field>
      <Field Name="Anniversary" Section="Personal">
        <xsl:value-of select="$string/contact/Anniversary"/>
      </Field>
      <Field Name="Children" Section="Personal">
        <xsl:value-of select="$string/contact/Children"/>
      </Field>
      <Field Name="Assistant" Section="Personal">
        <xsl:value-of select="$string/contact/AssistantName"/>
      </Field>
      <Field Name="Assistant Phone" Section="Personal">
        <xsl:value-of select="$string/contact/AssistantTelephoneNumber"/>
      </Field>
      <Field Name="Gender" Section="Personal">
        <xsl:value-of select="$string/contact/Gender"/>
      </Field>
      <Field Name="Hobby" Section="Personal">
        <xsl:value-of select="$string/contact/Hobby"/>
      </Field>


      <Field Name="Primary Phone" Section="Work">
        <xsl:value-of select="$string/contact/CompanyMainTelephoneNumber"/>
      </Field>
      <Field Name="Phone" Section="Work">
        <xsl:value-of select="$string/contact/BusinessTelephoneNumber"/>
      </Field>
      <Field Name="Phone2" Section="Work">
        <xsl:value-of select="$string/contact/Business2TelephoneNumber"/>
      </Field>
      <Field Name="Fax" Section="Work">
        <xsl:value-of select="$string/contact/BusinessFaxNumber"/>
      </Field>
      <Field Name="PO Box" Section="Work">
        <xsl:value-of select="$string/contact/BusinessAddressPostOfficeBox"/>
      </Field>
      <Field Name="Office Location" Section="Work">
        <xsl:value-of select="$string/contact/OfficeLocation"/>
      </Field>
      <Field Name="Street" Section="Work">
        <xsl:value-of select="$string/contact/BusinessAddressStreet"/>
      </Field>
      <Field Name="City" Section="Work">
        <xsl:value-of select="$string/contact/BusinessAddressCity"/>
      </Field>
      <Field Name="State" Section="Work">
        <xsl:value-of select="$string/contact/BusinessAddressState"/>
      </Field>
      <Field Name="Country" Section="Work">
        <xsl:value-of select="$string/contact/BusinessAddressCountry"/>
      </Field>
      <Field Name="Postcode" Section="Work">
        <xsl:value-of select="$string/contact/BusinessAddressPostalCode"/>
      </Field>
      <Field Name="Title" Section="Work">
        <xsl:value-of select="$string/contact/JobTitle"/>
      </Field>
      <Field Name="Company" Section="Work">
        <xsl:value-of select="$string/contact/CompanyName"/>
      </Field>
      <Field Name="Department" Section="Work">
        <xsl:value-of select="$string/contact/Department"/>
      </Field>
      <Field Name="Profession" Section="Work">
        <xsl:value-of select="$string/contact/Profession"/>
      </Field>
      <Field Name="Manager" Section="Work">
        <xsl:value-of select="$string/contact/ManagerName"/>
      </Field>
      <Field Name="Web" Section="Work">
        <xsl:value-of select="$string/contact/BusinessWebPage"/>
      </Field>


      <Field Name="Phone" Section="Other">
        <xsl:value-of select="$string/contact/OtherTelephoneNumber"/>
      </Field>
      <Field Name="Fax" Section="Other">
        <xsl:value-of select="$string/contact/OtherFaxNumber"/>
      </Field>
      <Field Name="PO Box" Section="Other">
        <xsl:value-of select="$string/contact/OtherAddressPostOfficeBox"/>
      </Field>
      <Field Name="Street" Section="Other">
        <xsl:value-of select="$string/contact/OtherAddressStreet"/>
      </Field>
      <Field Name="City" Section="Other">
        <xsl:value-of select="$string/contact/OtherAddressCity"/>
      </Field>
      <Field Name="State" Section="Other">
        <xsl:value-of select="$string/contact/OtherAddressState"/>
      </Field>
      <Field Name="Country" Section="Other">
        <xsl:value-of select="$string/contact/OtherAddressCountry"/>
      </Field>
      <Field Name="Postcode" Section="Other">
        <xsl:value-of select="$string/contact/OtherAddressPostalCode"/>
      </Field>
    </C>
  </xsl:template>

  <!--Template for converting SIFC that may represent company.-->
  <xsl:template name="Company">
    <xsl:param name="string" />

    <C>
      <xsl:attribute name="ID">
        <xsl:value-of select="$string/@ID"/>
      </xsl:attribute>
      <xsl:attribute name="IsCompany">
        <xsl:text>1</xsl:text>
      </xsl:attribute>

      <xsl:choose>
        <xsl:when test="$string/contact/CompanyName=''">
          <Field Name="FullName" Section="">
            <xsl:value-of select="$string/contact/FileAs"/>
          </Field>
        </xsl:when>
        <xsl:otherwise>
          <Field Name="FullName" Section="">
            <xsl:value-of select="$string/contact/CompanyName"/>
          </Field>
        </xsl:otherwise>
      </xsl:choose>
      
      <Field Name="Notes" Section="">
        <xsl:value-of select="$string/contact/Body"/>
      </Field>

      <Field Name="Primary Phone" Section="Company">
        <xsl:value-of select="$string/contact/CompanyMainTelephoneNumber"/>
      </Field>
      <Field Name="Phone" Section="Company">
        <xsl:value-of select="$string/contact/BusinessTelephoneNumber"/>
      </Field>
      <Field Name="Phone2" Section="Company">
        <xsl:value-of select="$string/contact/Business2TelephoneNumber"/>
      </Field>
      <Field Name="Fax" Section="Company">
        <xsl:value-of select="$string/contact/BusinessFaxNumber"/>
      </Field>
      <Field Name="PO Box" Section="Company">
        <xsl:value-of select="$string/contact/BusinessAddressPostOfficeBox"/>
      </Field>
      <Field Name="Office Location" Section="Company">
        <xsl:value-of select="$string/contact/OfficeLocation"/>
      </Field>
      <Field Name="Street" Section="Company">
        <xsl:value-of select="$string/contact/BusinessAddressStreet"/>
      </Field>
      <Field Name="City" Section="Company">
        <xsl:value-of select="$string/contact/BusinessAddressCity"/>
      </Field>
      <Field Name="State" Section="Company">
        <xsl:value-of select="$string/contact/BusinessAddressState"/>
      </Field>
      <Field Name="Country" Section="Company">
        <xsl:value-of select="$string/contact/BusinessAddressCountry"/>
      </Field>
      <Field Name="Postcode" Section="Company">
        <xsl:value-of select="$string/contact/BusinessAddressPostalCode"/>
      </Field>
      <Field Name="Title" Section="Company">
        <xsl:value-of select="$string/contact/JobTitle"/>
      </Field>
      <Field Name="Department" Section="Company">
        <xsl:value-of select="$string/contact/Department"/>
      </Field>
      <Field Name="Profession" Section="Company">
        <xsl:value-of select="$string/contact/Profession"/>
      </Field>
      <Field Name="Manager" Section="Company">
        <xsl:value-of select="$string/contact/ManagerName"/>
      </Field>
      <Field Name="Web" Section="Company">
        <xsl:value-of select="$string/contact/BusinessWebPage"/>
      </Field>

    </C>
  </xsl:template>

</xsl:stylesheet>