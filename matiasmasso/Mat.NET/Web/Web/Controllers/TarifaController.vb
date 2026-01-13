Public Class TarifaController
    Inherits _MatController


    Async Function Index(Optional contact As Nullable(Of Guid) = Nothing, Optional sFch As String = "") As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim Model As DTOTarifa = Nothing
        If contact Is Nothing Then
            Model = Await BLLTarifa.FromUser(exs, GetSession.User, Today)
        Else
            Dim oCustomer As DTOCustomer = BLL.BLLCustomer.Find(contact)
            Model = Await BLLTarifa.FromCustomer(exs, GlobalVariables.Emp, oCustomer, Today)
        End If
        Await MyBase.Log(DTOWebLog.LogCodes.Tarifa)
        Return View("Tarifa", Model)
    End Function


End Class