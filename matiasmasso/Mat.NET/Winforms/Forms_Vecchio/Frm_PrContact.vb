

Public Class Frm_PrContact
    Private mPrContact As PrContact
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oPrContact As prcontact)
        MyBase.new()
        Me.InitializeComponent()
        mPrContact = oPrContact
        'Me.Text = mObject.ToString
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mPrContact
            TextBoxNom.Text = .Nom
            TextBoxEmail.Text = .email
            Xl_Contact1.Contact = .Contact
            LoadCombo(ComboBoxEspecialitat, GetType(PrContact.Especialitats), "(seleccionar una especialitat)", .Especialitat)
            LoadCombo(ComboBoxCodiEntitat, GetType(PrContact.CodisEntitat), "(seleccionar un codi d'entitat)", .CodiEntitat)
            LoadCombo(ComboBoxCarrec, GetType(PrContact.Carrecs), "(seleccionar un càrrec)", .Carrec)
            LoadCombo(ComboBoxStatus, GetType(PrContact.Statuses), "(seleccionar un status)", .Status)
            TextBoxObs.Text = .Observacions
            If .Exists Then
                ButtonDel.Enabled = .AllowDelete
            End If
        End With
    End Sub

    Private Sub LoadCombo(ByVal oCombobox As ComboBox, ByVal oEnumType As Type, ByVal sNullText As String, ByVal iDefault As Integer)
        LoadComboFromEnum(oCombobox, oEnumType, sNullText, iDefault)
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged, _
         TextBoxEmail.TextChanged, _
          Xl_Contact1.AfterUpdate, _
           ComboBoxEspecialitat.SelectedIndexChanged, _
            ComboBoxCodiEntitat.SelectedIndexChanged, _
             ComboBoxCarrec.SelectedIndexChanged, _
              ComboBoxStatus.SelectedIndexChanged, _
               TextBoxObs.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mPrContact
            .Nom = TextBoxNom.Text
            .email = TextBoxEmail.Text
            .Contact = Xl_Contact1.Contact
            .Especialitat = CType(ComboBoxEspecialitat.SelectedValue, PrContact.Especialitats)
            .CodiEntitat = CType(ComboBoxCodiEntitat.SelectedValue, PrContact.CodisEntitat)
            .Carrec = CType(ComboBoxCarrec.SelectedValue, PrContact.Carrecs)
            .Status = CType(ComboBoxStatus.SelectedValue, PrContact.Statuses)
            .Observacions = TextBoxObs.Text
            .Update()
            RaiseEvent AfterUpdate(mPrContact, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mPrContact.AllowDelete Then
            mPrContact.Delete()
            RaiseEvent AfterUpdate(Nothing, System.EventArgs.Empty)
            Me.Close()
        End If
    End Sub
End Class