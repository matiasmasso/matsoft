Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class IbanController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Iban/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Iban.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'Iban des del Guid")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Iban/FromCcc/{ccc}")>
    Public Function Find(ccc As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Iban.FromCcc(ccc)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'Iban des del Ccc")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Iban")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)

        Dim resultHash As String = ""
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOIban)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el Iban")
            Else
                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If
                If BEBL.Iban.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.IbanLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.DocFileLoader.Upload")
        End Try

        Return result
    End Function



    <HttpGet>
    <Route("api/Iban/img/{guid}/{lang}")>
    Public Function img(guid As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oIban = BEBL.Iban.Find(guid)
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.Iban.Img(oIban, oLang)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descarregar la imatge de l'Iban")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Iban/img/{lang}")>
    Public Function img(lang As String, <FromBody> oIban As DTOIban) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim oBytes = BEBL.Iban.Img(oIban, oLang)
            Dim value = ImageMime.Factory(oBytes)
            retval = MyBase.HttpImageMimeResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la imatge de l'Iban")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/Iban/img/fromCcc/{ccc}/{lang}")>
    Public Function imgFromDigits(ccc As String, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.Iban.Img(ccc, oLang)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la imatge de l'Iban")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Iban/delete")>
    Public Function Delete(<FromBody> value As DTOIban) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Iban.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Iban")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Iban")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Iban/FromContact/{contact}/{cod}/{fch}")>
    Public Function Iban(contact As Guid, cod As DTOIban.Cods, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact = BEBL.Contact.Find(contact)
            If oContact Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("Contacte desconegut")
            Else
                Dim value As DTOIban = Nothing
                Dim values = BEBL.Ibans.FromContact(oContact, OnlyVigent:=True, oCod:=cod, DtFch:=fch)
                Select Case values.Count
                    Case 0
                        retval = Request.CreateResponse(HttpStatusCode.OK)
                    Case 1
                        value = values.First
                        retval = Request.CreateResponse(HttpStatusCode.OK, value)
                    Case Else
                        retval = MyBase.HttpErrorResponseMessage("Contacte amb més de un Iban vigent")
                End Select
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Ibans")
        End Try
        Return retval
    End Function

End Class

Public Class IbansController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Ibans/{emp}/{status}")>
    Public Function All(emp As DTOEmp.Ids, status As DTOIban.StatusEnum) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.Ibans.All(oEmp, status)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Ibans")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ibans/Clients/{emp}")>
    Public Function Clients(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.Ibans.Clients(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Ibans")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ibans/FromContact/{contact}/{onlyVigent}/{cod}")>
    Public Function FromContact(contact As Guid, onlyvigent As Integer, cod As DTOIban.Cods) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact = BEBL.Contact.Find(contact)
            If oContact Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("Contacte desconegut")
            Else
                Dim values = BEBL.Ibans.FromContact(oContact, (onlyvigent = 1), cod, Nothing)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Ibans")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ibans/FromBank/{emp}/{bank}/{onlyVigent}")>
    Public Function FromBank(emp As Integer, bank As Guid, onlyvigent As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oBank As New DTOBank(bank)
            Dim values = BEBL.Ibans.FromBank(oEmp, oBank, (onlyvigent = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Ibans")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ibans/FromBankBranch/{emp}/{bankbranch}/{onlyVigent}")>
    Public Function FromBankBranch(emp As Integer, bankbranch As Guid, onlyvigent As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oBankBranch As New DTOBankBranch(bankbranch)
            Dim values = BEBL.Ibans.FromBankBranch(oEmp, oBankBranch, (onlyvigent = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Ibans")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ibans/PendingUploads/{user}")>
    Public Function PendingUploads(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim values = BEBL.Ibans.PendingUploads(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Ibans")
        End Try
        Return retval
    End Function


End Class
