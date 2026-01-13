Public Class Frm_EdiManager
    Private _FirstTab As Tabs
    Private _FirstIOCod As DTOEdiFile.IOCods
    Private _IsLoaded(10) As Boolean
    Private _AllowEvents As Boolean

    Public Enum Tabs
        Order
    End Enum

    Public Sub New(Optional ioCod As DTOEdiFile.IOCods = DTOEdiFile.IOCods.Inbox, Optional tab As Tabs = Tabs.Order)
        InitializeComponent()
        _FirstIOCod = ioCod
        _FirstTab = tab
    End Sub

    Private Async Sub Frm_EdiManager_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _FirstIOCod = DTOEdiFile.IOCods.Inbox Then
            RadioButtonInbox.Checked = True
        Else
            RadioButtonOutbox.Checked = True
        End If
        TabControl1.SelectedIndex = _FirstTab
        Await LoadSelectedTab()
        _AllowEvents = True
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If _AllowEvents Then
            Await LoadSelectedTab()
        End If
    End Sub

    Private Async Function LoadSelectedTab() As Task
        Dim exs As New List(Of Exception)
        If Not _IsLoaded(TabControl1.SelectedIndex) Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.Order
                    Await refrescaEdiOrders()
            End Select
        End If
    End Function



    Private Async Function refrescaEdiOrders() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB.EdiOrders.All(exs)
        If exs.Count = 0 Then
            Xl_EdiOrders1.Load(items)
            _IsLoaded(TabControl1.SelectedIndex) = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


End Class