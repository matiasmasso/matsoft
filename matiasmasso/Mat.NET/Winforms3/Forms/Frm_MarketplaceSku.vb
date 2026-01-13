Public Class Frm_MarketplaceSku
    Private _value As DTOMarketplaceSku
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOMarketplaceSku)
        InitializeComponent()
        _value = value
    End Sub

    Private Sub Frm_Item_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.MarketPlace.LoadSku(exs, _value) Then
            With _value
                TextBoxMarketplace.Text = .MarketPlace.Nom
                TextBoxId.Text = .CustomId
                TextBoxNom.Text = .Sku.Nom
                CheckBoxEnabled.Checked = .Enabled
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxId.TextChanged,
         CheckBoxEnabled.CheckedChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _value
            .CustomId = TextBoxId.Text
            .Enabled = CheckBoxEnabled.Checked
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.MarketPlace.UpdateSku(exs, _value) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


End Class


