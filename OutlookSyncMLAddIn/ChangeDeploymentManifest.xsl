<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match ="/">
    <xsl:apply-templates mode ="copy"/>
  </xsl:template>
  <xsl:template match ="@*|node()" mode ="copy">
    <xsl:choose>
      <xsl:when test ="local-name()='expiration'">
        <beforeApplicationStartup xmlns="urn:schemas-microsoft-com:asm.v2"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:copy>
          <xsl:apply-templates select="@*"  mode="copy" />
          <xsl:apply-templates mode="copy" />
        </xsl:copy>

      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>
