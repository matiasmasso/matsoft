

Public Class Menu_Spv
    Private mSpv As spv

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oSpv As spv)
        MyBase.New()
        mSpv = oSpv
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
        MenuItem_Del(), _
        MenuItem_MailLabel()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        If mSpv.Id = 0 Then oMenuItem.Enabled = False
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_Del
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.DelText
        oMenuItem.Enabled = mSpv.AllowDelete
        Return oMenuItem
    End Function

    Private Function MenuItem_MailLabel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        AddHandler oMenuItem.Click, AddressOf Do_MailLabel
        oMenuItem.Text = "etiqueta per e-mail"
        oMenuItem.Image = My.Resources.MailSobreGroc
        Return oMenuItem
    End Function

    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Spv(mSpv)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            '.Spv = mSpv
            .Show()
        End With
    End Sub

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("eliminem la reparació num." & mSpv.Id & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mSpv.Delete( exs) Then
                MsgBox("Reparació num." & mSpv.Id & " eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(sender, e)
            Else
                MsgBox("Operació cancelada." & vbCrLf & "La reparació ja ha sortit." & vbCrLf & "Cal eliminar primer l'albará", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Else
            MsgBox("Operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub Do_MailLabel(ByVal sender As Object, ByVal e As System.EventArgs)
        mSpv.LabelEmailedTo = ""

        Dim exs as New List(Of exception)
        Dim oMailMessage As MailMessage = Nothing
        If mSpv.MailMessage(oMailMessage, exs) Then
            MatOutlook.NewMessage(oMailMessage)
        Else
            UIHelper.WarnError( exs, "error al redactar el missatge")
        End If

    End Sub

    '=======================================================================


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
