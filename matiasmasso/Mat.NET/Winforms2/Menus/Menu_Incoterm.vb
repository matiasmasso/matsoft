Public Class Menu_Incoterm
    Inherits Menu_Base

    Private _Incoterms As List(Of DTOIncoterm)
    Private _Incoterm As DTOIncoterm

    Public Sub New(ByVal oIncoterms As List(Of DTOIncoterm))
        MyBase.New()
        _Incoterms = oIncoterms
        If _Incoterms IsNot Nothing Then
            If _Incoterms.Count > 0 Then
                _Incoterm = _Incoterms.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oIncoterm As DTOIncoterm)
        MyBase.New()
        _Incoterm = oIncoterm
        _Incoterms = New List(Of DTOIncoterm)
        If _Incoterm IsNot Nothing Then
            _Incoterms.Add(_Incoterm)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Incoterms.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Incoterm(_Incoterm)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Incoterm.Delete(exs, _Incoterms.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

