Public Class Frm_WtbolSites
    Private _WtbolSites As List(Of DTOWtbolSite)
    Private _DefaultValue As DTOWtbolSite
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse
    Private _TabIsLoaded(10) As Boolean

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        Clicks
        Baskets
    End Enum

    Public Sub New(Optional oDefaultValue As DTOWtbolSite = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_WtbolSites_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_WtbolSites1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_WtbolSites1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_WtbolSites1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_WtbolSites1.RequestToAddNew
        Dim oWtbolSite As DTOWtbolSite = DTOWtbolSite.Factory()
        Dim oFrm As New Frm_WtbolSite(oWtbolSite)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_WtbolSites1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_WtbolSites1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _WtbolSites = Await FEB.WtbolSites.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_WtbolSites1.Load(_WtbolSites, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)

        If Not _TabIsLoaded(TabControl1.SelectedIndex) Then
            ProgressBar1.Visible = True
            Select Case TabControl1.SelectedIndex
                Case Tabs.Clicks
                    Dim oClicks = Await FEB.WtbolCtrs.All(exs)
                    ProgressBar1.Visible = False
                    If exs.Count = 0 Then
                        Xl_WtbolSiteClicks1.Load(oClicks)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case Tabs.Baskets
                    Dim oBaskets = Await FEB.Wtbol.Baskets(Current.Session.User, exs)
                    ProgressBar1.Visible = False
                    If exs.Count = 0 Then
                        Xl_WtbolBaskets1.Load(oBaskets)
                    Else
                        UIHelper.WarnError(exs)
                    End If
            End Select

            _TabIsLoaded(TabControl1.SelectedIndex) = True
        End If
    End Sub


    Private Sub CopiarEmailsTécnicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopiarEmailsTécnicsToolStripMenuItem.Click
        Dim emails As String = FEB.WtbolSites.EmailsForBcc(_WtbolSites)
        UIHelper.CopyToClipboard(emails, "emails copiats al portapapers")
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_WtbolSites1.Filter = e.Argument
    End Sub
End Class