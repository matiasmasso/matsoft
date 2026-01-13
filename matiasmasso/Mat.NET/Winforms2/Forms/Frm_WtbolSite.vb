Imports System.ComponentModel

Public Class Frm_WtbolSite

    Private _WtbolSite As DTOWtbolSite
    Private _Tab As Tabs
    Private _TabIsLoaded(10) As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        LandingPages
        Stocks
        Clicks
        Baskets
    End Enum

    Public Sub New(value As DTOWtbolSite, Optional Tab As Tabs = Tabs.General)
        MyBase.New()
        Me.InitializeComponent()
        _WtbolSite = value
        _Tab = Tab
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If Not _WtbolSite.IsNew Then
            UIHelper.ToggleProggressBar(Panel1, True)
            _WtbolSite = Await FEB.WtbolSite.Find(exs, _WtbolSite.Guid)
            UIHelper.ToggleProggressBar(Panel1, False)
        End If

        If exs.Count = 0 Then
            With _WtbolSite
                Me.Text = "Fitxa de comerç online: " & .Nom
                TextBoxWeb.Text = .Web
                TextBoxNom.Text = .Nom
                Xl_Contact21.Contact = .Customer
                TextBoxMerchantId.Text = .MerchantId
                TextBoxContactNom.Text = .ContactNom
                TextBoxContactEmail.Text = .ContactEmail
                TextBoxContactTel.Text = .ContactTel
                If .LandingPages IsNot Nothing Then
                    If .LandingPages.Count > 0 Then
                        Dim DtFch As Date = .LandingPages.Max(Function(x) x.FchCreated)
                        TextBoxLandingPages.Text = .LandingPages.Count & " pags. (" & Format(DtFch, "dd/MM/yy HH:mm") & ")"
                    End If
                    Xl_WtbolLandingPages1.Load(.LandingPages)
                End If
                If .FchLastStocks <> Nothing Then
                    TextBoxStocks.Text = Format(.FchLastStocks, "dd/MM/yy HH:mm")
                End If
                CheckBoxActive.Checked = .Active
                TextBoxObs.Text = .Obs
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With

            _AllowEvents = True
            TabControl1.SelectedIndex = _Tab
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxWeb.TextChanged,
        TextBoxNom.TextChanged,
         Xl_Contact21.AfterUpdate,
          TextBoxMerchantId.TextChanged,
           TextBoxContactNom.TextChanged,
            TextBoxContactTel.TextChanged,
              CheckBoxActive.CheckedChanged,
               TextBoxObs.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _WtbolSite
            .Web = TextBoxWeb.Text
            .Nom = TextBoxNom.Text
            .Customer = Xl_Contact21.Customer
            .MerchantId = TextBoxMerchantId.Text
            .ContactNom = TextBoxContactNom.Text
            .ContactEmail = TextBoxContactEmail.Text
            .ContactTel = TextBoxContactTel.Text
            .Active = CheckBoxActive.Checked
            .Obs = TextBoxObs.Text
            .UsrLog = DTOUsrLog.Factory(Current.Session.User)
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.WtbolSite.Update(_WtbolSite, exs) Then
            ButtonOk.Enabled = False
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_WtbolSite))
            UIHelper.ToggleProggressBar(Panel1, False)
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.WtbolSite.Delete(_WtbolSite, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_WtbolSite))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        If Not _TabIsLoaded(TabControl1.SelectedIndex) Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.General
                    Xl_TextboxSearch1.Visible = False
                Case Tabs.LandingPages
                    Xl_TextboxSearch1.Visible = True
                Case Tabs.Stocks
                    UIHelper.ToggleProggressBar(Panel1, True)
                    Dim oStocks = Await FEB.WtbolStocks.All(exs, _WtbolSite)
                    If exs.Count = 0 Then
                        Xl_WtbolStocks1.Load(oStocks)
                        Xl_TextboxSearch1.Visible = True
                        UIHelper.ToggleProggressBar(Panel1, False)
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs)
                    End If
                Case Tabs.Clicks
                    UIHelper.ToggleProggressBar(Panel1, True)
                    Dim oCtrs = Await FEB.WtbolCtrs.All(exs, _WtbolSite)
                    If exs.Count = 0 Then
                        Xl_WtbolSiteClicks1.Load(oCtrs)
                        UIHelper.ToggleProggressBar(Panel1, False)
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs)
                    End If
                Case Tabs.Baskets
                    UIHelper.ToggleProggressBar(Panel1, True)
                    Dim oBaskets = Await FEB.WtbolBaskets.All(exs, _WtbolSite)
                    If exs.Count = 0 Then
                        Xl_WtbolBaskets1.Load(oBaskets)
                        UIHelper.ToggleProggressBar(Panel1, False)
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs)
                    End If
            End Select
            _TabIsLoaded(TabControl1.SelectedIndex) = True
        End If
    End Sub

    Private Sub Xl_WtbolLandingPages1_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_WtbolLandingPages1.RequestToRemove
        Dim itemsToRemove = e.Argument
        For Each item In itemsToRemove
            _WtbolSite.LandingPages.Remove(item)
        Next
        Xl_WtbolLandingPages1.Load(_WtbolSite.LandingPages)
        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_WtbolLandingPages1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_WtbolLandingPages1.RequestToAddNew
        Dim item = DTOWtbolLandingPage.Factory(_WtbolSite, Current.Session.User)
        Dim oFrm As New Frm_WtbolLandingPage(item)
        AddHandler oFrm.AfterUpdate, AddressOf onLandingPagesAdded
        oFrm.Show()
    End Sub

    Private Sub onLandingPagesAdded(sender As Object, e As MatEventArgs)
        _WtbolSite.LandingPages.Add(e.Argument)
        Xl_WtbolLandingPages1.Load(_WtbolSite.LandingPages)
        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Select Case TabControl1.SelectedIndex
            Case Tabs.LandingPages
                Xl_WtbolLandingPages1.Filter = e.Argument
        End Select
    End Sub

    Private Async Sub Xl_WtbolSiteClicks1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_WtbolSiteClicks1.RequestToRefresh
        Dim exs As New List(Of Exception)
        Dim oCtrs = Await FEB.WtbolCtrs.All(exs, _WtbolSite)
        If exs.Count = 0 Then
            Xl_WtbolSiteClicks1.Load(oCtrs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_WtbolLandingPages1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_WtbolLandingPages1.RequestToRefresh
        Xl_WtbolLandingPages1.Load(_WtbolSite.LandingPages)
        ButtonOk.Enabled = True
    End Sub

    Private Sub Frm_WtbolSite_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If ButtonOk.Enabled = True Then
            Dim rc = MsgBox("desem els canvis abans de sortir?", MsgBoxStyle.YesNoCancel)
            Select Case rc
                Case MsgBoxResult.Yes
                    ButtonOk_Click(sender, New EventArgs)
                    e.Cancel = False
                Case MsgBoxResult.No
                    e.Cancel = False
                Case MsgBoxResult.Cancel
                    e.Cancel = True
            End Select
        End If
    End Sub

    Private Sub CopiarGuidToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopiarGuidToolStripMenuItem.Click
        UIHelper.CopyToClipboard(_WtbolSite.Guid.ToString)
    End Sub

    Private Sub FeedStocksParaHatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FeedStocksParaHatchToolStripMenuItem.Click
        Dim url = FEB.WtbolSite.HatchFeedUrl(_WtbolSite, True)
        UIHelper.ShowHtml(url)
    End Sub

End Class


