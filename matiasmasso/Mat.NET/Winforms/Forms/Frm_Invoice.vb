Public Class Frm_Invoice

    Private _Invoice As DTOInvoice
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOInvoice)
        MyBase.New()
        Me.InitializeComponent()
        _Invoice = value

        UIHelper.LoadComboFromEnum(ComboBoxConcepte, GetType(DTOInvoice.Conceptes), "(sel·leccionar concepte)")
        Dim CausasExencion As List(Of KeyValuePair(Of String, String)) = DTOBookFra.CausasExencion
        With ComboBoxCausaExempcio
            .DataSource = CausasExencion
            .DisplayMember = "value"
            .ValueMember = "key"
        End With
        Dim tiposFra As List(Of KeyValuePair(Of String, String)) = DTOBookFra.TiposFra
        With ComboBoxTipoFra
            .DataSource = tiposFra
            .DisplayMember = "value"
            .ValueMember = "key"
        End With
        Dim regEspOTrascs As List(Of KeyValuePair(Of String, String)) = DTOBookFra.regEspOTrascs
        With ComboBoxregEspOTrascs
            .DataSource = regEspOTrascs
            .DisplayMember = "value"
            .ValueMember = "key"
        End With

    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Invoice.Load(_Invoice, exs) Then
            If Not _Invoice.customer.IsConsumer Then
                'prevent reloading consumer since invoice has loaded customer name and nif from delivery consumerTicket
                FEB2.Customer.Load(_Invoice.customer, exs)
            End If

            Dim oInvoices As New List(Of DTOInvoice)
            oInvoices.Add(_Invoice)
            Dim oMenu As New Menu_Invoice(oInvoices)
            ArxiuToolStripMenuItem.DropDownItems.AddRange(oMenu.Range)

            LoadSiiTab()

            With _Invoice
                Await SetNumYSerie(exs)

                TextBoxNom.Text = .Nom
                If .Nifs IsNot Nothing AndAlso .Nifs.Count > 0 Then
                    Xl_LookupNif1.Load(.Nifs.First)
                End If
                TextBoxAdr.Text = .Adr
                Xl_LookupZip1.Load(.Zip)

                DateTimePicker1.Value = .Fch
                Xl_Langs1.Value = .Lang

                Dim oIncoterms = Await FEB2.Incoterms.All(exs)
                If exs.Count = 0 Then
                    Xl_LookupIncoterm1.Load(oIncoterms, .Incoterm)
                Else
                    UIHelper.WarnError(exs)
                End If

                If .ExportCod <> DTOInvoice.ExportCods.nacional Then
                    CheckBoxExport.Checked = True
                    CheckBoxCEE.Checked = .ExportCod = DTOInvoice.ExportCods.intracomunitari
                End If

                SetObs()

                Xl_InvoiceDeliveryItems1.Load(_Invoice.Deliveries, Xl_Langs1.Value)
                SetAmounts()
                Dim oMenuContact As New Menu_Contact(_Invoice.Customer)
                Dim oContextMenu As New ContextMenuStrip()
                oContextMenu.Items.AddRange(oMenuContact.Range)
                TextBoxNom.ContextMenuStrip = oContextMenu

                ShowStatus(_Invoice)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
        Else
            UIHelper.WarnError(exs)
        End If
        _AllowEvents = True
    End Sub

    Private Sub SetObs()
        With _Invoice
            TextBoxObs.Text = String.Format("{0}{1}{2}{3}", .Fpg & vbCrLf, .Ob1 & vbCrLf, .Ob2 & vbCrLf, .Ob3)
        End With
    End Sub

    Private Async Function SetNumYSerie(exs As List(Of Exception)) As Task
        With _Invoice
            If .IsNew Then
                Dim iNums = Await FEB2.Invoices.AvailableNums(Current.Session.Emp, .Fch.Year, .Serie, exs)
                If iNums.Count = 1 Then
                    'nomes dona el següent; conservar el que ha posat per defecte
                    TextBoxFra.Text = .NumeroYSerie
                Else
                    TextBoxFra.Visible = False
                    ComboBoxFraNum.Location = TextBoxFra.Location
                    ComboBoxFraNum.Visible = True
                    ComboBoxFraNum.DataSource = iNums
                    ComboBoxFraNum.SelectedIndex = 0
                End If
                DateTimePicker1.Enabled = True
            Else
                TextBoxFra.Text = .NumeroYSerie
            End If
        End With
    End Function

    Private Sub SetAmounts()
        Dim oBaseImponible = Xl_InvoiceDeliveryItems1.BaseImponible
        Dim oTotal = oBaseImponible.clone
        If oBaseImponible IsNot Nothing Then
            TextBoxBaseImponible.Text = DTOAmt.CurFormatted(oBaseImponible)

            If CheckBoxExport.Checked Then
                TextBoxIvaPct.Clear()
                TextBoxIvaAmt.Clear()
                TextBoxReqPct.Clear()
                TextBoxReqAmt.Clear()
            Else
                TextBoxIvaPct.Text = PercentFormated(DTOInvoice.ivaTipus(_Invoice))
                Dim oIvaAmt As DTOAmt = DTOInvoice.getIvaAmt(_Invoice)
                If oIvaAmt IsNot Nothing Then
                    oTotal.add(oIvaAmt)
                    TextBoxIvaAmt.Text = DTOAmt.CurFormatted(oIvaAmt)
                End If

                TextBoxReqPct.Text = PercentFormated(DTOInvoice.reqTipus(_Invoice))
                Dim oReqAmt As DTOAmt = DTOInvoice.getReqAmt(_Invoice)
                If oReqAmt IsNot Nothing Then
                    oTotal.add(oReqAmt)
                    TextBoxReqAmt.Text = DTOAmt.CurFormatted(oReqAmt)
                End If
            End If

            TextBoxLiquid.Text = DTOAmt.CurFormatted(oTotal)
        End If

    End Sub

    Private Sub LoadSiiTab()
        With _Invoice
            ComboBoxTipoFra.SelectedValue = .TipoFactura
            ComboBoxConcepte.SelectedIndex = .Concepte
            If .RegimenEspecialOTrascendencia <> Nothing Then
                ComboBoxRegEspOTrascs.SelectedValue = .RegimenEspecialOTrascendencia
            End If
            LabelNif.Text = .Customer.PrimaryNifCodNom(Current.Session.Lang)
            TextBoxNif.Text = .Customer.PrimaryNifValue()
            If .BaseImponible IsNot Nothing Then
                TextBoxBaseExempta.Text = DTOAmt.CurFormatted(.BaseImponible)
            End If
            ComboBoxCausaExempcio.SelectedValue = .SiiL9
            If DTOInvoice.isIVAExento(_Invoice) Then
                RadioButtonExempta.Checked = True
                GroupBoxSubjecta.Enabled = False
            ElseIf .IvaBaseQuotas IsNot Nothing Then
                Dim oTaxBaseQuota As DTOTaxBaseQuota = .IvaBaseQuotas.First
                TextBoxBaseSujeta.Text = DTOAmt.CurFormatted(oTaxBaseQuota.baseImponible)
                TextBoxIvaTipus.Text = oTaxBaseQuota.Tax.Tipus & "%"
                TextBoxIvaQuota.Text = DTOAmt.CurFormatted(oTaxBaseQuota.Quota)
                RadioButtonSubjecta.Checked = True
                GroupBoxExempta.Enabled = False
            End If
            Xl_SiiLog1.Load(.SiiLog)
        End With
    End Sub
    Private Function PercentFormated(src As Decimal) As String
        Dim retval As String = ""
        If src <> 0 Then
            If Math.Round(src, 0, MidpointRounding.AwayFromZero) = src Then
                retval = String.Format("{0:N0}%", src)
            ElseIf Math.Round(src, 1, MidpointRounding.AwayFromZero) = src Then
                retval = String.Format("{0:N1}%", src)
            ElseIf Math.Round(src, 2, MidpointRounding.AwayFromZero) = src Then
                retval = String.Format("{0:N2}%", src)
            End If
        End If
        Return retval
    End Function

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxFra.TextChanged,
         ComboBoxConcepte.SelectedIndexChanged,
          ComboBoxTipoFra.SelectedIndexChanged,
           ComboBoxCausaExempcio.SelectedIndexChanged,
            ComboBoxTipoFra.SelectedIndexChanged,
             ComboBoxRegEspOTrascs.SelectedIndexChanged,
              TextBoxNom.TextChanged,
               TextBoxAdr.TextChanged,
                Xl_LookupNif1.AfterUpdate,
                 Xl_LookupZip1.AfterUpdate,
                  Xl_LookupIncoterm1.AfterUpdate,
                   CheckBoxCEE.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub ShowStatus(oInvoice As DTOInvoice)
        Dim sb As New Text.StringBuilder

        If oInvoice.SiiLog IsNot Nothing Then
            'sb.Append("Sii: " & Format(oInvoice.SiiLog.Fch, "dd/MM/yy HH:mm") & " " & oInvoice.SiiErr)
        End If
        If oInvoice.IsNew Then
            sb.Append("pendent de desar")
        Else
            sb.Append(DTOInvoice.LastPrintedText(oInvoice))
        End If

        LabelStatus.Text = sb.ToString
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(PanelButtons, True)
        Dim sText As String = TextBoxObs.Text

        Dim lines() As String = sText.Split(vbCrLf)
        With _Invoice

            If CheckBoxExport.Checked Then
                If CheckBoxCEE.Checked Then
                    .ExportCod = DTOInvoice.ExportCods.intracomunitari
                Else
                    .ExportCod = DTOInvoice.ExportCods.extracomunitari
                End If
            Else
                .ExportCod = DTOInvoice.ExportCods.nacional
            End If
            .Incoterm = Xl_LookupIncoterm1.Value

            .Nom = TextBoxNom.Text
            .Adr = TextBoxAdr.Text
            .Zip = Xl_LookupZip1.Zip
            .Lang = Xl_Langs1.Value

            If .Nifs Is Nothing OrElse .Nifs.Count <= 1 Then
                If Xl_LookupNif1.Nif IsNot Nothing Then
                    .Nifs = DTONif.Collection.Factory(Xl_LookupNif1.Nif.Value, Xl_LookupNif1.Nif.Cod)
                End If
            Else
                If Xl_LookupNif1.Nif Is Nothing Then
                    .Nifs = DTONif.Collection.Factory(.Nifs(1).Value, .Nifs(1).Cod)
                Else
                    .Nifs = DTONif.Collection.Factory(Xl_LookupNif1.Nif.Value, Xl_LookupNif1.Nif.Cod, .Nifs(1).Value, .Nifs(1).Cod)
                End If
            End If

            If .IsNew Then
                If ComboBoxFraNum.Visible Then
                    If IsNumeric(ComboBoxFraNum.SelectedItem) Then
                        .Num = ComboBoxFraNum.SelectedItem
                    End If
                End If
                .Fch = DateTimePicker1.Value
                If .Vto = Nothing Then .Vto = .Fch
            End If
            If .SiiLog IsNot Nothing Then
                If .SiiLog.Result = DTOSiiLog.Results.Correcto Then
                    Dim sMsg As String = "Aquesta factura ja ha estat comunicada a Hisenda" & vbCrLf & "La modifiquem igualment?"
                    Dim rc = MsgBox(sMsg, MsgBoxStyle.OkCancel)
                    If rc <> MsgBoxResult.Ok Then
                        .AddException(DTOInvoiceException.Cods.siiLogged, sMsg)
                    End If
                End If
            End If
            If ComboBoxConcepte.SelectedIndex = DTOInvoice.Conceptes.notSet Then
                Dim sMsg As String = "Cal sel·leccioonar un concepte de facturació"
                .AddException(DTOInvoiceException.Cods.missingConcept, sMsg)
            Else
                .Concepte = ComboBoxConcepte.SelectedIndex
            End If
            .Exceptions.RemoveAll(Function(x) x.cod = DTOInvoiceException.Cods.obsTooLong)
            If lines.Length > 0 Then
                .Fpg = lines(0).Trim
                If .Fpg.Length > 50 Then
                    Dim sMsg As String = String.Format("La forma de pagament té {0} caracters quan no pot passar de {1}.", .Fpg.Length, 50)
                    .AddException(DTOInvoiceException.Cods.obsTooLong, sMsg)
                End If
                If lines.Length > 1 Then
                    .Ob1 = lines(1).Trim
                    If .Ob1.Length > 50 Then
                        Dim sMsg As String = String.Format("La 1ª linia de observacions té {0} caracters quan no pot passar de {1}.", .Ob1.Length, 50)
                        .AddException(DTOInvoiceException.Cods.obsTooLong, sMsg)
                    End If
                    If lines.Length > 2 Then
                        .Ob2 = lines(2).Trim
                        If .Ob2.Length > 50 Then
                            Dim sMsg As String = String.Format("La 2ª linia de observacions té {0} caracters quan no pot passar de {1}.", .Ob2.Length, 50)
                            .AddException(DTOInvoiceException.Cods.obsTooLong, sMsg)
                        End If
                        If lines.Length > 3 Then
                            .Ob3 = lines(3).Trim
                            If .Ob3.Length > 50 Then
                                Dim sMsg As String = String.Format("La 3ª linia de observacions té {0} caracters quan no pot passar de {1}.", .Ob3.Length, 50)
                                .AddException(DTOInvoiceException.Cods.obsTooLong, sMsg)
                            End If
                            If lines.Length > 4 Then
                                MsgBox("Les linies de observacions mes enllà de la quarta no queden registrades", MsgBoxStyle.Exclamation)
                            End If
                        End If
                    End If
                End If
            End If

            If ComboBoxTipoFra.SelectedIndex >= 0 Then
                .TipoFactura = ComboBoxTipoFra.SelectedValue
            Else
                .AddException(DTOInvoiceException.Cods.missingTipoFactura, "Falta especificar el tipus de factura")
            End If

            If .ExportCod = DTOInvoice.ExportCods.nacional Then
                .SiiL9 = ""
            Else
                If ComboBoxCausaExempcio.SelectedIndex < 0 Then
                    .AddException(DTOInvoiceException.Cods.missingTipoFactura, "Falta especificar el tipus de factura")
                Else
                    .SiiL9 = ComboBoxCausaExempcio.SelectedValue
                End If
            End If

            .RegimenEspecialOTrascendencia = ComboBoxRegEspOTrascs.SelectedValue

            If .TipoFactura = "F1" And .Serie = DTOInvoice.Series.rectificativa Then
                .TipoFactura = "R1"
            End If
        End With

        If _Invoice.Exceptions.Count = 0 Then
            Dim exs As New List(Of Exception)
            If Await InvoiceUpdaterHelper.Update(exs, _Invoice, Current.Session.User) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Invoice))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al desar")
            End If
        Else
            Dim sMsg As String = DTOInvoiceException.MultilineString(_Invoice.Exceptions)
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(sMsg)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.Invoice.Delete(_Invoice, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Invoice))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TextBoxCustomer_DoubleClick(sender As Object, e As EventArgs)
        Dim oFrm As New Frm_Contact(_Invoice.Customer)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaCustomer
        oFrm.Show()
    End Sub

    Private Sub refrescaCustomer(sender As Object, e As MatEventArgs)
        Dim oCustomer As DTOCustomer = e.Argument
        Dim exs As New List(Of Exception)
    End Sub

    Private Sub FitxaToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim oFrm As New Frm_Contact(_Invoice.Customer)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaCustomer
        oFrm.Show()
    End Sub

    Private Sub TextBoxObs_TextChanged(sender As Object, e As EventArgs) Handles TextBoxObs.TextChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub RadioButtonSubjecta_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSubjecta.CheckedChanged
        If RadioButtonExempta.Checked Then
            GroupBoxExempta.Enabled = True
            GroupBoxSubjecta.Enabled = False
        Else
            GroupBoxExempta.Enabled = False
            GroupBoxSubjecta.Enabled = True
        End If
    End Sub

    Private Async Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Dim iYear = DateTimePicker1.Value.Year
        If _Invoice.IsNew AndAlso _Invoice.Fch.Year <> iYear Then
            Dim exs As New List(Of Exception)
            Dim iNums = Await FEB2.Invoices.AvailableNums(Current.Session.Emp, iYear, _Invoice.Serie, exs)
            If iNums.Count = 1 Then
                TextBoxFra.Text = DTOInvoice.NumeroYSerie(iNums.First, _Invoice.Serie)
                _Invoice.Num = iNums.First
            Else
                TextBoxFra.Visible = False
                ComboBoxFraNum.Location = TextBoxFra.Location
                ComboBoxFraNum.Visible = True
                ComboBoxFraNum.DataSource = iNums
                ComboBoxFraNum.SelectedIndex = 0
            End If
            DateTimePicker1.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxExport_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxExport.CheckedChanged
        CheckBoxCEE.Visible = CheckBoxExport.Checked
        Xl_LookupIncoterm1.Visible = CheckBoxExport.Checked
        If _AllowEvents Then
            SetAmounts()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_Langs1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Langs1.AfterUpdate
        If _AllowEvents Then
            Xl_InvoiceDeliveryItems1.Load(_Invoice.Deliveries, Xl_Langs1.Value)
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub InversionSujetoPasivoToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles InversionSujetoPasivoToolStripMenuItem.CheckedChanged
        Dim exs As New List(Of Exception)
        If _AllowEvents Then
            If _Invoice.IsNew Then
                Dim ispText = Xl_Langs1.Value.Tradueix("Operación con inversión del sujeto pasivo conforme al Art. 84 (Uno.2º) de la Ley 37/1992 de IVA", "Operació amb inversió del subjecte passiu d’acord amb el previst a l’article 84.U.2º.f de la Llei d’IVA 37/1992")
                If _Invoice.Serie = DTOInvoice.Series.inversionSujetoPasivo Then
                    _Invoice.Serie = DTOInvoice.Series.standard
                    If ispText.Contains(_Invoice.Ob1) Then _Invoice.Ob1 = ""
                    If ispText.Contains(_Invoice.Ob2) Then _Invoice.Ob2 = ""
                    If ispText.Contains(_Invoice.Ob3) Then _Invoice.Ob3 = ""
                Else
                    _Invoice.Serie = DTOInvoice.Series.inversionSujetoPasivo
                    _Invoice.Ob1 = ispText.Substring(0, 50)
                    _Invoice.Ob2 = ispText.Substring(50)
                End If
                SetObs()
                Dim oTaxes As List(Of DTOTax) = DTOTax.closest(DateTimePicker1.Value)
                DTOInvoice.setImports(_Invoice, oTaxes)
                SetAmounts()
                Await SetNumYSerie(exs)
                If exs.Count > 0 Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError("No es pot canviar el subjecte passiu un cop desada la factura. Cal eliminar-la i tornar-la a expedir")
                _AllowEvents = False
                InversionSujetoPasivoToolStripMenuItem.Checked = Not InversionSujetoPasivoToolStripMenuItem.Checked
                _AllowEvents = True
            End If
        End If
    End Sub
End Class


