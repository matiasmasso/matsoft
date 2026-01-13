

Public Class Frm_Impagat
    Private mImpagat As Impagat
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property Impagat() As Impagat
        Set(ByVal value As Impagat)
            If value IsNot Nothing Then
                mImpagat = value
                refresca()
            End If
        End Set
    End Property

    Private Sub refresca()
        LoadStatus()
        With mImpagat
            TextBoxRefBanc.Text = .RefBanc
            Xl_AmtNominal.Amt = .Nominal
            Xl_AmtDespeses.Amt = .Gastos
            Dim oTmp As maxisrvr.Amt = .Nominal.Clone
            oTmp.Add(.Gastos)
            Xl_AmtDeute.Amt = oTmp
            Xl_AmtPagatACompte.Amt = .PagatACompte
            Dim oPendent As maxisrvr.Amt = oTmp.Clone
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

            If .Insolvencia.Exists Then
                CheckBoxInsolvencia.Checked = True
                LabelRefInsolvencia.Visible = True
                TextBoxInsolvencia.Visible = True
                TextBoxInsolvencia.Text = .Insolvencia.Id
                ButtonShowInsolvencia.Visible = True
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

            ButtonDel.Enabled = .Exists

            'Tab efecte
            With .Csb
                With .Csa
                    PictureBoxBancLogo.Image = .Banc.Img48
                    TextBoxCsa.Text = "remesa presentada el " & .fch.ToShortDateString & " a " & .Banc.Abr
                End With
                TextBoxCliNom.Text = .Client.Clx
                TextBoxVto.Text = .Vto.ToShortDateString
                TextBoxEur.Text = .Amt.CurFormat
                TextBoxTxt.Text = .txt
                Xl_Iban1.Load(.Iban)
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Sub LoadStatus()

    End Sub

    Private Sub CheckBoxAFP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAFP.CheckedChanged
        DateTimePickerAFP.Visible = CheckBoxAFP.Checked
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        CheckBoxAFP.CheckedChanged, _
        CheckBoxSaldat.CheckedChanged, _
        CheckBoxInsolvencia.CheckedChanged, _
        DateTimePickerAFP.ValueChanged, _
        DateTimePickerSaldo.ValueChanged, _
        CheckBoxSaldat.CheckedChanged, _
        CheckBoxInsolvencia.CheckedChanged, _
        TextBoxRefBanc.TextChanged, _
        TextBoxObs.TextChanged

        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_Amt_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
 Xl_AmtDespeses.AfterUpdate, _
  Xl_AmtPagatACompte.AfterUpdate

        Dim oTmp As maxisrvr.Amt = Xl_AmtNominal.Amt
        oTmp.Add(Xl_AmtDespeses.Amt)
        Xl_AmtDeute.Amt = oTmp
        oTmp.Substract(Xl_AmtPagatACompte.Amt)
        Xl_AmtPendent.Amt = oTmp

        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mImpagat
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
                .Status = MaxiSrvr.Impagat.StatusCodes.Saldat
            Else
                .FchSdo = Date.MinValue
                If .Status = MaxiSrvr.Impagat.StatusCodes.Saldat Then
                    .Status = MaxiSrvr.Impagat.StatusCodes.EnNegociacio
                End If
            End If

            If CheckBoxInsolvencia.Checked Then
                If IsNumeric(TextBoxInsolvencia.Text) Then
                    Dim oInsolvencia As New Insolvencia(mImpagat.Csb.Csa.emp, TextBoxInsolvencia.Text)
                    If oInsolvencia.Exists Then
                        If oInsolvencia.Contact.Id = mImpagat.Csb.Client.Id Then
                            .Insolvencia = oInsolvencia
                        Else
                            MsgBox("aquest numero de insolvencia no correspon al mateix deutor que l'impagat", MsgBoxStyle.Exclamation, "MAT.NET")
                            Exit Sub
                        End If
                    Else
                        MsgBox("aquest numero de insolvencia no existeix", MsgBoxStyle.Exclamation, "MAT.NET")
                        Exit Sub
                    End If
                Else
                    MsgBox("nomes s'admeten referencies numeriques de insolvencia", MsgBoxStyle.Exclamation, "MAT.NET")
                    Exit Sub
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

            .Obs = TextBoxObs.Text
        End With

        Dim exs as New List(Of exception)
        If mImpagat.Update( exs) Then
            RaiseEvent AfterUpdate(mImpagat, New System.EventArgs)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar l'impagat")
        End If

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        mImpagat.Delete()
        RaiseEvent AfterUpdate(mImpagat, New System.EventArgs)
        Me.Close()
    End Sub

    Private Sub CheckBoxSaldat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxSaldat.CheckedChanged
        DateTimePickerSaldo.Visible = CheckBoxSaldat.Checked
    End Sub

    Private Sub CheckBoxInsolvencia_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxInsolvencia.CheckedChanged
        LabelRefInsolvencia.Visible = CheckBoxInsolvencia.Checked
        TextBoxInsolvencia.Visible = CheckBoxInsolvencia.Checked
        ButtonShowInsolvencia.Visible = CheckBoxInsolvencia.Checked
    End Sub


    Private Sub CheckBoxAsnefAlta_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAsnefAlta.CheckedChanged
        If mAllowEvents Then
            DateTimePickerAsnefAlta.Visible = CheckBoxAsnefAlta.Checked
            CheckBoxAsnefBaixa.Enabled = CheckBoxAsnefAlta.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxAsnefBaixa_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAsnefBaixa.CheckedChanged
        If mAllowEvents Then
            DateTimePickerAsnefBaixa.Visible = CheckBoxAsnefBaixa.Checked
            ButtonOk.Enabled = True
        End If
    End Sub
End Class