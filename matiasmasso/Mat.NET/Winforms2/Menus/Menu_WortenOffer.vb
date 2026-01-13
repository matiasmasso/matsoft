Public Class Menu_WortenOffer
    Inherits Menu_Base

    Private _WortenOffers As List(Of DTO.Integracions.Worten.Offer)
    Private _WortenOffer As DTO.Integracions.Worten.Offer

    Public Sub New(ByVal oWortenOffers As List(Of DTO.Integracions.Worten.Offer))
        MyBase.New()
        _WortenOffers = oWortenOffers
        If _WortenOffers IsNot Nothing Then
            If _WortenOffers.Count > 0 Then
                _WortenOffer = _WortenOffers.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oWortenOffer As DTO.Integracions.Worten.Offer)
        MyBase.New()
        _WortenOffer = oWortenOffer
        _WortenOffers = New List(Of DTO.Integracions.Worten.Offer)
        If _WortenOffer IsNot Nothing Then
            _WortenOffers.Add(_WortenOffer)
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
        oMenuItem.Enabled = _WortenOffers.Count = 1
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
        Dim oFrm As New Frm_WortenOffer(_WortenOffer)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            'If Await FEB.WortenOffer.Delete(exs, _WortenOffers.First) Then
            ' MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            ' Else
            '    UIHelper.WarnError(exs, "error al eliminar el registre")
            'End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class


