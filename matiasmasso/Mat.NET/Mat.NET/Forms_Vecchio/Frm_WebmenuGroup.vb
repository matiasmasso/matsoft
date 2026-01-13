

Public Class Frm_WebmenuGroup
    Private mWebmenuGroup As DTOWebMenuGroup
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

    Public Sub New(ByVal oWebMenuGroup As DTOWebMenuGroup)
        MyBase.New()
        Me.InitializeComponent()
        mWebMenuGroup = oWebMenuGroup
        TextBoxNom.Text = mWebmenuGroup.Esp
        'ButtonDel.Enabled = mWebMenuGroup.AllowDelete
    End Sub

    Private Sub TextBoxNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        mWebmenuGroup.Esp = TextBoxNom.Text
        Dim exs As New List(Of Exception)
        If BLL.BLLWebMenuGroup.update(mWebmenuGroup, exs) Then
            RaiseEvent AfterUpdate(mWebmenuGroup, EventArgs.Empty)
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
                    'loadMenus()
                    Done = True
                End If
        End Select
    End Sub

  
End Class