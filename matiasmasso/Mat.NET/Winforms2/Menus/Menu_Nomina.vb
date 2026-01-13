Public Class Menu_Nomina

    Inherits Menu_Base

    Private _Nominas As List(Of DTONomina)
    Private _Nomina As DTONomina

    Public Sub New(ByVal oNominas As List(Of DTONomina))
        MyBase.New()
        _Nominas = oNominas
        If _Nominas IsNot Nothing Then
            If _Nominas.Count > 0 Then
                _Nomina = _Nominas.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oNomina As DTONomina)
        MyBase.New()
        _Nomina = oNomina
        _Nominas = New List(Of DTONomina)
        If _Nomina IsNot Nothing Then
            _Nominas.Add(_Nomina)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Browse())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Fitxa"
        oMenuItem.Enabled = _Nominas.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Document"
        oMenuItem.Enabled = _Nominas.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Browse
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
        Dim oFrm As New Frm_Nomina(_Nomina)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If _Nomina.cca Is Nothing Then
            If Not FEB.Nomina.Load(_Nomina, exs) Then
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If
        UIHelper.ShowPdf(_Nomina.cca.docFile)
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquesta nómina?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Nomina.Delete(_Nominas.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

