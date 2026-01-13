Public Class Frm_EdiExceptions
    Private _values As List(Of DTO.Integracions.Edi.Exception)
    Private Async Sub Frm_EdiExceptions_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Sub RequestToRefresh() Handles Xl_EdiExceptions1.RequestToRefresh
        Await Refresca()
    End Sub

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        _values = Await FEB.InvRpts.Exceptions(exs)
        If exs.Count = 0 Then
            Xl_EdiExceptions1.Load(_values)
        Else
            UIHelper.WarnError(exs)
        End If

    End Function


    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_EdiExceptions1.filter = e.Argument
    End Sub
End Class