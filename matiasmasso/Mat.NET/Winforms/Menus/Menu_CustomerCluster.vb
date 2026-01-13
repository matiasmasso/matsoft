Public Class Menu_CustomerCluster
    Inherits Menu_Base

    Private _CustomerClusters As List(Of DTOCustomerCluster)
    Private _CustomerCluster As DTOCustomerCluster

    Public Sub New(ByVal oCustomerClusters As List(Of DTOCustomerCluster))
        MyBase.New()
        _CustomerClusters = oCustomerClusters
        If _CustomerClusters IsNot Nothing Then
            If _CustomerClusters.Count > 0 Then
                _CustomerCluster = _CustomerClusters.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oCustomerCluster As DTOCustomerCluster)
        MyBase.New()
        _CustomerCluster = oCustomerCluster
        _CustomerClusters = New List(Of DTOCustomerCluster)
        If _CustomerCluster IsNot Nothing Then
            _CustomerClusters.Add(_CustomerCluster)
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
        oMenuItem.Enabled = _CustomerClusters.Count = 1
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
        Dim oFrm As New Frm_CustomerCluster(_CustomerCluster)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem el cluster " & _CustomerCluster.Nom & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.CustomerCluster.Delete(exs, _CustomerClusters.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el cluster")
            End If
            Else
                MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
            End If
        End Sub

    End Class

