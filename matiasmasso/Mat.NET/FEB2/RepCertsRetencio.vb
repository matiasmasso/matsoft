Public Class RepCertRetencio
    Inherits _FeblBase

    Shared Function CertFactoryUrl(oCert As DTORepCertRetencio) As String
        Dim oJson As New MatJSonObject()
        oJson.AddValuePair("Guid", oCert.Rep.Guid.ToString())
        oJson.AddValuePair("Year", oCert.Fch.Year)
        oJson.AddValuePair("Quarter", TimeHelper.Quarter(oCert.Fch))
        Dim sBase64 As String = oJson.ToBase64
        Dim retval As String = UrlHelper.Factory(True, "doc", DTODocFile.Cods.RepCertRetencio, sBase64)
        Return retval
    End Function

    Shared Async Function FromBase64(sBase64 As String) As Task(Of DTORepCertRetencio)
        Dim retval As DTORepCertRetencio = Nothing
        Dim oJson As MatJSonObject = MatJSonObject.FromBase64(sBase64)
        Dim oRepGuid As New Guid(oJson.GetValue("Guid"))
        Dim Year As Integer = oJson.GetValue("Year")
        Dim Quarter As Integer = oJson.GetValue("Quarter")
        Dim oRep As New DTORep(oRepGuid)
        Dim exs As New List(Of Exception)
        Dim values As List(Of DTORepCertRetencio) = Await FEB2.RepCertsRetencio.All(exs, oRep, Year, Quarter)
        If values.Count > 0 Then
            retval = values(0)
        End If
        Return retval
    End Function

End Class

Public Class RepCertsRetencio
    Inherits _FeblBase
    Shared Async Function All(exs As List(Of Exception), oRep As DTORep, Optional iYear As Integer = 0, Optional iQuarter As Integer = 0) As Task(Of List(Of DTORepCertRetencio))
        Return Await Api.Fetch(Of List(Of DTORepCertRetencio))(exs, "RepCertsRetencio", oRep.Guid.ToString, iYear, iQuarter)
    End Function

End Class
