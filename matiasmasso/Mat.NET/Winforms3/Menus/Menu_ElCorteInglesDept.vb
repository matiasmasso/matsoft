Public Class Menu_ElCorteInglesDept
    Inherits Menu_Base

    Private _Values As List(Of DTO.Integracions.ElCorteIngles.Dept)
    Private _Value As DTO.Integracions.ElCorteIngles.Dept

    Public Sub New(ByVal oValues As List(Of DTO.Integracions.ElCorteIngles.Dept))
        MyBase.New()
        _Values = oValues
        If _Values IsNot Nothing Then
            If _Values.Count > 0 Then
                _Value = _Values.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oValue As DTO.Integracions.ElCorteIngles.Dept)
        MyBase.New()
        _Value = oValue
        _Values = New List(Of DTO.Integracions.ElCorteIngles.Dept)
        If _Value IsNot Nothing Then
            _Values.Add(_Value)
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
        oMenuItem.Enabled = _Values.Count = 1
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
        Dim oFrm As New Frm_ElCorteInglesDept(_Value)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.ElCorteInglesDept.Delete(exs, _Values.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub



End Class


