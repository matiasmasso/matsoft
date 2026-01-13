Public Class Menu_CliProductBlocked
    Private _CliProductsBlocked As List(Of DTOCliProductBlocked)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oCliProductsBlocked As List(Of DTOCliProductBlocked))
        MyBase.New()
        _CliProductsBlocked = oCliProductsBlocked
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Client(),
        MenuItem_Delete()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Client() As ToolStripMenuItem
        Dim exs As New List(Of Exception)

        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Client..."
        If _CliProductsBlocked.Count = 1 Then
            Dim oContactMenu = FEB.ContactMenu.FindSync(exs, _CliProductsBlocked.First.contact)
            Dim oMenuContact As New Menu_Contact(_CliProductsBlocked.First.contact, oContactMenu)
            oMenuItem.DropDownItems.AddRange(oMenuContact.Range)
        Else
            oMenuItem.Enabled = False
        End If
        Return oMenuItem
    End Function


    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CliProductBlocked(_CliProductsBlocked.First)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquests " & _CliProductsBlocked.Count & " registres?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.CliProductsBlocked.Delete(exs, _CliProductsBlocked) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar els registres")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class

