Public Class Menu_PrNumero
    Private _Numeros As PrNumeros

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal onumero As PrNumero)
        MyBase.New()
        _Numeros = New PrNumeros
        _Numeros.Add(onumero)
    End Sub

    Public Sub New(ByVal onumeros As PrNumeros)
        MyBase.New()
        _Numeros = onumeros
    End Sub

    Public Function Range() As ToolStripMenuItem()
        'Return (New ToolStripMenuItem() {MenuItem_Zoom(), MenuItem_Change()})
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
        MenuItem_PdfBrowse(), _
        MenuItem_PdfImport(), _
        MenuItem_Del()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.Lupa
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_PdfBrowse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "veure portada"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_PdfBrowse
        Return oMenuItem
    End Function

    Private Function MenuItem_PdfImport() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "importar portada"
        oMenuItem.Image = My.Resources.disk
        AddHandler oMenuItem.Click, AddressOf Do_PdfImport
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "eliminar"
        oMenuItem.Image = My.Resources.aspa
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function





    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPrNumero As PrNumero = PrNumeroLoader.Find(_Numeros.First.Guid)
        Dim oFrm As New Frm_PrNumero(oPrNumero)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_PdfBrowse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oNumero As PrNumero = _Numeros.First
        UIHelper.ShowStream(oNumero.DocFile)
    End Sub

    Private Sub Do_PdfImport(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oNumero As PrNumero = _Numeros(0)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "IMPORTAR PORTADA DE " & oNumero.Revista.Nom
            .Filter = "documents pdf (*.pdf)|*.pdf"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim exs as New List(Of exception)
                Dim oDocFile As DTODocFile = BLL_DocFile.FromFile(.FileName, exs)
                If exs.Count = 0 Then
                    oNumero.DocFile = oDocFile
                    If PrNumeroLoader.Update(oNumero, exs) Then
                        RaiseEvent AfterUpdate(oNumero, New System.EventArgs)
                    Else
                        UIHelper.WarnError( exs, "error al guardar el fitxer")
                    End If
                Else
                    UIHelper.WarnError( exs, "error al importar el fitxer")
                End If
            End If
        End With
    End Sub

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPrNumero As PrNumero = _Numeros.First
        Dim exs as New List(Of exception)
        If PrNumeroLoader.Delete(oPrNumero, exs) Then
            RaiseEvent AfterUpdate(oPrNumero, New System.EventArgs)
        Else
            UIHelper.WarnError( exs, "error al eliminar el número de la revista")
        End If
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
