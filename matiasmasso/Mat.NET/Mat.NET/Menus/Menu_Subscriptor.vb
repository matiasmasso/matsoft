Public Class Menu_Subscriptor
    Private _Subscriptor As Subscriptor

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oSubscriptor As Subscriptor)
        MyBase.New()
        _Subscriptor = oSubscriptor
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Email(), _
                                         MenuItem_Subscripcio(), _
                                         MenuItem_UnSubscribe()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Email() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "email"
        Dim oMenu_Email As New Menu_Email(_Subscriptor)
        oMenuItem.DropDownItems.AddRange(oMenu_Email.Range)
        Return oMenuItem
    End Function

    Private Function MenuItem_Subscripcio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "subscripció"
        Dim oMenu_Subscripcio As New Menu_Subscripcio(_Subscriptor.Subscripcio)
        oMenuItem.DropDownItems.AddRange(oMenu_Subscripcio.Range)
        Return oMenuItem
    End Function

    Private Function MenuItem_UnSubscribe() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "retirar subscripció"
        oMenuItem.Image = My.Resources.DelText
        AddHandler oMenuItem.Click, AddressOf Do_UnSubscribe
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_UnSubscribe(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("retirem la subscripció a " & _Subscriptor.Adr & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If _Subscriptor.Unsubscribe(_Subscriptor.Subscripcio, exs) Then
                RefreshRequest(_Subscriptor, New System.EventArgs)
            Else
                MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operacio cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(_Subscriptor, New System.EventArgs)
    End Sub

End Class


