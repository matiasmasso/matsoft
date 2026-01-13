Public Class Frm_Tel
    Private _Tel As DTOContactTel
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oTel As DTOContactTel)
        MyBase.New
        InitializeComponent()
        _Tel = oTel
        ComboBoxCod.Visible = True
        ComboBoxCod.SelectedItem = _Tel
    End Sub

    Private Sub Frm_Tel_Load(sender As Object, e As EventArgs) Handles Me.Load

        Select Case _Tel.Cod
            Case DTOContactTel.Cods.NotSet
                Me.Text = "Telefon"
                LabelCod.Visible = True
                PictureBox1.Image = Nothing
            Case DTOContactTel.Cods.tel
                Me.Text = "Telèfon"
                PictureBox1.Image = My.Resources.tel
            Case DTOContactTel.Cods.fax
                Me.Text = "Fax"
                PictureBox1.Image = My.Resources.fax
            Case DTOContactTel.Cods.movil
                Me.Text = "Mobil"
                PictureBox1.Image = My.Resources.CellPhone
        End Select

        With _Tel
            ComboBoxCod.SelectedIndex = .Cod
            Xl_LookupArea1.Load(.Country)
            TextBoxNum.Text = DTOBaseTel.Formatted(_Tel, ShowCountryPrefix:=False)
            TextBoxObs.Text = .Obs
            CheckBoxPrivat.Checked = .Privat
        End With
        refrescaprefixe()
        _AllowEvents = True
    End Sub

    Private Sub refrescaprefixe()
        With _Tel
            Dim sPrefix As String = ""
            If .Country IsNot Nothing Then
                If .Country.PrefixeTelefonic Is Nothing Then
                    LabelNum.Text = "Número"
                Else
                    LabelNum.Text = String.Format("Número (+{0})", .Country.PrefixeTelefonic)
                End If
            End If

        End With
    End Sub

    Private Sub Xl_LookupArea1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupArea1.AfterUpdate
        refrescaprefixe()
        ButtonOk.Enabled = True
    End Sub

    Private Function CurrentCod() As DTOContactTel.Cods
        Dim retval As DTOContactTel.Cods = ComboBoxCod.SelectedIndex
        Return retval
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If CurrentCod() <= DTOContactTel.Cods.NotSet Then
            MsgBox("Cal triar un dispositiu del desplegable", MsgBoxStyle.Exclamation)
        Else
            With _Tel
                .Cod = ComboBoxCod.SelectedIndex
                .Country = Xl_LookupArea1.Area
                .Value = TextHelper.LeaveJustNumbericDigits(TextBoxNum.Text)
                .Obs = TextBoxObs.Text
                .Privat = CheckBoxPrivat.Checked
            End With
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Tel))
            Me.Close()
        End If
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        TextBoxNum.TextChanged,
         TextBoxObs.TextChanged,
          CheckBoxPrivat.CheckedChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub ComboBoxCod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCod.SelectedIndexChanged
        Select Case CurrentCod()
            Case DTOContactTel.Cods.NotSet
                PictureBox1.Visible = False
            Case DTOContactTel.Cods.tel
                PictureBox1.Visible = True
                PictureBox1.Image = My.Resources.tel
            Case DTOContactTel.Cods.fax
                PictureBox1.Visible = True
                PictureBox1.Image = My.Resources.Fax2
            Case DTOContactTel.Cods.movil
                PictureBox1.Visible = True
                PictureBox1.Image = My.Resources.movil
        End Select
    End Sub

    Private Sub Xl_LookupArea1_onLookUpRequest(sender As Object, e As EventArgs) Handles Xl_LookupArea1.onLookUpRequest
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectCountry, Xl_LookupArea1.Area)
        AddHandler oFrm.onItemSelected, AddressOf onAreaSelected
        oFrm.Show()
    End Sub

    Private Sub onAreaSelected(sender As Object, e As MatEventArgs)
        Xl_LookupArea1.Load(e.Argument)
    End Sub
End Class