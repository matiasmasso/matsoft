Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BankBranchController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BankBranch/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.BankBranch.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BankBranch")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/BankBranch/{bank}/{id}")>
    Public Function Find(bank As Guid, id As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBank = BEBL.Bank.Find(bank)
            Dim value = BEBL.BankBranch.Find(oBank, id)
            If value IsNot Nothing Then
                value.Bank = oBank
            End If
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BankBranch")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/BankBranch/{country}/{bankId}/{branchId}")>
    Public Function FromCod(country As Guid, bankId As String, branchId As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCountry As New DTOCountry(country)
            Dim value = BEBL.BankBranch.Find(oCountry, bankId, branchId)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BankBranch")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/BankBranch/fromIban/{IbanDigits}")>
    Public Function FromIban(ibanDigits As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.IbanStructure.GetBankBranch(ibanDigits)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BankBranch")
        End Try
        Return retval
    End Function

    'BankBranch/fromIban


    <HttpPost>
    <Route("api/BankBranch")>
    Public Function Update(<FromBody> value As DTOBankBranch) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BankBranch.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la BankBranch")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la BankBranch")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/BankBranch/delete")>
    Public Function Delete(<FromBody> value As DTOBankBranch) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BankBranch.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la BankBranch")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la BankBranch")
        End Try
        Return retval
    End Function

End Class

Public Class BankBranchesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BankBranches/fromBank/{bank}")>
    Public Function fromBank(bank As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBank As New DTOBank(bank)
            Dim values = BEBL.BankBranches.All(oBank)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BankBranchs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/BankBranches/fromLocation/{location}")>
    Public Function fromLocation(location As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLocation As New DTOLocation(location)
            Dim values = BEBL.BankBranches.All(oLocation)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BankBranchs")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/BankBranches/relocate/{locationTo}")>
    Public Function reLocate(locationTo As Guid, <FromBody> bankBranches() As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oLocation As New DTOLocation(locationTo)
            Dim oBankBranches As New List(Of DTOBankBranch)
            For Each guid In bankBranches
                oBankBranches.Add(New DTOBankBranch(guid))
            Next
            Dim value = BEBL.BankBranches.reLocate(exs, oLocation, oBankBranches)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els centres de un usuari")
        End Try
        Return retval
    End Function
End Class

