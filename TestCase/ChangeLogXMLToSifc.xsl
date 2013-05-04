<?xml version="1.0" encoding="UTF-8"?>
<!--Convert Open Contacts' Change log XML to Sif-C

Open Contacts manages data fields dynamically, and fields can be deleted. Deleting field is equivilent to clearing a static field
of traditional address book. Thus, Open Contacts needs to maintain a list of deleted fields, and present them in the Change Log XML
as dynamic fields with empty value, while can be transformed to empty static fields of SIFC.

The following fields are not implemented:
CallbackTelephoneNumber, CarTelephoneNumber, Categories, Companies, ComputerNetworkName, Importance,
Initials, Language, Mileage, OfficeLocation, OrganizationalIDNumber, Revision, RadioTelephoneNumber, Sensitivity,
Subject, TelexNumber, Timezone, Uid, YomiCompanyName, YomiFirstName.
 
There was not technical difficulty to implement these fields, and they can be added later for specific projects,
as long as the device can support these fields.

Please read the following comments for modifying the mapping between Open Contacts and Sif-C.

References: Sync4j SyncServer Developer's Guide (January 2005)

-->
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" encoding="UTF-8" />

  <xsl:template match="/OpenContactsChangeLog">
    <SIFCChangeLog>
      <LastAnchor>
        <xsl:value-of select="LastAnchor"/>
      </LastAnchor>
      <Source>
        <xsl:value-of select="Source"/>
      </Source>
      <Changes>
        <xsl:for-each select ="Changes/New/C">
          <New>
            <C>
              <xsl:attribute name="ID">
                <xsl:value-of select="@ID"/>
              </xsl:attribute>
              <xsl:apply-templates select="."/>
            </C>
          </New>
        </xsl:for-each>

        <xsl:for-each select ="Changes/Update/C">
          <Update>
            <C>
              <xsl:attribute name="ID">
                <xsl:value-of select="@ID"/>
              </xsl:attribute>
              <xsl:apply-templates select="."/>
            </C>
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
    </SIFCChangeLog>
  </xsl:template>

  <xsl:template match="C">
    <xsl:choose>
      <xsl:when test="@IsCompany='0'">
        <xsl:call-template name="Person">
          <xsl:with-param name="string" select="." />
        </xsl:call-template>
      </xsl:when>
      <xsl:when test="@IsCompany='1'">
        <xsl:call-template name="Company">
          <xsl:with-param name="string" select="." />
        </xsl:call-template>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <!-- There are 2 templates for personal contacts and organizational contacts.-->
  
  <!--Template for converting fields of a person.
  Generally you may modify a file name / field section pair to map a Sif-C element.
  -->
  <xsl:template name="Person">
    <xsl:param name="string" />
    <contact>
      <SIFVersion>1.0</SIFVersion>
      <xsl:for-each select="$string/Field">
        <xsl:choose>
          <!--Surname of the main section of Open Contacts is mapped to LastName of Sif-C-->
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
          <xsl:when test="@Name='MidName' and @Section=''">
            <MiddleName>
              <xsl:value-of select="."/>
            </MiddleName>
          </xsl:when>
          <xsl:when test="@Name='FullName' and @Section=''">
            <FileAs>
              <xsl:value-of select="."/>
            </FileAs>
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
          <!--Anniversary of the Personal section is mapped to Anniversary of Sif-C.-->
          <xsl:when test="@Name='Anniversary' and @Section='Personal'">
            <Anniversary>
              <xsl:value-of select="."/>
            </Anniversary>
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
            <Email1Address>
              <xsl:value-of select="."/>
            </Email1Address>
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
            <BusinessTelephoneNumber>
              <xsl:value-of select="."/>
            </BusinessTelephoneNumber>
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

  <!--Template for converting fields of a company-->
  <xsl:template name="Company">
    <xsl:param name="string" />
    <contact>
      <SIFVersion>1.0</SIFVersion>
      <xsl:for-each select="$string/Field">
        <xsl:choose>
          <xsl:when test="@Name='FullName' and @Section=''">
            <FileAs>
              <xsl:value-of select="."/>
            </FileAs>
            <CompanyName>
              <xsl:value-of select="."/>             
            </CompanyName>
          </xsl:when>
          <xsl:when test="@Name='Notes' and @Section=''">
            <Body>
              <xsl:value-of select="."/>
            </Body>
          </xsl:when>
          <xsl:when test="@Name='Phone2' and (@Section='Company' or @Section='Work')">
            <Business2TelephoneNumber>
              <xsl:value-of select="."/>
            </Business2TelephoneNumber>
          </xsl:when>
          <xsl:when test="@Name='City' and (@Section='Company' or @Section='Work')">
            <BusinessAddressCity>
              <xsl:value-of select="."/>
            </BusinessAddressCity>
          </xsl:when>
          <xsl:when test="@Name='Country' and (@Section='Company' or @Section='Work')">
            <BusinessAddressCountry>
              <xsl:value-of select="."/>
            </BusinessAddressCountry>
          </xsl:when>
          <xsl:when test="@Name='Postcode' and (@Section='Company' or @Section='Work')">
            <BusinessAddressPostalCode>
              <xsl:value-of select="."/>
            </BusinessAddressPostalCode>
          </xsl:when>
          <xsl:when test="@Name='PO Box' and (@Section='Company' or @Section='Work')">
            <BusinessAddressPostOfficeBox>
              <xsl:value-of select="."/>
            </BusinessAddressPostOfficeBox>
          </xsl:when>
          <xsl:when test="@Name='State' and (@Section='Company' or @Section='Work')">
            <BusinessAddressState>
              <xsl:value-of select="."/>
            </BusinessAddressState>
          </xsl:when>
          <xsl:when test="@Name='Street' and (@Section='Company' or @Section='Work')">
            <BusinessAddressStreet>
              <xsl:value-of select="."/>
            </BusinessAddressStreet>
          </xsl:when>
          <xsl:when test="@Name='Fax' and (@Section='Company' or @Section='Work')">
            <BusinessFaxNumber>
              <xsl:value-of select="."/>
            </BusinessFaxNumber>
          </xsl:when>
          <xsl:when test="@Name='Phone' and (@Section='Company' or @Section='Work')">
            <BusinessTelephoneNumber>
              <xsl:value-of select="."/>
            </BusinessTelephoneNumber>
          </xsl:when>
          <xsl:when test="@Name='Web' and (@Section='Company' or @Section='Work')">
            <BusinessWebPage>
              <xsl:value-of select="."/>
            </BusinessWebPage>
          </xsl:when>
          <xsl:when test="@Name='Primary Phone' and (@Section='Company' or @Section='Work')">
            <CompanyMainTelephoneNumber>
              <xsl:value-of select="."/>
            </CompanyMainTelephoneNumber>
          </xsl:when>
          <xsl:when test="@Name='Company' and (@Section='Company' or @Section='Work')">
            <CompanyName>
              <xsl:value-of select="."/>
            </CompanyName>
          </xsl:when>
          <xsl:when test="@Name='Department' and (@Section='Company' or @Section='Work')">
            <Department>
              <xsl:value-of select="."/>
            </Department>
          </xsl:when>
        </xsl:choose>

      </xsl:for-each>
    </contact>
  </xsl:template>

</xsl:stylesheet>