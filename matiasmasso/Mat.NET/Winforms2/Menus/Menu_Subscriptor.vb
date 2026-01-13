Public Class Menu_Subscriptor
    Private _Subscriptors As List(Of DTOSubscriptor)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)


    Public Sub New(ByVal oSubscriptors As List(Of DTOSubscriptor))
        MyBase.New()
        _Subscriptors = oSubscriptors
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Email(),
                                         MenuItem_Subscripcio(),
                                         MenuItem_UnSubscribe()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Email() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "email"
        Dim oMenu_Email As New Menu_User(_Subscriptors.First)
        oMenuItem.DropDownItems.AddRange(oMenu_Email.Range)
        Return oMenuItem
    End Function

    Private Function MenuItem_Subscripcio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "subscripció"
        Dim oMenu_Subscripcio As New Menu_Subscripcio(_Subscriptors.First.Subscription)
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

    Private Async Sub Do_UnSubscribe(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult
        If _Subscriptors.Count = 1 Then
            rc = MsgBox("retirem la subscripció a " & _Subscriptors.First.EmailAddress & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        Else
            rc = MsgBox(String.Format("retirem aquests {0} subscriptors?", _Subscriptors.Count), MsgBoxStyle.OkCancel, "MAT.NET")
        End If
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Subscriptors.Unsubscribe(exs, _Subscriptors) Then
                RefreshRequest(Me, New MatEventArgs(_Subscriptors))
            Else
                MsgBox(ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operacio cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Subscriptors))
    End Sub

End Class


