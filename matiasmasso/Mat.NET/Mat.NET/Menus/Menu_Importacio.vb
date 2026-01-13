

Public Class Menu_Importacio
    Private mImportacio As Importacio

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oImportacio As Importacio)
        MyBase.New()
        mImportacio = oImportacio
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Intrastat(), _
        MenuItem_NewEntrada(), _
        MenuItem_NewImportacio(), _
        MenuItem_NewFra(), _
        MenuItem_Del()})
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

    Private Function MenuItem_Intrastat() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Intrastat"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Intrastat
        Return oMenuItem
    End Function

    Private Function MenuItem_NewEntrada() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "albará d'entrada"
        oMenuItem.Image = My.Resources.notepad
        AddHandler oMenuItem.Click, AddressOf Do_NewEntrada
        Return oMenuItem
    End Function

    Private Function MenuItem_NewFra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "entrada factura"
        oMenuItem.Visible = False 'tret perque per aqui no ho assigna a la remesa. Cal arrossegar el PDF a la remesa corresponent del llistat de remeses de importacio per posar en marxa el proces
        oMenuItem.Image = My.Resources.NewDoc
        AddHandler oMenuItem.Click, AddressOf Do_NewFra
        Return oMenuItem
    End Function

    Private Function MenuItem_NewImportacio() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Nova importacio de " & mImportacio.Proveidor.Nom_o_NomComercial
        AddHandler oMenuItem.Click, AddressOf Do_NewImportacio
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        If mImportacio Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.Enabled = (mImportacio.Items.Count = 0)
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        ImportacioLoader.Load(mImportacio)
        Dim oFrm As New Frm_Importacio(mImportacio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Intrastat(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oIntrastat As Intrastat = mImportacio.FirstIntrastat
        Dim oFrm As New Frm_Intrastat(oIntrastat)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem remesa " & mImportacio.Id & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then

            Dim exs as New List(Of exception)
            If mImportacio.Delete( exs) Then
                MsgBox("remesa " & mImportacio.Id & " eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, New System.EventArgs)
            Else
                MsgBox("error al eliminar el document de la remesa" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If

        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_NewEntrada(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_AlbNew2(mImportacio)
        oFrm.Show()
    End Sub

    Private Sub Do_NewFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Wz_Proveidor_NewFra(mImportacio)
        oFrm.Show()
    End Sub

    Private Sub Do_NewImportacio(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oImportacio As New Importacio(mImportacio.Proveidor, Today)
        Dim oFrm As New Frm_Importacio(oImportacio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    '===================================================================

    Private Sub SaveFra(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oCca As Cca = CType(sender, Cca)
        'Dim oItem As New ImportacioItem(ImportacioItem.SourceCodes.Fra, oCca.Guid)
        'mImportacio.Items.Add(oItem)
        'mImportacio.UpdateItems()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class
