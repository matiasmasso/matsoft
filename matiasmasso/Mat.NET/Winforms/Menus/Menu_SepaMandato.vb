Public Class Menu_SepaMandato

    Inherits Menu_Base

    Private _SepaMandatos As List(Of DTOSepaMandato)
    Private _SepaMandato As DTOSepaMandato

    Public Sub New(ByVal oSepaMandatos As List(Of DTOSepaMandato))
        MyBase.New()
        _SepaMandatos = oSepaMandatos
        If _SepaMandatos IsNot Nothing Then
            If _SepaMandatos.Count > 0 Then
                _SepaMandato = _SepaMandatos.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSepaMandato As DTOSepaMandato)
        MyBase.New()
        _SepaMandato = oSepaMandato
        _SepaMandatos = New List(Of DTOSepaMandato)
        If _SepaMandato IsNot Nothing Then
            _SepaMandatos.Add(_SepaMandato)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _SepaMandatos.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
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
        Dim oFrm As New Frm_SepaMandato(_SepaMandato)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.SepaMandato.Delete(_SepaMandatos.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


