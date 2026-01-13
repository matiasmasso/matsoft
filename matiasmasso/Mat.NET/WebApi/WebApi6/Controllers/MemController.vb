Public Class MemController

    Inherits _BaseController

    <HttpPost>
    <Route("api/mems")>
    Public Function Mems(UserContact As DUI.UserContact) As List(Of DUI.Mem)
        Dim retval As New List(Of DUI.Mem)
        Dim oUser As DTOUser = BLLUser.Find(UserContact.User.Guid)
        Dim oContact As New DTOContact(UserContact.Contact.Guid)
        If oUser IsNot Nothing Then
            Dim oMems As List(Of DTOMem) = BLLMems.All(oContact, IIf(oUser.Rol.IsRep, DTOMem.Cods.Rep, DTOMem.Cods.NotSet), oUser)
            For Each oMem As DTOMem In oMems
                Dim item As New DUI.Mem
                With item
                    .Fch = oMem.Fch
                    .Text = oMem.Text
                    .User = BLLUser.NicknameOrElse(oMem.Usr)
                End With
                retval.Add(item)
            Next
        End If
        Return retval
    End Function

    <HttpPost>
    <Route("api/mem/update")>
    Public Function Update(duiMem As DUI.Mem) As DUI.TaskResult
        Dim retval As New DUI.TaskResult
        Dim oUser As DTOUser = BLLUser.Find(New Guid(duiMem.User))
        Dim oContact As New DTOContact(New Guid(duiMem.Contact))
        If oUser Is Nothing Then
            retval.Message = oUser.Lang.Tradueix("usuario desconocido", "usuari desconegut", "unknown user")
        Else
            Dim oMem As New DTOMem()
            With oMem
                .Usr = oUser
                .Fch = Today
                .Text = duiMem.Text
                .Contact = oContact
                .Cod = DTOMem.Cods.Rep
            End With

            Dim exs As New List(Of Exception)
            If BLLMem.Update(oMem, exs) Then
                retval.Success = True
            Else
                retval.Message = BLLExceptions.ToFlatString(exs)
            End If
        End If
        Return retval
    End Function
End Class