

Public Class Frm_Contact_Pnd
    Private _Pnd As DTOPnd
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oPnd As DTOPnd)
        MyBase.New()
        Me.InitializeComponent()
        _Pnd = oPnd
    End Sub

    Private Async Sub Frm_Contact_Pnd_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Pnd.Load(_Pnd, exs) Then
            LoadDefaults()
            Await refresca()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        With _Pnd
            If .IsNew Then
                Me.Text = "NOVA PARTIDA PENDENT DE LIQUIDAR"
            Else
                Me.Text = "PARTIDA PENDENT DE LIQUIDAR"
                ButtonDel.Enabled = True
            End If
            If .Contact IsNot Nothing Then
                TextBoxContactNom.Text = .Contact.FullNom
            End If
            If .Amt IsNot Nothing Then
                Xl_AmtEur.Value = DTOAmt.Factory(.Amt.Eur)
                Xl_AmtCurDivisa.Amt = .Amt
                If .Amt.Cur.Tag <> "EUR" Then
                    CheckBoxDivisa.Checked = True
                    Xl_AmtCurDivisa.Visible = True
                End If
            End If
            If .Vto > DateTimePickerVto.MinDate Then
                DateTimePickerVto.Value = .Vto
            End If
            Xl_Cta1.Cta = .Cta
            ComboBoxAD.SelectedValue = .Cod
            ComboBoxStatus.SelectedValue = .Status
            ComboBoxCfp.SelectedValue = .Cfp
            TextBoxObs.Text = .Fpg
            TextBoxFraNum.Text = .FraNum
            TextBoxYea.Text = .Yef
            If .Cca IsNot Nothing Then
                Xl_LookupCca1.Cca = .Cca
            End If
            If .CcaVto IsNot Nothing Then
                Xl_LookupCca2.Cca = .CcaVto
            End If
            DateTimePicker2.Text = .Fch

            If Not .IsNew Then
                If .Csb IsNot Nothing Then
                    Dim exs As New List(Of Exception)
                    If FEB2.Csb.Load(.Csb, exs) Then
                        Dim oCsa As DTOCsa = .Csb.Csa
                        If oCsa IsNot Nothing Then
                            LabelCsa.Text = "remesa " & oCsa.Id & " del " & oCsa.Fch.ToShortDateString & " a " & oCsa.Banc.Abr & " per " & DTOAmt.CurFormatted(DTOCsa.TotalNominal(oCsa))
                            PictureBoxBancLogo.Image = LegacyHelper.ImageHelper.Converter(oCsa.banc.Logo)

                            TextBoxCsbDoc.Text = .Csb.Id

                            TextBoxCliNom.Text = .Csb.Contact.FullNom
                            TextBoxVto.Text = .Csb.Vto.ToShortDateString
                            TextBox1.Text = DTOAmt.CurFormatted(.Csb.Amt)
                            TextBoxTxt.Text = .Csb.txt
                            PictureBoxCsbIban.Image = LegacyHelper.ImageHelper.Converter(Await FEB2.Iban.Img(exs, .Csb.Iban, Current.Session.Lang))
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        End With
    End Function

    Private Sub LoadDefaults()
        LoadComboFromEnum(ComboBoxAD, GetType(DTOPnd.Codis))
        LoadComboFromEnum(ComboBoxStatus, GetType(DTOPnd.StatusCod))
        LoadComboFromEnum(ComboBoxCfp, GetType(DTOPaymentTerms.CodsFormaDePago))
    End Sub

    Private Sub LoadPndFromForm()
        With _Pnd
            If CheckBoxDivisa.Checked Then
                .Amt = DTOAmt.Factory(Xl_AmtEur.Value.Eur, Xl_AmtCurDivisa.Amt.Cur.Tag, Xl_AmtCurDivisa.Amt.Val)
            Else
                .Amt = Xl_AmtEur.Value
            End If
            .Vto = DateTimePickerVto.Value
            If Xl_Cta1.Cta Is Nothing Then Throw New Exception("falta especificar el compte")
            .Cta = Xl_Cta1.Cta
            .Cod = ComboBoxAD.SelectedValue
            .Status = ComboBoxStatus.SelectedValue
            .Cfp = ComboBoxCfp.SelectedValue
            .Fpg = TextBoxObs.Text
            .FraNum = TextBoxFraNum.Text
            .Yef = TextBoxYea.Text
            .Cca = Xl_LookupCca1.Cca
            .CcaVto = Xl_LookupCca2.Cca
            .Fch = DateTimePicker2.Value
        End With
    End Sub

    Private Sub TextBoxEur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = "." Then
            e.KeyChar = ","
        End If
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    ComboBoxStatus.SelectedIndexChanged,
    TextBoxObs.TextChanged,
    ComboBoxCfp.SelectedIndexChanged,
    ComboBoxAD.SelectedIndexChanged,
    DateTimePicker2.ValueChanged,
    TextBoxFraNum.TextChanged,
    TextBoxYea.TextChanged,
    DateTimePickerVto.ValueChanged,
    Xl_AmtCurDivisa.AfterUpdate,
    Xl_AmtEur.AfterUpdate,
    Xl_Cta1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Try
            LoadPndFromForm()
            Dim exs As New List(Of Exception)
            If Await FEB2.Pnd.Update(_Pnd, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Pnd))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al desar PND")
            End If
        Catch ex As Exception
            UIHelper.WarnError(ex)
        End Try
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem la partida?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then

            Dim exs As New List(Of Exception)
            If Await FEB2.Pnd.Delete(_Pnd, exs) Then
                MsgBox("partida eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                MsgBox("error al eliminar la partida pendent" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("operació cancelada per l'usuari", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Public Sub LoadComboFromEnum(ByVal oCombobox As ComboBox, ByVal oEnumType As Type)
        Dim oTb As New System.Data.DataTable
        oTb.Columns.Add("COD", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("NOM", System.Type.GetType("System.String"))
        Dim oRow As System.Data.DataRow

        Dim v As Integer
        For Each v In [Enum].GetValues(oEnumType)
            oRow = oTb.NewRow
            oRow(0) = v
            oTb.Rows.Add(oRow)
        Next

        Dim i As Integer = 0
        Dim s As String '= [Enum].Parse(GetType(test), test.uno)
        'For Each s In [Enum].GetNames(GetType(test))
        For Each s In [Enum].GetNames(oEnumType)
            oTb.Rows(i)(1) = s
            i = i + 1
        Next

        With oCombobox
            .DataSource = oTb
            .ValueMember = "COD"
            .DisplayMember = "NOM"
        End With
    End Sub


    Private Sub CheckBoxDivisa_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxDivisa.CheckedChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
            Xl_AmtCurDivisa.Visible = CheckBoxDivisa.Checked
        End If
    End Sub

    Private Sub onNewCcaSelected(sender As Object, e As MatEventArgs)
        Dim oCcb As DTOCcb = e.Argument
        If oCcb IsNot Nothing Then
            Dim oCca = oCcb.Cca
            _Pnd.Cca = oCca
            TextBoxYea.Text = oCca.Fch.Year
            DateTimePicker2.Value = oCca.Fch

            For Each Itm As DTOCcb In oCca.Items
                Select Case Itm.Cta.Codi
                    Case DTOPgcPlan.Ctas.Clients
                        Xl_AmtEur.Value = Itm.Amt
                        ComboBoxAD.SelectedIndex = IIf(Itm.Dh = DTOCcb.DhEnum.Debe, DTOPnd.Codis.Deutor, DTOPnd.Codis.Creditor)
                        Xl_Cta1.Cta = New DTOPgcCta(Itm.Cta.Guid)
                        ComboBoxStatus.SelectedIndex = DTOPnd.StatusCod.pendent
                        Exit For
                    Case DTOPgcPlan.Ctas.ProveidorsEur, DTOPgcPlan.Ctas.ProveidorsUsd, DTOPgcPlan.Ctas.ProveidorsGbp
                        Xl_AmtEur.Value = Itm.Amt
                        ComboBoxAD.SelectedIndex = IIf(Itm.Dh = DTOCcb.DhEnum.Debe, DTOPnd.Codis.Deutor, DTOPnd.Codis.Creditor)
                        Xl_Cta1.Cta = New DTOPgcCta(Itm.Cta.Guid)
                        ComboBoxStatus.SelectedIndex = DTOPnd.StatusCod.pendent
                        Exit For
                End Select
            Next
            Xl_LookupCca1.Cca = oCca
        End If
    End Sub

    Private Sub onNewCcaVtoSelected(sender As Object, e As MatEventArgs)
        Dim oCcb As DTOCcb = e.Argument
        If oCcb IsNot Nothing Then
            Dim oCca = oCcb.Cca
            _Pnd.CcaVto = oCca
            Xl_LookupCca2.Cca = oCca
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_LookupCca1_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupCca1.RequestToLookup
        Dim iYear As Integer = DateTimePicker2.Value.Year
        Dim oFrm As New Frm_Extracte(_Pnd.Contact, _Pnd.Cta, DTOExercici.FromYear(Current.Session.Emp, iYear), DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onNewCcaSelected
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupCca2_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupCca2.RequestToLookup
        Dim iYear As Integer = DateTimePickerVto.Value.Year
        Dim oFrm As New Frm_Extracte(_Pnd.Contact, _Pnd.Cta, DTOExercici.FromYear(Current.Session.Emp, iYear), DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onNewCcaVtoSelected
        oFrm.Show()
    End Sub


End Class