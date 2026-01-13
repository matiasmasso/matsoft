Public Class Menu_MarketPlace
    Inherits Menu_Base

    Private _MarketPlaces As List(Of DTOMarketPlace)
    Private _MarketPlace As DTOMarketPlace

    Public Sub New(ByVal oMarketPlaces As List(Of DTOMarketPlace))
        MyBase.New()
        _MarketPlaces = oMarketPlaces
        If _MarketPlaces IsNot Nothing Then
            If _MarketPlaces.Count > 0 Then
                _MarketPlace = _MarketPlaces.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oMarketPlace As DTOMarketPlace)
        MyBase.New()
        _MarketPlace = oMarketPlace
        _MarketPlaces = New List(Of DTOMarketPlace)
        If _MarketPlace IsNot Nothing Then
            _MarketPlaces.Add(_MarketPlace)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_AddConsumerTicket())
        MyBase.AddMenuItem(MenuItem_Advanced())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _MarketPlaces.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_AddConsumerTicket() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Nou ticket"
        oMenuItem.Enabled = _MarketPlaces.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_AddConsumerTicket
        Return oMenuItem
    End Function

    Private Function MenuItem_Advanced() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avançat..."
        oMenuItem.DropDownItems.Add(MenuItem_CopyGuid())
        oMenuItem.DropDownItems.Add(MenuItem_Delete())
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyGuid() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar Guid"
        AddHandler oMenuItem.Click, AddressOf Do_CopyGuid
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
        Dim oFrm As New Frm_MarketPlace(_MarketPlace)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AddConsumerTicket(sender As Object, e As System.EventArgs)
        Dim oConsumerTicket = DTOConsumerTicket.Factory(Current.Session.User, _MarketPlace)
        Dim oFrm As New Frm_ConsumerTicketFactory(oConsumerTicket)
        AddHandler oFrm.afterUpdate, AddressOf RefreshRequest
        oFrm.show
    End Sub

    Private Sub Do_CopyGuid(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyToClipboard(_MarketPlace.Guid.ToString())
    End Sub


    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest Market Place?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.MarketPlace.Delete(exs, _MarketPlaces.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el Market Place")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

