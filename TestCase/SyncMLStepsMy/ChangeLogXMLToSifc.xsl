<?xml version="1.0" encoding="UTF-8"?>
<!--Convert Change log xml to Sif-C

The order of converion of fields may or may not be significant to a device, however, the order is significant to
Open Contacts which lists fields dynamically.

The following fields are not implemented:
CallbackTelephoneNumber, CarTelephoneNumber, Categories, Companies, ComputerNetworkName, Importance,
Initials, Language, Mileage, OfficeLocation, OrganizationalIDNumber, Revision, RadioTelephoneNumber, Sensitivity,
Subject, TelexNumber, Timezone, Uid, YomiCompanyName, YomiFirstName.

There was not technical difficulty to implement these fields, and they can be added later for specific projects,
as long as the device can support these fields.

References: Sync4j SyncServer Developer's Guide (January 2005)

-->
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" encoding="UTF-8" />

  <xsl:template match="/">
    <contact>
    <SIFVersion>1.0</SIFVersion>
    <xsl:for-each select="C/Field">
      <xsl:choose>
        <xsl:when test="@Name='Surname' and @Section=''">
          <LastName>
            <xsl:value-of select="."/>
          </LastName>
        </xsl:when>
        <xsl:when test="@Name='GivenName' and @Section=''">
          <FirstName>
            <xsl:value-of select="."/>
          </FirstName>
        </xsl:when>
        <xsl:when test="@Name='Surname' and @Section=''">
          <LastName>
            <xsl:value-of select="."/>
          </LastName>
        </xsl:when>
        <xsl:when test="@Name='MidName' and @Section=''">
          <MiddleName>
            <xsl:value-of select="."/>
          </MiddleName>
        </xsl:when>
        <xsl:when test="@Name='Title' and @Section=''">
          <Title>
            <xsl:value-of select="."/>
          </Title>
        </xsl:when>
        <xsl:when test="@Name='Notes' and @Section=''">
          <Body>
            <xsl:value-of select="."/>
          </Body>
        </xsl:when>
        <xsl:when test="@Name='Anniversay' and @Section='Personal'">
          <Anniversay>
            <xsl:value-of select="."/>
          </Anniversay>
        </xsl:when>
        <xsl:when test="@Name='Assistant' and @Section='Personal'">
          <AssistantName>
            <xsl:value-of select="."/>
          </AssistantName>
        </xsl:when>
        <xsl:when test="@Name='Birthday' and @Section='Personal'">
          <Birthday>
            <xsl:value-of select="."/>
          </Birthday>
        </xsl:when>
        <xsl:when test="@Name='Mobile' and @Section='Personal'">
          <MobileTelephoneNumber>
            <xsl:value-of select="."/>
          </MobileTelephoneNumber>
        </xsl:when>
        <xsl:when test="@Name='Nickname' and @Section='Personal'">
          <NickName>
            <xsl:value-of select="."/>
          </NickName>
        </xsl:when>
        <xsl:when test="@Name='Children' and @Section='Personal'">
          <Children>
            <xsl:value-of select="."/>
          </Children>
        </xsl:when>
        <xsl:when test="@Name='Email' and @Section='Personal'">
          <EmailAddress>
            <xsl:value-of select="."/>
          </EmailAddress>
        </xsl:when>
        <xsl:when test="@Name='Email2' and @Section='Personal'">
          <Email2Address>
            <xsl:value-of select="."/>
          </Email2Address>
        </xsl:when>
        <xsl:when test="@Name='Email3' and @Section='Personal'">
          <Email3Address>
            <xsl:value-of select="."/>
          </Email3Address>
        </xsl:when>
        <xsl:when test="@Name='Gender' and @Section='Personal'">
          <Gender>
            <xsl:value-of select="."/>
          </Gender>
        </xsl:when>
        <xsl:when test="@Name='Hobby' and @Section='Personal'">
          <Hobby>
            <xsl:value-of select="."/>
          </Hobby>
        </xsl:when>
        <xsl:when test="@Name='Phone2' and @Section='Personal'">
          <Home2TelephoneNumber>
            <xsl:value-of select="."/>
          </Home2TelephoneNumber>
        </xsl:when>
        <xsl:when test="@Name='City' and @Section='Personal'">
          <HomeAddressCity>
            <xsl:value-of select="."/>
          </HomeAddressCity>
        </xsl:when>
        <xsl:when test="@Name='Country' and @Section='Personal'">
          <HomeAddressCountry>
            <xsl:value-of select="."/>
          </HomeAddressCountry>
        </xsl:when>
        <xsl:when test="@Name='Postcode' and @Section='Personal'">
          <HomeAddressPostalCode>
            <xsl:value-of select="."/>
          </HomeAddressPostalCode>
        </xsl:when>
        <xsl:when test="@Name='PO Box' and @Section='Personal'">
          <HomeAddressPostOfficeBox>
            <xsl:value-of select="."/>
          </HomeAddressPostOfficeBox>
        </xsl:when>
        <xsl:when test="@Name='State' and @Section='Personal'">
          <HomeAddressState>
            <xsl:value-of select="."/>
          </HomeAddressState>
        </xsl:when>
        <xsl:when test="@Name='Street' and @Section='Personal'">
          <HomeAddressStreet>
            <xsl:value-of select="."/>
          </HomeAddressStreet>
        </xsl:when>
        <xsl:when test="@Name='Fax' and @Section='Personal'">
          <HomeFaxNumber>
            <xsl:value-of select="."/>
          </HomeFaxNumber>
        </xsl:when>
        <xsl:when test="@Name='Phone' and @Section='Personal'">
          <HomeTelephoneNumber>
            <xsl:value-of select="."/>
          </HomeTelephoneNumber>
        </xsl:when>
        <xsl:when test="@Name='Web' and @Section='Personal'">
          <HomeWebPage>
            <xsl:value-of select="."/>
          </HomeWebPage>
        </xsl:when>
        <xsl:when test="@Name='Primary Phone' and @Section='Personal'">
          <PrimaryTelephoneNumber>
            <xsl:value-of select="."/>
          </PrimaryTelephoneNumber>
        </xsl:when>
        <xsl:when test="@Name='Spouse' and @Section='Personal'">
          <Spouse>
            <xsl:value-of select="."/>
          </Spouse>
        </xsl:when>
        <xsl:when test="@Name='Suffix' and @Section='Personal'">
          <Suffix>
            <xsl:value-of select="."/>
          </Suffix>
        </xsl:when>
        <xsl:when test="@Name='Web' and @Section='Personal'">
          <WebPage>
            <xsl:value-of select="."/>
          </WebPage>
        </xsl:when>
        <xsl:when test="@Name='Phone2' and @Section='Work'">
          <Business2TelephoneNumber>
            <xsl:value-of select="."/>
          </Business2TelephoneNumber>
        </xsl:when>
        <xsl:when test="@Name='City' and @Section='Work'">
          <BusinessAddressCity>
            <xsl:value-of select="."/>
          </BusinessAddressCity>
        </xsl:when>
        <xsl:when test="@Name='Country' and @Section='Work'">
          <BusinessAddressCountry>
            <xsl:value-of select="."/>
          </BusinessAddressCountry>
        </xsl:when>
        <xsl:when test="@Name='Postcode' and @Section='Work'">
          <BusinessAddressPostalCode>
            <xsl:value-of select="."/>
          </BusinessAddressPostalCode>
        </xsl:when>
        <xsl:when test="@Name='PO Box' and @Section='Work'">
          <BusinessAddressPostOfficeBox>
            <xsl:value-of select="."/>
          </BusinessAddressPostOfficeBox>
        </xsl:when>
        <xsl:when test="@Name='State' and @Section='Work'">
          <BusinessAddressState>
            <xsl:value-of select="."/>
          </BusinessAddressState>
        </xsl:when>
        <xsl:when test="@Name='Title' and @Section='Work'">
          <JobTitle>
            <xsl:value-of select="."/>
          </JobTitle>
        </xsl:when>
        <xsl:when test="@Name='Manager' and @Section='Work'">
          <ManagerName>
            <xsl:value-of select="."/>
          </ManagerName>
        </xsl:when>
        <xsl:when test="@Name='Street' and @Section='Work'">
          <BusinessAddressStreet>
            <xsl:value-of select="."/>
          </BusinessAddressStreet>
        </xsl:when>
        <xsl:when test="@Name='Fax' and @Section='Work'">
          <BusinessFaxNumber>
            <xsl:value-of select="."/>
          </BusinessFaxNumber>
        </xsl:when>
        <xsl:when test="@Name='Phone' and @Section='Work'">
          <BusinessBusinessTelephoneNumber>
            <xsl:value-of select="."/>
          </BusinessBusinessTelephoneNumber>
        </xsl:when>
        <xsl:when test="@Name='Web' and @Section='Work'">
          <BusinessWebPage>
            <xsl:value-of select="."/>
          </BusinessWebPage>
        </xsl:when>
        <xsl:when test="@Name='Primary Phone' and @Section='Work'">
          <CompanyMainTelephoneNumber>
            <xsl:value-of select="."/>
          </CompanyMainTelephoneNumber>
        </xsl:when>
        <xsl:when test="@Name='Company' and @Section='Work'">
          <CompanyName>
            <xsl:value-of select="."/>
          </CompanyName>
        </xsl:when>
        <xsl:when test="@Name='Department' and @Section='Work'">
          <Department>
            <xsl:value-of select="."/>
          </Department>
        </xsl:when>
        <xsl:when test="@Name='Profession' and @Section='Work'">
          <Profession>
            <xsl:value-of select="."/>
          </Profession>
        </xsl:when>
        <xsl:when test="@Name='City' and @Section='Other'">
          <OtherAddressCity>
            <xsl:value-of select="."/>
          </OtherAddressCity>
        </xsl:when>
        <xsl:when test="@Name='Country' and @Section='Other'">
          <OtherAddressCountry>
            <xsl:value-of select="."/>
          </OtherAddressCountry>
        </xsl:when>
        <xsl:when test="@Name='Postcode' and @Section='Other'">
          <OtherAddressPostalCode>
            <xsl:value-of select="."/>
          </OtherAddressPostalCode>
        </xsl:when>
        <xsl:when test="@Name='PO Box' and @Section='Other'">
          <OtherAddressPostOfficeBox>
            <xsl:value-of select="."/>
          </OtherAddressPostOfficeBox>
        </xsl:when>
        <xsl:when test="@Name='State' and @Section='Other'">
          <OtherAddressState>
            <xsl:value-of select="."/>
          </OtherAddressState>
        </xsl:when>
        <xsl:when test="@Name='Street' and @Section='Other'">
          <OtherAddressStreet>
            <xsl:value-of select="."/>
          </OtherAddressStreet>
        </xsl:when>
        <xsl:when test="@Name='Fax' and @Section='Other'">
          <OtherFaxNumber>
            <xsl:value-of select="."/>
          </OtherFaxNumber>
        </xsl:when>
        <xsl:when test="@Name='Phone' and @Section='Other'">
          <OtherTelephoneNumber>
            <xsl:value-of select="."/>
          </OtherTelephoneNumber>
        </xsl:when>
      </xsl:choose>

    </xsl:for-each>
    </contact>
  </xsl:template>

</xsl:stylesheet>