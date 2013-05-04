<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="html"/>

  <xsl:template match="/xsl:stylesheet">

    <table border="1" width="100%" id="table2">
      <tr>

        <td>
          <b>OC Fields</b>
        </td>
        <td>
          <b>SIF-C Fields</b>
        </td>
      </tr>
      <xsl:for-each select="xsl:template/Field">
        <tr>
          <td>
            <xsl:value-of select="@Section"/>/<xsl:value-of select="@Name"/>
          </td>
          <td>
            <xsl:for-each select="xsl:value-of">
              <xsl:value-of select="@select"/>
            </xsl:for-each>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>
