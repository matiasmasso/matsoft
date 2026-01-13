Public Class Menu_SepaMe
    Inherits Menu_Base

    Private _SepaMes As List(Of DTOSepaMe)
    Private _SepaMe As DTOSepaMe

    Public Sub New(ByVal oSepaMes As List(Of DTOSepaMe))
        MyBase.New()
        _SepaMes = oSepaMes
        If _SepaMes IsNot Nothing Then
            If _SepaMes.Count > 0 Then
                _SepaMe = _SepaMes.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSepaMe As DTOSepaMe)
        MyBase.New()
        _SepaMe = oSepaMe
        _SepaMes = New List(Of DTOSepaMe)
        If _SepaMe IsNot Nothing Then
            _SepaMes.Add(_SepaMe)
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
        oMenuItem.Enabled = _SepaMes.Count = 1
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
        Dim oFrm As New Frm_SepaMe(_SepaMe)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.SepaMe.Delete(exs, _SepaMes.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class


