Imports System.Net
Imports System.Web.Http

Public Class ValuesController
    Inherits ApiController

    ' GET api/values
    Public Function GetValues() As IEnumerable(Of String)
        Return New String() {"value1", "value2"}
    End Function


    <HttpGet>
    <Route("api/PatientSearch/{MRNumber}")>
    Public Function HelloWorld(MRNumber As Integer) As List(Of Person)
        'read as http://localhost:18734/api/PatientSearch/5
        Dim retval As New List(Of Person)
        retval.Add(New Person With {.Nom = "Joe", .Cognom = "Doe"})
        retval.Add(New Person With {.Nom = "Mary", .Cognom = "Smith"})
        Return retval
    End Function


    <HttpGet>
    <Route("api/Curs")>
    Public Function Curs() As List(Of DTOCur)
        Dim retval As New List(Of DTOCur)
        retval.Add(New DTOCur("EUR") With {.NomEsp = "Euro"})
        retval.Add(New DTOCur("USD") With {.NomEsp = "Dolar"})
        retval.Add(New DTOCur("GBP") With {.NomEsp = "Libra esterlina"})
        Return retval
    End Function

    ' GET api/values/5
    Public Function GetValue(ByVal id As Integer) As String
        Return "value"
    End Function

    ' POST api/values
    Public Sub PostValue(<FromBody()> ByVal value As String)

    End Sub

    ' PUT api/values/5
    Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub

    ' DELETE api/values/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub
End Class

Public Class Person
    Property Nom As String
    Property Cognom As String
End Class
