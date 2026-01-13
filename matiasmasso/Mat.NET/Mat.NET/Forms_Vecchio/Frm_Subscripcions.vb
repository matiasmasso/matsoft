

Public Class Frm_Subscripcions
    Private mEmp as DTOEmp = Nothing
    Private mAllowEvents As Boolean = False

    Private Enum Cols
        Id
        Nom
    End Enum

    Private Sub Frm_Subscripcions_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mEmp =BLL.BLLApp.Emp
        LoadCollection()
    End Sub

    Private Sub LoadCollection()
        Dim oSubscripcions As List(Of DTOSubscription) = BLL.BLLSubscriptions.All()
        mAllowEvents = False
        ListBox1.Items.Clear()
        For Each oSubscripcio As DTOSubscription In oSubscripcions
            Dim oListItem As New MaxiSrvr.MatListItem(oSubscripcio, BLL.BLLSubscription.NomOrId(oSubscripcio, BLL.BLLSession.Current.Lang))
            ListBox1.Items.Add(oListItem)
        Next
        mAllowEvents = True
    End Sub

    Private Function CurrentItem() As DTOSubscription
        Dim oRetVal As DTOSubscription = Nothing
        If ListBox1.SelectedIndex > -1 Then
            Dim oItem As MaxiSrvr.MatListItem = ListBox1.SelectedItem
            oRetVal = CType(oItem.Value, DTOSubscription)
        End If
        Return oRetVal
    End Function

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Xl_Subscripcio1.Subscripcio = CurrentItem()
    End Sub

    Private Sub Xl_Subscripcio1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Subscripcio1.AfterUpdate
        Dim idx As Integer = ListBox1.SelectedIndex
        LoadCollection()
        ListBox1.SelectedIndex = idx
    End Sub


End Class