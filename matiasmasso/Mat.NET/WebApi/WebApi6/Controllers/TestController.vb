Imports Newtonsoft.Json.Linq
Public Class TestController
    Inherits ApiController

    <HttpGet>
    <Route("api/about")>
    Public Function aboutGet() As Object

        Dim adr As New JObject
        adr.Add("direccion", "Diagonal 403")
        adr.Add("población", "Barcelona")
        adr.Add("código postal", "08008")

        Dim o = New JObject()
        o.Add("razon social", "MATIAS MASSO, S.A.")
        o.Add("oficinas", adr)
        o.Add("teléfono", "932541522")
        o.Add("email", "info@matiasmasso.es")
        o.Add("web", "www.matiasmasso.es")
        o.Add("metodo", "GET")

        Return o
    End Function

    <HttpPost>
    <Route("api/about")>
    Public Function aboutPost(data As Object) As Object
        Dim sNom As String = data("posted").value

        Dim adr As New JObject
        adr.Add("direccion", "Diagonal 403")
        adr.Add("población", "Barcelona")
        adr.Add("código postal", "08008")

        Dim o = New JObject()
        o.Add("razon social", "MATIAS MASSO, S.A.")
        o.Add("oficinas", adr)
        o.Add("teléfono", "932541522")
        o.Add("email", "info@matiasmasso.es")
        o.Add("web", "www.matiasmasso.es")
        o.Add("posted", sNom)

        Return o
    End Function
End Class
