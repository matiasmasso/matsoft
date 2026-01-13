Public Class Frm_WebRoute

    Private _WebPage As DTOWebPage
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oWebPage As DTOWebPage)
        MyBase.New()
        Me.InitializeComponent()
        LoadIds()
        _WebPage = oWebPage
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With _WebPage
            ComboBoxId.SelectedValue = .Id
            TextBoxRuta.Text = .Route
            TextBoxUrl.Text = .Url
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        ComboBoxId.SelectedIndexChanged, _
         TextBoxRuta.TextChanged, _
          TextBoxUrl.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _WebPage
            .Id = ComboBoxId.SelectedValue
            .Route = TextBoxRuta.Text
            .Url = TextBoxUrl.Text
        End With

        Dim exs As New List(Of Exception)
        If Await FEBL.WebPage.Update(exs, _WebPage) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_WebPage))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEBL.WebPage.Delete(exs, _WebPage) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadIds()
        UIHelper.LoadComboFromEnum(ComboBoxId, GetType(DTOWebPage.Ids))
    End Sub
End Class