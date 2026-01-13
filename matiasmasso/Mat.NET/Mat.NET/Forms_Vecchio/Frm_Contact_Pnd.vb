

Public Class Frm_Contact_Pnd
    Private mPnd As pnd
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oPnd As Pnd)
        MyBase.New()
        Me.InitializeComponent()
        mPnd = oPnd
        LoadDefaults()
        refresca()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        With mPnd
            If .Id = 0 Then
                Me.Text = "NOVA PARTIDA PENDENT DE LIQUIDAR"
            Else
                Me.Text = "PARTIDA " & .Id & " PENDENT DE LIQUIDAR"
                ButtonDel.Enabled = True
            End If
            TextBoxContactNom.Text = .Contact.Clx
            Xl_AmtEur.Amt = New maxisrvr.Amt(.Amt.Eur)
            Xl_AmtCurDivisa.Amt = .Amt
            If .Amt.Cur.Id <> "EUR" Then
                CheckBoxDivisa.Checked = True
                Xl_AmtCurDivisa.Visible = True
            End If
            DateTimePicker1.Value = .Vto
            Xl_Cta1.Cta = .Cta
            ComboBoxAD.SelectedValue = .Cod
            ComboBoxStatus.SelectedValue = .Status
            ComboBoxCfp.SelectedValue = .Cfp
            TextBoxObs.Text = .Fpg
            TextBoxFraNum.Text = .FraNum
            TextBoxYea.Text = .Yef
            If .Cca Is Nothing Then
                ButtonZoom.Enabled = False
            Else
                TextBoxCca.Text = .Cca.Id
            End If

            DateTimePicker2.Text = .Fch

            If .Id > 0 Then
                If .Csb IsNot Nothing Then
                    Dim oCsa As Csa = .Csb.Csa
                    If oCsa.Exists Then
                        TextBoxCsaYea.Text = oCsa.yea
                        TextBoxCsaId.Text = oCsa.Id
                        LabelCsa.Text = "remesa " & oCsa.Id & " del " & oCsa.fch.ToShortDateString & " a " & oCsa.Banc.Abr & " per " & oCsa.Amt.CurFormat
                        PictureBoxBancLogo.Image = oCsa.Banc.Img48

                        TextBoxCsbDoc.Text = .Csb.Id

                        TextBoxCliNom.Text = .Csb.Client.Clx
                        TextBoxVto.Text = .Csb.Vto.ToShortDateString
                        TextBox1.Text = .Csb.Amt.CurFormat
                        TextBoxTxt.Text = .Csb.txt
                        PictureBoxCsbIban.Image = BLL.BLLIban.Img(.Csb.Iban.Digits)
                    End If
                End If
            End If
        End With
    End Sub

    Private Sub LoadDefaults()
        LoadComboFromEnum(ComboBoxAD, GetType(Pnd.Codis))
        LoadComboFromEnum(ComboBoxStatus, GetType(Pnd.StatusCod))
        LoadComboFromEnum(ComboBoxCfp, GetType(Contact.Cfps))
    End Sub

    Private Sub LoadPndFromForm()
        With mPnd
            If CheckBoxDivisa.Checked Then
                .Amt = New MaxiSrvr.Amt(Xl_AmtEur.Amt.Eur, Xl_AmtCurDivisa.Amt.Cur, Xl_AmtCurDivisa.Amt.Val)
            Else
                .Amt = Xl_AmtEur.Amt
            End If
            .Vto = DateTimePicker1.Value
            If Xl_Cta1.Cta Is Nothing Then Throw New Exception("falta especificar el compte")
            .Cta = Xl_Cta1.Cta
            .Cod = ComboBoxAD.SelectedValue
            .Status = ComboBoxStatus.SelectedValue
            .Cfp = ComboBoxCfp.SelectedValue
            .Fpg = TextBoxObs.Text
            .FraNum = TextBoxFraNum.Text
            .Yef = TextBoxYea.Text
            If IsNumeric(TextBoxCca.Text) Then
                .Cca = MaxiSrvr.Cca.FromNum(.Contact.Emp, TextBoxYea.Text, TextBoxCca.Text)
            End If
            .Fch = DateTimePicker2.Value
            Dim iCsaYea As Integer = IIf(IsNumeric(TextBoxCsaYea.Text), TextBoxCsaYea.Text, 0)
            Dim iCsaId As Integer = IIf(IsNumeric(TextBoxCsaId.Text), TextBoxCsaId.Text, 0)
            Dim iCsbDoc As Integer = IIf(IsNumeric(TextBoxCsbDoc.Text), TextBoxCsbDoc.Text, 0)
            .Csb = New Csb(MaxiSrvr.Csa.FromNum(mPnd.Contact.Emp, iCsaYea, iCsaId), iCsbDoc)
        End With
    End Sub

    Private Sub TextBoxEur_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = "." Then
            e.KeyChar = ","
        End If
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    ComboBoxStatus.SelectedIndexChanged, _
    TextBoxObs.TextChanged, _
    ComboBoxCfp.SelectedIndexChanged, _
    ComboBoxAD.SelectedIndexChanged, _
    DateTimePicker2.ValueChanged, _
    TextBoxFraNum.TextChanged, _
    TextBoxCca.TextChanged, _
    TextBoxYea.TextChanged, _
    DateTimePicker1.ValueChanged, _
    Xl_AmtCurDivisa.AfterUpdate, _
    Xl_AmtEur.AfterUpdate, _
    Xl_Cta1.AfterUpdate

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Try
            LoadPndFromForm()
            Dim exs As New List(Of Exception)
            If mPnd.Update(exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(mPnd))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al desar PND")
            End If
        Catch ex As Exception
            UIHelper.WarnError(ex)
        End Try
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem la partida " & mPnd.Id, MsgBoxStyle.OKCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then

            Dim exs as New List(Of exception)
            If mPnd.Delete( exs) Then
                MsgBox("partida eliminada", MsgBoxStyle.Information, "MAT.NET")
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                MsgBox("error al eliminar la partida pendent" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
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

    Private Sub TextBoxCsa_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxCsaId.TextChanged, TextBoxCsaYea.TextChanged
        If mAllowEvents Then
            If IsNumeric(TextBoxCsaYea.Text) Then
                If IsNumeric(TextBoxCsaId.Text) Then
                    Dim oCsa As Csa = MaxiSrvr.Csa.FromNum(mPnd.Contact.Emp, TextBoxCsaYea.Text, TextBoxCsaId.Text)
                    If oCsa.Exists Then
                        LabelCsa.Text = "remesa " & oCsa.Id & " del " & oCsa.fch.ToShortDateString & " a " & oCsa.Banc.Abr & " per " & oCsa.Amt.CurFormat
                        TextBoxCsbDoc.Text = ""
                        PictureBoxBancLogo.Image = oCsa.Banc.Img48
                        ButtonOk.Enabled = True
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub TextBoxCsbDoc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxCsbDoc.TextChanged
        If mAllowEvents Then
            If IsNumeric(TextBoxCsaYea.Text) Then
                If IsNumeric(TextBoxCsaId.Text) Then
                    Dim oCsa As Csa = MaxiSrvr.Csa.FromNum(mPnd.Contact.Emp, TextBoxCsaYea.Text, TextBoxCsaId.Text)
                    If oCsa.Exists Then
                        If IsNumeric(TextBoxCsbDoc.Text) Then
                            Dim oCsb As New Csb(oCsa, TextBoxCsbDoc.Text)
                            TextBoxCliNom.Text = oCsb.Client.Clx
                            TextBoxVto.Text = oCsb.Vto.ToShortDateString
                            TextBox1.Text = oCsb.Amt.CurFormat
                            TextBoxTxt.Text = oCsb.txt
                            PictureBoxCsbIban.Image = BLL.BLLIban.Img(oCsb.Iban.Digits)
                            ButtonOk.Enabled = True
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub CheckBoxDivisa_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxDivisa.CheckedChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
            Xl_AmtCurDivisa.Visible=CheckBoxDivisa.Checked
        End If
    End Sub

    Private Sub ButtonBrowseCca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBrowseCca.Click
        Dim DtFch As DateTime = DateTimePicker2.Value
        If DtFch = DateTimePicker2.MinDate Then DtFch = DateTimePicker1.Value
        Dim oExercici As New Exercici(BLL.BLLApp.Emp, DtFch.Year)
        Dim oFrm As New Frm_CliCtas(mPnd.Contact, Xl_Cta1.Cta, oExercici, bll.dEFAULTS.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onNewCcaSelected
        oFrm.Show()
    End Sub

    Private Sub onNewCcaSelected(sender As Object, e As MatEventArgs)
        Dim oCca As Cca = e.Argument
        If oCca IsNot Nothing Then
            mPnd.Cca = oCca
            TextBoxCca.Text = oCca.Id
            TextBoxYea.Text = oCca.fch.Year
            DateTimePicker2.Value = oCca.fch

            For Each Itm As Ccb In oCca.ccbs
                Select Case Itm.Cta.Cod
                    Case DTOPgcPlan.Ctas.Clients
                        Xl_AmtEur.Amt = Itm.Amt
                        ComboBoxAD.SelectedIndex = IIf(Itm.Dh = DTOCcb.DhEnum.Debe, Pnd.Codis.Deutor, Pnd.Codis.Creditor)
                        Xl_Cta1.Cta = Itm.Cta
                        ComboBoxStatus.SelectedIndex = Pnd.StatusCod.pendent
                        Exit For
                    Case DTOPgcPlan.Ctas.proveidorsEur, DTOPgcPlan.Ctas.proveidorsUsd, DTOPgcPlan.Ctas.proveidorsGbp
                        Xl_AmtEur.Amt = Itm.Amt
                        ComboBoxAD.SelectedIndex = IIf(Itm.Dh = DTOCcb.DhEnum.Debe, Pnd.Codis.Deutor, Pnd.Codis.Creditor)
                        Xl_Cta1.Cta = Itm.Cta
                        ComboBoxStatus.SelectedIndex = Pnd.StatusCod.pendent
                        Exit For
                End Select
            Next
            ButtonZoom.Enabled = True
        End If
    End Sub

    Private Sub Xl_AmtCurDivisa_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtCurDivisa.AfterUpdate

    End Sub

    Private Sub ButtonZoom_Click(sender As Object, e As EventArgs) Handles ButtonZoom.Click
        Dim oFrm As New Frm_Cca(mPnd.Cca)
        oFrm.Show()
    End Sub
End Class