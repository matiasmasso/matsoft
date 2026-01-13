

Public Class Frm_Impagat
    Private _Impagat As DTOImpagat
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(value As DTOImpagat)
        MyBase.New
        InitializeComponent()
        _Impagat = value


    End Sub

    Private Async Sub Frm_Impagat_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Impagat IsNot Nothing Then
            If FEB2.Impagat.Load(_Impagat, exs) Then
                Await refresca()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        LoadStatus()
        With _Impagat
            TextBoxRefBanc.Text = .RefBanc
            Xl_AmtNominal.Amt = .Nominal
            Xl_AmtDespeses.Amt = .Gastos
            Dim oTmp As DTOAmt = .Nominal.Clone
            oTmp.Add(.Gastos)
            Xl_AmtDeute.Amt = oTmp
            Xl_AmtPagatACompte.Amt = .PagatACompte
            Dim oPendent As DTOAmt = oTmp.Clone
            oPendent.Substract(.PagatACompte)
            Xl_AmtPendent.Amt = oPendent

            If .FchAFP > DateTime.MinValue Then
                CheckBoxAFP.Checked = True
                DateTimePickerAFP.Value = .FchAFP
            Else
                DateTimePickerAFP.Visible = False
            End If

            If .FchSdo > DateTime.MinValue Then
                CheckBoxSaldat.Checked = True
                DateTimePickerSaldo.Value = .FchSdo
            Else
                DateTimePickerSaldo.Visible = False
            End If

            'If .Insolvencia.Exists Then
            ' CheckBoxInsolvencia.Checked = True
            ' LabelRefInsolvencia.Visible = True
            ' TextBoxInsolvencia.Visible = True
            ' TextBoxInsolvencia.Text = .Insolvencia.Id
            ' ButtonShowInsolvencia.Visible = True
            ' End If

            If .CcaIncobrable IsNot Nothing Then
                CheckBoxIncobrable.Checked = True
                Xl_LookupCcaIncobrable.Visible = True
                Xl_LookupCcaIncobrable.Cca = .CcaIncobrable
            End If

            If .AsnefAlta = Date.MinValue Then
            Else
                DateTimePickerAsnefAlta.Visible = True
                DateTimePickerAsnefAlta.Value = .AsnefAlta
                CheckBoxAsnefAlta.Checked = True
                CheckBoxAsnefBaixa.Enabled = True
                If .AsnefBaixa = Date.MinValue Then
                Else
                    CheckBoxAsnefBaixa.Checked = True
                    DateTimePickerAsnefBaixa.Visible = True
                    DateTimePickerAsnefBaixa.Value = .AsnefBaixa
                End If
            End If

            TextBoxObs.Text = .Obs

            ButtonDel.Enabled = Not .IsNew

            'Tab efecte
            With .Csb

                With .Csa
                    FEB2.Banc.Load(.Banc, exs)
                    PictureBoxBancLogo.Image = LegacyHelper.ImageHelper.Converter(.banc.Logo)
                    TextBoxCsa.Text = "remesa presentada el " & .Fch.ToShortDateString & " a " & .Banc.Abr
                End With
                TextBoxCliNom.Text = .Contact.FullNom
                TextBoxVto.Text = .Vto.ToShortDateString
                TextBoxEur.Text = DTOAmt.CurFormatted(.Amt)
                TextBoxTxt.Text = .Txt
                Await Xl_Iban1.Load(.Iban)
            End With
        End With
        _AllowEvents = True
    End Function

    Private Sub LoadStatus()

    End Sub

    Private Sub CheckBoxAFP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAFP.CheckedChanged
        DateTimePickerAFP.Visible = CheckBoxAFP.Checked
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        CheckBoxAFP.CheckedChanged,
        CheckBoxSaldat.CheckedChanged,
        DateTimePickerAFP.ValueChanged,
        DateTimePickerSaldo.ValueChanged,
        CheckBoxSaldat.CheckedChanged,
        TextBoxRefBanc.TextChanged,
        TextBoxObs.TextChanged

        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_Amt_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
 Xl_AmtDespeses.AfterUpdate,
  Xl_AmtPagatACompte.AfterUpdate

        Dim oTmp As DTOAmt = Xl_AmtNominal.Amt
        oTmp.Add(Xl_AmtDespeses.Amt)
        Xl_AmtDeute.Amt = oTmp
        oTmp.Substract(Xl_AmtPagatACompte.Amt)
        Xl_AmtPendent.Amt = oTmp

        ButtonOk.Enabled = True
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Impagat
            .RefBanc = TextBoxRefBanc.Text
            .Gastos = Xl_AmtDespeses.Amt
            .PagatACompte = Xl_AmtPagatACompte.Amt
            If CheckBoxAFP.Checked Then
                .FchAFP = DateTimePickerAFP.Value
            Else
                .FchAFP = Date.MinValue
            End If
            If CheckBoxSaldat.Checked Then
                .FchSdo = DateTimePickerSaldo.Value
                .Status = DTOImpagat.StatusCodes.Saldat
            Else
                .FchSdo = Date.MinValue
                If .Status = DTOImpagat.StatusCodes.Saldat Then
                    .Status = DTOImpagat.StatusCodes.EnNegociacio
                End If
            End If

            If CheckBoxAsnefAlta.Checked Then
                .AsnefAlta = DateTimePickerAsnefAlta.Value
                If CheckBoxAsnefBaixa.Checked Then
                    .AsnefBaixa = DateTimePickerAsnefBaixa.Value
                Else
                    .AsnefBaixa = Date.MinValue
                End If
            Else
                .AsnefAlta = Date.MinValue
                .AsnefBaixa = Date.MinValue
            End If

            If CheckBoxIncobrable.Checked Then
                .CcaIncobrable = Xl_LookupCcaIncobrable.Cca
            Else
                .CcaIncobrable = Nothing
            End If

            .Obs = TextBoxObs.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Impagat.Update(_Impagat, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Impagat))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar l'impagat")
        End If

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB2.Impagat.Delete(_Impagat, exs) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al eliminar l'impagat")
        End If
        Me.Close()
    End Sub

    Private Sub CheckBoxSaldat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxSaldat.CheckedChanged
        DateTimePickerSaldo.Visible = CheckBoxSaldat.Checked
    End Sub


    Private Sub CheckBoxAsnefAlta_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAsnefAlta.CheckedChanged
        If _AllowEvents Then
            DateTimePickerAsnefAlta.Visible = CheckBoxAsnefAlta.Checked
            CheckBoxAsnefBaixa.Enabled = CheckBoxAsnefAlta.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxAsnefBaixa_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAsnefBaixa.CheckedChanged
        If _AllowEvents Then
            DateTimePickerAsnefBaixa.Visible = CheckBoxAsnefBaixa.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxIncobrable_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIncobrable.CheckedChanged
        If _AllowEvents Then
            Xl_LookupCcaIncobrable.Visible = CheckBoxIncobrable.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_LookupCcaIncobrable_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupCcaIncobrable.RequestToLookup
        Dim oLliurat As DTOContact = _Impagat.Csb.Contact
        Dim oFrm As New Frm_Extracte(oLliurat,,, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onCcaIncobrableSelected
        oFrm.Show()

    End Sub

    Private Sub onCcaIncobrableSelected(sender As Object, e As MatEventArgs)
        Dim oCcb As DTOCcb = e.Argument
        Xl_LookupCcaIncobrable.Cca = oCcb.Cca
        ButtonOk.Enabled = True
    End Sub
End Class