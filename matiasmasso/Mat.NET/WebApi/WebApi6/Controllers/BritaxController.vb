Imports System.Net
Imports System.Net.Http.Formatting

Imports System.Web.Http
Imports System.Net.Http
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Web
Imports System.Net.Mail
Imports System.IO

Public Class BritaxController
    Inherits ApiController

    <HttpGet>
    <Route("api/britax/storelocator")>
    Public Function storelocator(contactProduct As DUI.ContactProduct) As XML.Customer2
        'file from Lyndsey email Nadja from 29/4/2015
        Dim oXML As New XML
        Dim oMM As New XML.Customer2
        With oMM
            .AccountNum = 687
            .Name = "MATIAS MASSO, S.A."
            .Street = "Diagonal 403"
            .ZipCode = "08008"
            .City = "Barcelona"
            .Country = "ES"
            .Phone = "932541522"
            .Email = "info@matiasmasso.es"
            .Web = "https://www.matiasmasso.es"
            .Distributor = True
        End With
        ' oXML.Customers.Add(oMM)

        'BLLProductDistributors.BritaxXml()

        'Dim src As String = BLL.BLLProductDistributors.BritaxXml
        'HttpContext.Response.AddHeader("content-disposition", "attachment; filename='MATIAS MASSO sale points network.csv'")
        'Dim oStream As Byte() = System.Text.Encoding.UTF8.GetBytes(src)
        'Dim retval As New Mvc.FileContentResult(oStream, "text/xml")

        Dim s As String
        Dim oFormatter As New XmlMediaTypeFormatter
        oFormatter.CreateXmlSerializer(GetType(String))
        Dim responseMessage As New HttpResponseMessage(HttpStatusCode.OK)
        'Dim Content As New ObjectContent(Of String)(oMM, oFormatter)
        'UseXmlSerializer = True

        Dim retval As XML.Customer2 = oMM
        Return retval

    End Function

    Function FileResult(oStream As Byte(), sFilename As String) As System.Web.Mvc.FileResult
        Dim retval As Mvc.FileResult = Nothing
        Dim oMime As DTOEnums.MimeCods = FileSystemHelper.GetMimeFromExtension(sFilename)
        Dim sContentType As String = MediaHelper.ContentType(oMime)
        retval = New System.Web.Mvc.FileContentResult(oStream, sContentType) ' "application/pdf")
        retval.FileDownloadName = sFilename ' Server.UrlEncode(sFilename)
        Return retval
    End Function


    Public Class XML

        Property Customers As List(Of Customer2)
        Public Class Customer2
            Property AccountNum As Integer
            Property Name As String
            Property Street As String
            Property ZipCode As String
            Property City As String
            Property Country As String
            Property Phone As String
            Property Email As String
            Property Web As String
            Property Distributor As Boolean

        End Class
    End Class
End Class

