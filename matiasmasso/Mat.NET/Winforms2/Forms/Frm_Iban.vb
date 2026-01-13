Public Class Frm_Iban
    Private _Iban As DTOIban
    Private _CsbsLoaded As Boolean

    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Csbs
    End Enum

    Public Sub New(value As DTOIban)
        MyBase.New()
        Me.InitializeComponent()
        _Iban = value
    End Sub

    Private Async Sub Frm_Iban_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Iban.Load(_Iban, exs) Then
            UIHelper.LoadComboFromEnum(ComboBoxFormat, GetType(DTOIban.Formats))
            With _Iban
                If .IsNew Then TextBoxGuid.Enabled = True
                TextBoxGuid.Text = .Guid.ToString
                Xl_Contact21.Contact = .Titular
                Xl_IbanTextbox1.Value = _Iban.Digits
                Await Xl_Lookup_BankBranch1.Load(_Iban)
                ComboBoxFormat.SelectedIndex = .Format

                TextBoxPersonNom.Text = .PersonNom
                TextBoxPersonDni.Text = .PersonDni

                DateTimePickerFchFrom.MinDate = New Date(1985, 5, 28)
                LoadProposits(_Iban.Cod)
                If .FchFrom <= DateTimePickerFchFrom.MinDate Then
                    DateTimePickerFchFrom.Value = DateTimePickerFchFrom.MinDate
                Else
                    DateTimePickerFchFrom.Value = .FchFrom
                End If
                If .FchTo > DateTimePickerFchTo.MinDate Then
                    CheckBoxFchTo.Checked = True
                    DateTimePickerFchTo.Visible = True
                    DateTimePickerFchTo.Value = .FchTo
                End If

                If .UsrDownloaded Is Nothing Then
                    LabelDownloaded.Text = "pendent de descarregar"
                    LabelUploaded.Visible = False
                    LabelApproved.Visible = False
                    ButtonApprove.Visible = False
                Else
                    LabelDownloaded.Text = "descarregat a " & Format(.FchDownloaded, "dd/MM/yy HH:mm") & " per " & DTOUser.NicknameOrElse(.UsrDownloaded)
                    LabelUploaded.Visible = True

                    If .UsrUploaded Is Nothing Then
                        LabelUploaded.Text = "pendent de signar"
                        LabelApproved.Visible = False
                        ButtonApprove.Visible = False
                    Else
                        LabelUploaded.Text = "signat a " & Format(.FchUploaded, "dd/MM/yy HH:mm") & " per " & DTOUser.NicknameOrElse(.UsrUploaded)
                        LabelApproved.Visible = False

                        If .UsrApproved Is Nothing Then
                            LabelApproved.Text = "pendent de validar"
                            ButtonApprove.Visible = True
                        Else
                            LabelApproved.Text = "validat a " & Format(.FchApproved, "dd/MM/yy HH:mm") & " per " & DTOUser.NicknameOrElse(.UsrApproved)
                            LabelApproved.Visible = True
                            ButtonApprove.Visible = False
                        End If
                    End If
                End If


                If .UsrApproved Is Nothing Then
                    ButtonApprove.Visible = .DocFile IsNot Nothing
                Else
                    If .UsrDownloaded Is Nothing Then LabelDownloaded.Visible = False
                    If .UsrUploaded Is Nothing Then LabelApproved.Visible = False
                    LabelApproved.Text = "validat a " & Format(.FchApproved, "dd/MM/yy HH:mm") & " per " & DTOUser.NicknameOrElse(.UsrApproved)
                    LabelApproved.Visible = True
                    ButtonApprove.Visible = False
                End If

                Await Xl_DocFile1.Load(.DocFile)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvent = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_Contact21.AfterUpdate,
         Xl_Lookup_BankBranch1.AfterUpdate,
          ComboBoxFormat.SelectedIndexChanged,
           ComboBoxTipus.SelectedIndexChanged,
           DateTimePickerFchFrom.ValueChanged,
            DateTimePickerFchTo.ValueChanged,
              Xl_DocFile1.AfterUpdate

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If _Iban.IsNew Then
            Dim sGuidCandidate As String = TextBoxGuid.Text
            If GuidHelper.IsGuid(sGuidCandidate) Then
                _Iban.Guid = New Guid(sGuidCandidate)
                Save()
            Else
                UIHelper.WarnError("El numero de referencia ha de ser un número Guid" & vbCrLf & "(32 numeros hexadecimals + 4 guions en format 8-4-4-4-12)")
            End If
        Else
            Save()
        End If
    End Sub

    Private Async Sub Save()
        Dim exs As New List(Of Exception)
        With _Iban
            .Emp = Current.Session.Emp
            .Titular = Xl_Contact21.Contact
            .Digits = DTOIban.CleanCcc(Xl_IbanTextbox1.Value)
            .BankBranch = Xl_Lookup_BankBranch1.Branch
            .Format = ComboBoxFormat.SelectedIndex
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxFchTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Date.MinValue
            End If
            If Xl_DocFile1.IsDirty Then
                .FchUploaded = DTO.GlobalVariables.Now()
                .FchApproved = .FchUploaded
                .UsrUploaded = Current.Session.User
                .UsrApproved = .UsrUploaded
            End If
            .DocFile = Xl_DocFile1.Value
            .Cod = ComboBoxTipus.SelectedItem.key
        End With

        Dim sWarningMessage As String = ""
        If FEB.Iban.CheckIfOthersMatchingTimeSpan(exs, _Iban, sWarningMessage) Then
            MsgBox("error al desar la domiciliació:" & vbCrLf & sWarningMessage, MsgBoxStyle.Exclamation)
        Else
            If exs.Count = 0 Then
                UIHelper.ToggleProggressBar(Panel1, True)
                If Await FEB.Iban.Update(_Iban, exs) Then
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_Iban))
                    Me.Close()
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError(exs, "error al desar la domiciliació")
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.Iban.Delete(_Iban, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Iban))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub Xl_IbanTextbox1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_IbanTextbox1.ValueChanged
        If _AllowEvent Then
            ButtonOk.Enabled = True
            Await Xl_Lookup_BankBranch1.Load(Xl_IbanTextbox1.Value)
        End If
    End Sub

    Private Sub CheckBoxFchTo_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchTo.CheckedChanged
        If _AllowEvent Then
            DateTimePickerFchTo.Visible = CheckBoxFchTo.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonApprove_Click(sender As Object, e As EventArgs) Handles ButtonApprove.Click
        With _Iban
            .UsrApproved = Current.Session.User
            .FchApproved = DTO.GlobalVariables.Now()
        End With

        Save()
    End Sub

    Private Sub LoadProposits(oTipus As DTOIban.Cods)
        Dim oList As New List(Of KeyValuePair(Of Integer, String))
        For Each o As DTOIban.Cods In [Enum].GetValues(GetType(DTOIban.Cods))
            Dim item As KeyValuePair(Of Integer, String) = Nothing
            Select Case o
                Case DTOIban.Cods.Staff
                    item = New KeyValuePair(Of Integer, String)(o, "personal")
                    oList.Add(item)
                Case DTOIban.Cods._NotSet
                Case Else
                    item = New KeyValuePair(Of Integer, String)(o, o.ToString())
                    oList.Add(item)
            End Select
        Next
        With ComboBoxTipus
            .DataSource = oList
            .DisplayMember = "value"
            .SelectedItem = oList.Find(Function(x) x.Key = CInt(oTipus))
        End With

    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Csbs
                If Not _CsbsLoaded Then
                    Dim oCsbs = Await FEB.Csbs.All(exs, _Iban)
                    If exs.Count = 0 Then
                        Xl_IbanCsbs1.Load(oCsbs)
                        _CsbsLoaded = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub

    Private Sub TextBoxGuid_TextChanged(sender As Object, e As EventArgs) Handles TextBoxGuid.TextChanged
        Dim sCandidate As String = TextBoxGuid.Text
        If GuidHelper.IsGuid(sCandidate) Then
            TextBoxGuid.BackColor = Color.AliceBlue
        Else
            TextBoxGuid.BackColor = Color.LightYellow
        End If
    End Sub
End Class