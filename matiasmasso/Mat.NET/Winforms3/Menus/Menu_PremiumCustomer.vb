Public Class Menu_PremiumCustomer
    Inherits Menu_Base

    Private _PremiumCustomers As List(Of DTOPremiumCustomer)
    Private _PremiumCustomer As DTOPremiumCustomer

    Public Sub New(ByVal oPremiumCustomers As List(Of DTOPremiumCustomer))
        MyBase.New()
        _PremiumCustomers = oPremiumCustomers
        If _PremiumCustomers IsNot Nothing Then
            If _PremiumCustomers.Count > 0 Then
                _PremiumCustomer = _PremiumCustomers.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oPremiumCustomer As DTOPremiumCustomer)
        MyBase.New()
        _PremiumCustomer = oPremiumCustomer
        _PremiumCustomers = New List(Of DTOPremiumCustomer)
        If _PremiumCustomer IsNot Nothing Then
            _PremiumCustomers.Add(_PremiumCustomer)
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
        oMenuItem.Enabled = _PremiumCustomers.Count = 1
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
        Dim oFrm As New Frm_PremiumCustomer(_PremiumCustomer)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.premiumCustomer.Delete(_PremiumCustomers.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


