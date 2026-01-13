Public Class Frm_FairGuest
    Private _FairGuest As DTOFairGuest
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOFairGuest)
        MyBase.New()
        Me.InitializeComponent()
        _FairGuest = value
        BLL.BLLFairGuest.Load(_FairGuest)
        If _FairGuest.Country Is Nothing Then
            _FairGuest.Country = BLL.Defaults.Country(Current.Session.Emp)
        End If
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _FairGuest
            TextBoxFirstName.Text = .FirstName
            TextBoxLastName.Text = .LastName
            TextBoxPosition.Text = .Position
            TextBoxNIF.Text = .NIF
            TextBoxRaoSocial.Text = .RaoSocial
            ComboBoxActivityCode.SelectedIndex = .ActivityCode
            TextBoxAddress.Text = .Address
            TextBoxZip.Text = .Zip
            TextBoxLocation.Text = .Location
            Xl_Country1.Country = .Country
            TextBoxPhone.Text = .Phone
            TextBoxCellPhone.Text = .CellPhone
            TextBoxFax.Text = .Fax
            TextBoxEmail.Text = .Email
            TextBoxWeb.Text = .web
            RadioButtonLongDistance.Checked = (.CodeDistance = DTOFairGuest.CodeDistances.LongerThan200Km)
            If .IsNew Then
                TextBoxFchCreated.Visible = False
            Else
                TextBoxFchCreated.Text = Format(.FchCreated, "dd/MM/yy HH:mm")
            End If
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            TextBoxFirstName.TextChanged, _
            TextBoxLastName.TextChanged, _
            TextBoxPosition.TextChanged, _
            TextBoxNIF.TextChanged, _
            TextBoxRaoSocial.TextChanged, _
            ComboBoxActivityCode.SelectedIndexChanged, _
            TextBoxAddress.TextChanged, _
            TextBoxZip.TextChanged, _
            TextBoxLocation.TextChanged, _
            Xl_Country1.AfterUpdate, _
            TextBoxPhone.TextChanged, _
            TextBoxCellPhone.TextChanged, _
            TextBoxFax.TextChanged, _
            TextBoxEmail.TextChanged, _
            TextBoxWeb.TextChanged, _
            RadioButtonLongDistance.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _FairGuest
            .FirstName = TextBoxFirstName.Text
            .LastName = TextBoxLastName.Text
            .Position = TextBoxPosition.Text
            .NIF = TextBoxNIF.Text
            .RaoSocial = TextBoxRaoSocial.Text
            .ActivityCode = ComboBoxActivityCode.SelectedIndex
            .Address = TextBoxAddress.Text
            .Zip = TextBoxZip.Text
            .Location = TextBoxLocation.Text
            .Country = Xl_Country1.Country
            .Phone = TextBoxPhone.Text
            .CellPhone = TextBoxCellPhone.Text
            .Fax = TextBoxFax.Text
            .Email = TextBoxEmail.Text
            .web = TextBoxWeb.Text
            .CodeDistance = IIf(RadioButtonLongDistance.Checked, DTOFairGuest.CodeDistances.LongerThan200Km, DTOFairGuest.CodeDistances.CloserThan200Km)
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLFairGuest.Update(_FairGuest, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_FairGuest))
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
            If BLL.BLLFairGuest.Delete(_FairGuest, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_FairGuest))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


