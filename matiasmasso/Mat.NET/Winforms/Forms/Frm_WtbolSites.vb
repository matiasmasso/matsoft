Public Class Frm_WtbolSites
    Private _WtbolSites As List(Of DTOWtbolSite)
    Private _DefaultValue As DTOWtbolSite
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        Serps
        CtrSites
        CtrProducts
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
        _WtbolSites = Await FEB2.WtbolSites.All(exs)
        If exs.Count = 0 Then
            Xl_WtbolSites1.Load(_WtbolSites, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Serps
                Await RefrescaSerps()
            Case Tabs.CtrSites
                Await RefrescaCtrSites()
            Case Tabs.CtrProducts
                Await RefrescaCtrProducts()
            Case Tabs.Baskets
                Await RefrescaBaskets()
        End Select
    End Sub

    Private Async Function RefrescaSerps() As Task
        Dim exs As New List(Of Exception)
        Dim oSerps = Await FEB2.WtbolSerps.All(exs)
        If exs.Count = 0 Then
            Xl_WtbolSerps1.Load(oSerps)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function RefrescaCtrSites() As Task
        Dim exs As New List(Of Exception)
        Dim oSerps = Await FEB2.WtbolSerps.All(exs)
        If exs.Count = 0 Then
            Dim oCtrs = Await FEB2.WtbolCtrs.All(exs)
            If exs.Count = 0 Then
                Xl_WtBolCtrsSites.Load(oSerps, oCtrs, Xl_WtBolCtrs.Modes.Sites)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function RefrescaCtrProducts() As Task
        Dim exs As New List(Of Exception)
        Dim oSerps = Await FEB2.WtbolSerps.All(exs)
        If exs.Count = 0 Then
            Dim oCtrs = Await FEB2.WtbolCtrs.All(exs)
            If exs.Count = 0 Then
                Xl_WtBolCtrsProducts.Load(oSerps, oCtrs, Xl_WtBolCtrs.Modes.Products)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function RefrescaBaskets() As Task
        Dim exs As New List(Of Exception)
        Dim oBaskets = Await FEB2.Wtbol.Baskets(Current.Session.User, exs)
        If exs.Count = 0 Then
            Xl_WtbolSitesBaskets1.Load(oBaskets)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_WtbolSerps1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_WtbolSerps1.RequestToRefresh
        Await RefrescaSerps()
    End Sub

    Private Sub CopiarEmailsTécnicsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopiarEmailsTécnicsToolStripMenuItem.Click
        Dim emails As String = FEB2.WtbolSites.EmailsForBcc(_WtbolSites)
        UIHelper.CopyToClipboard(emails, "emails copiats al portapapers")
    End Sub
End Class