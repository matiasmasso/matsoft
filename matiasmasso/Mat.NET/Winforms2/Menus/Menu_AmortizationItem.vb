Public Class Menu_AmortizationItem

    Inherits Menu_Base

    Private _AmortizationItems As List(Of DTOAmortizationItem)
    Private _AmortizationItem As DTOAmortizationItem


    Public Sub New(ByVal oAmortizationItems As List(Of DTOAmortizationItem))
        MyBase.New()
        _AmortizationItems = oAmortizationItems
        If _AmortizationItems IsNot Nothing Then
            If _AmortizationItems.Count > 0 Then
                _AmortizationItem = _AmortizationItems.First
            End If
        End If
    End Sub

    Public Sub New(ByVal oAmortizationItem As DTOAmortizationItem)
        MyBase.New()
        _AmortizationItem = oAmortizationItem
        _AmortizationItems = New List(Of DTOAmortizationItem)
        If _AmortizationItem IsNot Nothing Then
            _AmortizationItems.Add(_AmortizationItem)
        End If
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Cca(),
        MenuItem_Delete()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _AmortizationItems.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Cca() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Assentament"
        oMenuItem.Enabled = _AmortizationItems.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Cca
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedeix"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_AmortizationItem(_AmortizationItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Cca(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Cca(_AmortizationItem.Cca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Rc As MsgBoxResult = MsgBox("retrocedim la quota d'amortització?", MsgBoxStyle.OkCancel, "MAT.NET")
        If Rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.AmortizationItem.Delete(_AmortizationItem, exs) Then
                MsgBox("quota donada de baixa", MsgBoxStyle.Information, "MAT.NET")
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub


End Class


