Public Class UtilitiesController
    Inherits _MatController





    Function Index() As ActionResult
        Return Nothing
    End Function

    Async Function Countries() As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oCountries As List(Of DTOGuidNom) = Await FEB2.Countries.GuidNoms(ContextHelper.GetLang, exs)
        Dim retval As JsonResult = Json(oCountries, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function ZipCodLocation(countryGuid As Guid, zipCod As String) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oCountry As New DTOCountry(countryGuid)
        Dim oLocation = Await FEB2.Location.FromZip(oCountry, zipCod, exs)
        Dim sLocation As String = ""
        If oLocation IsNot Nothing Then sLocation = oLocation.Nom
        Dim retval As JsonResult = Json(sLocation, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

End Class

Public Class XmlResult
    Inherits ActionResult
    Private m_objectToSerialize As Object

    ''' <summary>
    ''' Initializes a new instance of the <see cref="XmlResult"/> class.
    ''' </summary>
    ''' <param name="objectToSerialize">The object to serialize to XML.</param>
    Public Sub New(objectToSerialize As Object)
        Me.m_objectToSerialize = objectToSerialize
    End Sub

    ''' <summary>
    ''' Gets the object to be serialized to XML.
    ''' </summary>
    Public ReadOnly Property ObjectToSerialize() As Object
        Get
            Return Me.m_objectToSerialize
        End Get
    End Property

    ''' <summary>
    ''' Serialises the object that was passed into the constructor to XML and writes the corresponding XML to the result stream.
    ''' </summary>
    ''' <param name="context">The controller context for the current request.</param>
    Public Overrides Sub ExecuteResult(context As ControllerContext)
        If Me.m_objectToSerialize IsNot Nothing Then
            context.HttpContext.Response.Clear()
            Dim xs = New System.Xml.Serialization.XmlSerializer(Me.m_objectToSerialize.[GetType]())
            context.HttpContext.Response.ContentType = "text/xml"
            xs.Serialize(context.HttpContext.Response.Output, Me.m_objectToSerialize)
        End If
    End Sub
End Class
