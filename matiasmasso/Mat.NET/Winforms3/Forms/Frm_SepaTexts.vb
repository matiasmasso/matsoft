Public Class Frm_SepaTexts

    Private _DefaultValue As DTOSepaText

    Private Async Sub Frm_SepaTexts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_Langs1.Value = Current.Session.Lang
        Await refresca()
    End Sub


    Private Async Sub Xl_SepaTexts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SepaTexts1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oSepaTexts = Await FEB.SepaTexts.All(exs)
        If exs.Count = 0 Then
            Xl_SepaTexts1.Load(oSepaTexts, CurrentLang)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function CurrentLang() As DTOLang
        Return Xl_Langs1.Value
    End Function

    Private Async Sub Xl_Langs1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Langs1.AfterUpdate
        Await refresca()
    End Sub

    Private Async Sub Xl_SepaTexts1_RequestToUpdate(sender As Object, e As MatEventArgs) Handles Xl_SepaTexts1.RequestToUpdate
        Dim oSepaText As DTOSepaText = e.Argument.src
        Dim exs As New List(Of Exception)
        If Await FEB.SepaText.Update(oSepaText, exs) Then
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_SepaTexts1.Filter = e.Argument
    End Sub
End Class