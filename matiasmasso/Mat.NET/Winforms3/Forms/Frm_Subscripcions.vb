

'Imports System.Net.PeerToPeer.Collaboration

Public Class Frm_Subscripcions
    Private mAllowEvents As Boolean = False

    Private Enum Cols
        Id
        Nom
    End Enum

    Private Async Sub Frm_Subscripcions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Await LoadCollection()
    End Sub

    Private Async Function LoadCollection() As Task
        Dim exs As New List(Of Exception)
        Dim oSubscripcions = Await FEB.Subscriptions.All(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            mAllowEvents = False
            ListBox1.Items.Clear()
            For Each oSubscripcio As DTOSubscription In oSubscripcions
                Dim oListItem As New MatListItem(oSubscripcio, DTOSubscription.NomOrId(oSubscripcio, Current.Session.Lang))
                ListBox1.Items.Add(oListItem)
            Next
            mAllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function CurrentItem() As DTOSubscription
        Dim oRetVal As DTOSubscription = Nothing
        If ListBox1.SelectedIndex > -1 Then
            Dim oItem As MatListItem = ListBox1.SelectedItem
            oRetVal = DirectCast(oItem.Value, DTOSubscription)
        End If
        Return oRetVal
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCurrentItem As DTOSubscription = CurrentItem()

        If oCurrentItem IsNot Nothing Then
            Dim oMenu_Subscripcio As New Menu_Subscripcio(oCurrentItem)
            'AddHandler oMenu_Subscripcio.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Subscripcio.Range)
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Async Sub Do_AddNew()
        Dim oSsc = DTOSubscription.Factory(GlobalVariables.Emp)
        Dim exs As New List(Of Exception)
        If Await FEB.Subscription.Update(exs, oSsc) Then
            Await LoadCollection()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Await Xl_Subscripcio1.Load(CurrentItem())
        SetContextMenu()
    End Sub

    Private Async Sub Xl_Subscripcio1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Subscripcio1.AfterUpdate
        Dim idx As Integer = ListBox1.SelectedIndex
        Await LoadCollection()
        ListBox1.SelectedIndex = idx
    End Sub

End Class