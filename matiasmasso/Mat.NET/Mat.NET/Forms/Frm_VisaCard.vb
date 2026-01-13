Public Class Frm_VisaCard
    Private _VisaCard As DTOVisaCard
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOVisaCard)
        MyBase.New()
        Me.InitializeComponent()
        _VisaCard = value
        BLL.BLLVisaCard.Load(_VisaCard)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _VisaCard
            TextBoxNom.Text = .Nom
            TextBoxDigits.Text = .Digits
            TextBoxCaduca.Text = .Caduca
            TextBoxCCID.Text = .CCID
            Xl_LookupVisaOrg1.VisaOrg = .Emisor
            Xl_ContactTitular.Contact = .Titular
            Xl_LookupBanc1.Banc = .Banc

            If .Baja <> Nothing Then
                CheckBoxBaja.Checked = True
                DateTimePickerBaja.Visible = True
                DateTimePickerBaja.Value = .Baja
            End If

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged, _
         TextBoxDigits.TextChanged, _
          TextBoxCaduca.TextChanged, _
           TextBoxCCID.TextChanged, _
            Xl_LookupVisaOrg1.AfterUpdate, _
             Xl_LookupBanc1.AfterUpdate, _
              CheckBoxBaja.CheckedChanged, _
               DateTimePickerBaja.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _VisaCard
            .Nom = TextBoxNom.Text
            .Digits = TextBoxDigits.Text
            .Caduca = TextBoxCaduca.Text
            .CCID = TextBoxCCID.Text
            .Emisor = Xl_LookupVisaOrg1.VisaOrg
            .Titular = Xl_ContactTitular.Contact
            .Banc = Xl_LookupBanc1.Banc

            If CheckBoxBaja.Checked Then
                .Baja = DateTimePickerBaja.Value
            Else
                .Baja = Nothing
            End If
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLVisaCard.Update(_VisaCard, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_VisaCard))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLVisaCard.Delete(_VisaCard, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_VisaCard))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub CheckBoxBaja_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxBaja.CheckedChanged
        If _AllowEvents Then
            DateTimePickerBaja.Visible = CheckBoxBaja.Checked
        End If
    End Sub


End Class


