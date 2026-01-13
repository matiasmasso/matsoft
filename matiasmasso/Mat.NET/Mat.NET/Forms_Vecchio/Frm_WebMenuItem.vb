

Public Class Frm_WebMenuItem
    Private mWebMenuItem as DTOWebMenuItem
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Private Enum Cols
        Guid
        Nom
        Url
    End Enum

    Private Enum Tabs
        General
        Menus
    End Enum

    Public Sub New(ByVal oWebMenuItem as DTOWebMenuItem)
        MyBase.New()
        Me.InitializeComponent()
        mWebMenuItem = oWebMenuItem
        With mWebMenuItem
            Xl_Lookup_WebmenuGroup1.Group = .Group
            TextBoxNom_ESP.Text = .Esp
            TextBoxNom_CAT.Text = .Cat
            TextBoxNom_ENG.Text = .Eng
            'TextBoxUrl.Text = .Url
            Xl_Lookup_WebRoute1.WebPage = .WebRoute
            mAllowEvents = True
        End With
    End Sub

    Private Sub Control_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom_ESP.TextChanged, _
         TextBoxNom_CAT.TextChanged, _
          TextBoxNom_ENG.TextChanged, _
           Xl_Lookup_WebRoute1.AfterUpdate, _
            Xl_Lookup_WebmenuGroup1.AfterUpdate

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mWebMenuItem
            .Group = Xl_Lookup_WebmenuGroup1.Group
            .Esp = TextBoxNom_ESP.Text
            .Cat = TextBoxNom_CAT.Text
            .Eng = TextBoxNom_ENG.Text
            .WebRoute = Xl_Lookup_WebRoute1.WebPage
        End With
        Dim exs As New List(Of Exception)
        If BLL.BLLWebMenuItem.Update(mWebMenuItem, exs) Then
            RaiseEvent AfterUpdate(mWebMenuItem, EventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Menus
                Static Done As Boolean
                If Not Done Then
                    Dim oRols As List(Of DTORol) = BLL.BLLWebMenuItem.GetRolsFromWebMenuItem(mWebMenuItem)
                    Xl_Rols1.Load(oRols)
                    Done = True
                End If
        End Select
    End Sub


    Private Sub Xl_Rols_Allowed1_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_Rols1.AfterUpdate
        'Dim oRols As DTORols = Xl_Rols1.Rols
        'mWebMenuItem.UpdateRols(oRols)
        mWebMenuItem.Rols = Xl_Rols1.Rols
        Dim exs As New List(Of Exception)
        If BLL.BLLWebMenuItem.Update(mWebMenuItem, exs) Then
            Dim oRols As List(Of DTORol) = BLL.BLLWebMenuItem.GetRolsFromWebMenuItem(mWebMenuItem)
            Xl_Rols1.Load(oRols)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class