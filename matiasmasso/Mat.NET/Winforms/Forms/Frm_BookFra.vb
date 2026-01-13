
Public Class Frm_BookFra
    Private _BookFra As DTOBookFra
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oBookFra As DTOBookFra)
        MyBase.New()
        Me.InitializeComponent()
        _BookFra = oBookFra
    End Sub

    Private Sub Frm_BookFra_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.BookFra.Load(exs, _BookFra) Then
            Dim bl As Boolean = Xl_BaseQuotaIva.EditQuotaAllowed
            LoadComboboxes()
            Refresca()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Refresca()
        With _BookFra
            Xl_Contact1.Contact = .Contact
            Xl_Cta1.Cta = .Cta
            TextBoxFranum.Text = .FraNum
            TextBoxDsc.Text = .Dsc

            If .TipoFra = Nothing Then
                ComboBoxTipoFra.SelectedIndex = 0
            Else
                ComboBoxTipoFra.SelectedValue = .TipoFra
            End If

            If Not String.IsNullOrEmpty(.ClaveRegimenEspecialOTrascendencia) Then
                ComboBoxRegEspOTrascs.SelectedValue = .ClaveRegimenEspecialOTrascendencia
            End If

            Dim oSujetos As List(Of DTOBaseQuota) = .IvaBaseQuotas.
            Where(Function(x) x.Tipus > 0).
            OrderByDescending(Function(y) y.Tipus).ToList

            If oSujetos.Count > 0 Then
                Xl_BaseQuotaIva.Load(oSujetos(0))
                If oSujetos.Count > 1 Then
                    Xl_BaseQuotaIva1.Load(oSujetos(1))
                    If oSujetos.Count > 2 Then
                        Xl_BaseQuotaIva2.Load(oSujetos(2))
                    End If
                End If
            End If

            If .BaseQuotaIvaExenta IsNot Nothing Then
                Xl_AmtCurBaseExento.Amt = .baseQuotaIvaExenta.baseImponible
                ComboBoxCausaExempcio.SelectedValue = .ClaveExenta
            End If

            If .IrpfBaseQuota IsNot Nothing Then
                Xl_BaseQuotaIrpf.Load(.IrpfBaseQuota)
            End If

            Xl_DocFile1.Load(.Cca.DocFile)
            Calcula()

            Xl_SiiLog1.Load(.SiiLog)
            'LabelSiiError.Text = .SiiLog.ErrMsg
            Select Case .SiiEstadoCuadre
                Case 0
                    LabelSiiCuadre.Text = ""
                Case 1
                    LabelSiiCuadre.Text = "No contrastable"
                Case 2
                    LabelSiiCuadre.Text = String.Format("{0} {1:dd/MM/yyy HH:mm:ss}", "En proceso de contraste", .SiiTimestampEstadoCuadre)
                Case 3
                    LabelSiiCuadre.Text = String.Format("{0} {1:dd/MM/yyy HH:mm:ss}", "No contrastada", .SiiTimestampEstadoCuadre)
                Case 4
                    LabelSiiCuadre.Text = String.Format("{0} {1:dd/MM/yyy HH:mm:ss}", "Parcialmente contrastada", .SiiTimestampEstadoCuadre)
                Case 5
                    LabelSiiCuadre.Text = String.Format("{0} {1:dd/MM/yyy HH:mm:ss}", "Contrastada", .SiiTimestampEstadoCuadre)
            End Select

            If .SiiTimestampUltimaModificacion = Nothing Then
                LabelSiiLastEdited.Text = ""
            Else
                LabelSiiLastEdited.Text = String.Format("{0:dd/MM/yyy HH:mm:ss}", .SiiTimestampUltimaModificacion)
            End If


            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
    End Sub

    Private Sub Calcula()
        Dim oTot = DTOAmt.Empty
        oTot.Add(Xl_BaseQuotaIva.Base)
        oTot.Add(Xl_BaseQuotaIva1.Base)
        oTot.Add(Xl_BaseQuotaIva2.Base)
        oTot.Add(Xl_AmtCurBaseExento.Amt)
        Xl_AmtDevengat.Amt = oTot.Clone

        oTot.Add(Xl_BaseQuotaIva.Quota)
        oTot.Add(Xl_BaseQuotaIva1.Quota)
        oTot.Add(Xl_BaseQuotaIva2.Quota)
        oTot.Substract(Xl_BaseQuotaIrpf.Quota)
        Xl_AmtTot.Amt = oTot
    End Sub

    Private Sub LoadComboboxes()
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
        With ComboBoxRegEspOTrascs
            .DataSource = regEspOTrascs
            .DisplayMember = "value"
            .ValueMember = "key"
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         Xl_Contact1.AfterUpdate,
          Xl_Cta1.AfterUpdate,
            TextBoxFranum.TextChanged,
             ComboBoxTipoFra.SelectedIndexChanged,
              ComboBoxCausaExempcio.SelectedIndexChanged,
               ComboBoxRegEspOTrascs.SelectedIndexChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub AmountChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
             Xl_BaseQuotaIva.AfterUpdate,
             Xl_BaseQuotaIva1.AfterUpdate,
             Xl_BaseQuotaIva2.AfterUpdate,
              Xl_BaseQuotaIrpf.AfterUpdate,
               Xl_AmtCurBaseExento.AfterUpdate,
                Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            Calcula()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)

        With _BookFra
            .Contact = Xl_Contact1.Contact
            .Cta = Xl_Cta1.Cta
            .TipoFra = ComboBoxTipoFra.SelectedValue

            .FraNum = TextBoxFranum.Text
            .Dsc = TextBoxDsc.Text
            .ClaveRegimenEspecialOTrascendencia = ComboBoxRegEspOTrascs.SelectedValue
            .IvaBaseQuotas = New List(Of DTOBaseQuota)
            If ComboBoxTipoFra.SelectedValue = "" Then
                exs.Add(New Exception("Cal indicar el tipus de factura"))
            Else
                .TipoFra = ComboBoxTipoFra.SelectedValue
            End If
            .IvaBaseQuotas = New List(Of DTOBaseQuota)
            If Xl_BaseQuotaIva.Quota IsNot Nothing Then
                If Xl_BaseQuotaIva.Quota.IsNotZero Then
                    .IvaBaseQuotas.Add(Xl_BaseQuotaIva.Value)
                End If
            End If
            If Xl_BaseQuotaIva1.Quota IsNot Nothing Then
                If Xl_BaseQuotaIva1.Quota.IsNotZero Then
                    .IvaBaseQuotas.Add(Xl_BaseQuotaIva1.Value)
                End If
            End If
            If Xl_BaseQuotaIva2.Quota IsNot Nothing Then
                If Xl_BaseQuotaIva2.Quota.IsNotZero Then
                    .IvaBaseQuotas.Add(Xl_BaseQuotaIva2.Value)
                End If
            End If
            If Xl_AmtCurBaseExento.Amt IsNot Nothing Then
                If Xl_AmtCurBaseExento.Amt.IsNotZero Then
                    Dim oExento As New DTOBaseQuota(Xl_AmtCurBaseExento.Amt)
                    .IvaBaseQuotas.Add(oExento)
                    If ComboBoxCausaExempcio.SelectedValue = "" Then
                        exs.Add(New Exception("Cal indicar la causa de exempció de Iva"))
                    Else
                        .ClaveExenta = ComboBoxCausaExempcio.SelectedValue
                    End If
                End If
            End If
            If Xl_BaseQuotaIrpf.Quota IsNot Nothing Then
                If Xl_BaseQuotaIrpf.Quota.IsNotZero Then
                    .IrpfBaseQuota = Xl_BaseQuotaIrpf.Value
                End If
            End If

            If exs.Count = 0 Then
                If Await FEB2.BookFra.Update(exs, _BookFra) Then
                    If Xl_DocFile1.IsDirty Then
                        If FEB2.Cca.Load(.Cca, exs) Then
                            .Cca.DocFile = Xl_DocFile1.Value
                            .Cca.UsrLog.UsrLastEdited = Current.Session.User
                            .Cca.Id = Await FEB2.Cca.Update(exs, .Cca)
                            If exs.Count = 0 Then
                                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BookFra))
                                Me.Close()
                            Else
                                MsgBox("error al desar assentament de la factura" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
                            End If
                        Else
                            UIHelper.ToggleProggressBar(Panel1, False)
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        RaiseEvent AfterUpdate(Me, New MatEventArgs(_BookFra))
                        Me.Close()
                    End If
                Else
                    UIHelper.ToggleProggressBar(Panel1, False)
                    MsgBox("error al desar la factura:" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
                End If
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
        End With


    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB2.BookFra.Delete(exs, _BookFra) Then
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class