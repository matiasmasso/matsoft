Public Class Frm_PromofarmaFeedItem
    Private _value As DTO.Integracions.Promofarma.Feed.Item
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTO.Integracions.Promofarma.Feed.Item)
        InitializeComponent()
        _value = value
    End Sub

    Private Sub Frm_Item_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.PromofarmaFeedItem.Load(exs, _value) Then
            With _value
                TextBoxId.Text = .IdPromofarma
                TextBoxNom.Text = .NombreProducto
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
            .IdPromofarma = TextBoxId.Text
            .Enabled = CheckBoxEnabled.Checked
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.PromofarmaFeedItem.Update(exs, _value) Then
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


