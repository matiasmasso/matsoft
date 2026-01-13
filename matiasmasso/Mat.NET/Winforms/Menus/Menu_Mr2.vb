

Public Class Menu_Mr2
    Private mMr2 As Mr2

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oMr2 As Mr2)
        MyBase.New()
        mMr2 = oMr2
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Retrocedeix()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================


    Private Function MenuItem_Retrocedeix() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Enabled = Not BLL.BLLApp.CcaIsBlockedYear(mMr2.Fch.Year)
        oMenuItem.Text = "retrocedir"
        oMenuItem.Image = My.Resources.UNDO
        AddHandler oMenuItem.Click, AddressOf Do_Retrocedeix
        Return oMenuItem
    End Function

  


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

  
    Private Sub Do_Retrocedeix(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Rc As MsgBoxResult = MsgBox("retrocedim la quota d'amortització?", MsgBoxStyle.OkCancel, "MAT.NET")
        If Rc = MsgBoxResult.Ok Then
            Dim sErr As String = ""
            If mMr2.Delete(sErr) Then
                MsgBox("quota donada de baixa", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(sender, e)
            Else
                MsgBox("Operació no efectuada." & vbCrLf & sErr, MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
