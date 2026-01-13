Public Class Frm_MarketPlace
    Private _MarketPlace As DTOMarketPlace
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Tickets
    End Enum

    Public Sub New(value As DTOMarketPlace)
        MyBase.New()
        Me.InitializeComponent()
        _MarketPlace = value
    End Sub

    Private Sub Frm_MarketPlace_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.MarketPlace.Load(exs, _MarketPlace) Then
            With _MarketPlace
                Xl_Contact21.LoadLite(.Contact())
                If .IsNew Then Xl_Contact21.Enabled = True
                TextBoxNom.Text = .Nom
                Xl_Percent1.Value = .Commission
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With

            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
             TextBoxNom.TextAlignChanged,
              Xl_Percent1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If _MarketPlace.IsNew Then
            _MarketPlace = New DTOMarketPlace(Xl_Contact21.Contact.Guid)
        End If

        With _MarketPlace
            .Nom = TextBoxNom.Text
            .Commission = Xl_Percent1.Value
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB2.MarketPlace.Update(exs, _MarketPlace) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_MarketPlace))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
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
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB2.MarketPlace.Delete(exs, _MarketPlace) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_MarketPlace))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Tickets
                Dim values = Await FEB2.ConsumerTickets.All(exs, _MarketPlace, Today.Year)
                If exs.Count = 0 Then
                    Xl_ConsumerTickets1.Load(values)
                End If
        End Select
    End Sub
End Class


