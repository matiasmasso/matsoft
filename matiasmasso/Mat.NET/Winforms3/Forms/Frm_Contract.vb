Public Class Frm_Contract
    Private _Contract As DTOContract
    Private _AllowEvents As Boolean = False

    Private Enum Tabs
        General
        Detall
    End Enum

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oContract As DTOContract)
        MyBase.New()
        Me.InitializeComponent()
        _Contract = oContract
    End Sub

    Private Async Sub Frm_Contract_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckBoxPrivat.Visible = Current.Session.User.Rol.IsAdmin

        Dim exs As New List(Of Exception)
        Dim oCodis = Await FEB.ContractCodis.All(exs)
        If exs.Count = 0 Then
            LoadCodis(oCodis)
            Refresca()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Refresca()
        With _Contract
            If .Guid.Equals(System.Guid.Empty) Then
                Me.Text = "NOU CONTRACTE"
            Else
                Me.Text = "CONTRACTE " & .Guid.ToString
                Dim exs As New List(Of Exception)
                If FEB.Contract.Load(_Contract, exs) Then
                    CheckBoxPrivat.Checked = .Privat
                    If .Codi IsNot Nothing Then
                        ComboBoxCodis.SelectedValue = .Codi.Guid
                    End If
                    If .Contact IsNot Nothing Then
                        Xl_Contact1.Contact = .Contact
                        'Xl_Contact_Logo1.Contact = .Contact
                    End If
                    TextBoxNom.Text = .Nom
                    TextBoxNum.Text = .Num
                    If .fchFrom <> Nothing Then
                        DateTimePickerFchFrom.Value = .fchFrom
                    End If
                    If .fchTo = Nothing Then
                        CheckBoxIndefinit.Checked = True
                        DateTimePickerFchTo.Enabled = False
                    Else
                        DateTimePickerFchTo.Enabled = True
                        DateTimePickerFchTo.Value = .fchTo
                    End If
                    Xl_DocFile1.Load(.DocFile)
                    ButtonDel.Enabled = Not .IsNew
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub

    Private Sub LoadCodis(oCodis)
        With ComboBoxCodis
            .DataSource = oCodis
            .DisplayMember = "Nom"
            .ValueMember = "Guid"
        End With
    End Sub

    Private Sub CheckBoxIndefinit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxIndefinit.CheckedChanged
        If _AllowEvents Then
            DateTimePickerFchTo.Enabled = Not CheckBoxIndefinit.Checked
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        If Xl_Contact1.Contact IsNot Nothing Then
            With _Contract
                .codi = ComboBoxCodis.SelectedItem
                .contact = Xl_Contact1.Contact
                .nom = TextBoxNom.Text
                .num = TextBoxNum.Text
                .fchFrom = DateTimePickerFchFrom.Value
                If CheckBoxIndefinit.Checked Then
                    .fchTo = Nothing
                Else
                    .fchTo = DateTimePickerFchTo.Value
                End If
                .privat = CheckBoxPrivat.Checked
                If Xl_DocFile1.IsDirty Then
                    .docFile = Xl_DocFile1.Value
                End If
            End With

            Dim exs As New List(Of Exception)
            If Await FEB.Contract.Update(_Contract, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Contract))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs, "error al desar el contracte")
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError("contacte incorrecte")
        End If
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_Contact1.AfterUpdate,
     ComboBoxCodis.SelectedIndexChanged,
      TextBoxNom.TextChanged,
      TextBoxNum.TextChanged,
       DateTimePickerFchFrom.ValueChanged,
        DateTimePickerFchTo.ValueChanged,
         CheckBoxIndefinit.CheckedChanged,
          CheckBoxPrivat.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest contracte?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Contract.Delete(_Contract, exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar el contracte")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case DirectCast(TabControl1.SelectedIndex, Tabs)
            Case Tabs.General
            Case Tabs.Detall
        End Select
    End Sub


    Private Sub Xl_DocFile1_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFile1.AfterFileDropped
        ButtonOk.Enabled = True
    End Sub


End Class