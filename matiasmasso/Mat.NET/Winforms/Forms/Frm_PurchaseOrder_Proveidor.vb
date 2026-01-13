Imports System.ComponentModel

Public Class Frm_PurchaseOrder_Proveidor

    Private _PurchaseOrder As DTOPurchaseOrder
    Private _Cur As DTOCur
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOPurchaseOrder)
        MyBase.New
        InitializeComponent()

        _PurchaseOrder = value
    End Sub

    Private Async Sub Frm_PurchaseOrder_Proveidor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.PurchaseOrder.Load(exs, _PurchaseOrder, GlobalVariables.Emp.Mgz) Then
            Await refresca()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        With _PurchaseOrder
            SetTitle()
            TextBoxPdd.Text = .Concept
            DateTimePickerFch.Value = .fch
            SetFchDelivery(.fchDeliveryMin)
            If .fchDeliveryMin <> Nothing Then
                CheckBoxFchDelivery.Checked = True
                DateTimePickerDelivery.Visible = True
                DateTimePickerDelivery.Value = .fchDeliveryMin
            End If
            CheckBoxHide.Checked = .Hide
            Xl_PdcSrc1.Load(.Source)
            SetCurrency(.Cur)
            TextBoxObs.Text = .Obs
            Xl_ContactDeliverTo.Contact = .DeliverTo
            Await Xl_PurchaseOrderItems1.Load(_PurchaseOrder)
            Xl_UsrLog1.Load(.UsrLog)
            ButtonOk.Enabled = .IsNew
        End With
        SetTotals()
    End Function

    Private Sub SetTitle()
        Dim sTitle As String = ""
        If _PurchaseOrder.IsNew Then
            sTitle = String.Format("Nova comanda a {0}", _PurchaseOrder.Proveidor.FullNom)
        Else
            sTitle = String.Format("Comanda #{0} a {1}", _PurchaseOrder.Num, _PurchaseOrder.Proveidor.FullNom)
        End If
        Me.Text = sTitle
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquesta comanda?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.PurchaseOrder.Delete(exs, _PurchaseOrder) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "No es pot eliminar aquesta comanda:")
            End If
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        Dim wasNew As Boolean = _PurchaseOrder.IsNew
        If SetOrderFromForm(exs) Then
            Dim pOrder = Await FEB2.PurchaseOrder.Update(exs, _PurchaseOrder)
            If exs.Count = 0 Then
                _PurchaseOrder = pOrder
                If wasNew Then
                    Dim oPdf As New LegacyHelper.PdfAlb2({_PurchaseOrder}.ToList, True)
                    Dim oCert = Await FEB2.Cert.Find(GlobalVariables.Emp.Org, exs)
                    Dim oStream = oPdf.Stream(exs, oCert)
                    Dim oDocfile = LegacyHelper.DocfileHelper.Factory(exs, oStream)
                    _PurchaseOrder.DocFile = oDocfile
                    'desa un segon cop dons no podem redactar el pdf abans de saber el número de la comanda
                    _PurchaseOrder.IsNew = False 'perque gestioni l'update correctament el segon cop
                    pOrder = Await FEB2.PurchaseOrder.Update(exs, _PurchaseOrder)
                    UIHelper.ToggleProggressBar(Panel1, False)
                    If exs.Count = 0 Then
                        _PurchaseOrder = pOrder
                    Else
                        UIHelper.WarnError(exs, "Error al desar el pdf de la comanda:")
                    End If
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_PurchaseOrder))
                    MsgBox("Comanda desada amb el numero " & _PurchaseOrder.Num)
                Else
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_PurchaseOrder))
                End If

                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs, "Error al desar la comanda:")
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "Error al validar la comanda:")
        End If
    End Sub

    Private Function SetOrderFromForm(exs As List(Of Exception)) As Boolean
        With _PurchaseOrder
            .Concept = TextBoxPdd.Text
            .Fch = DateTimePickerFch.Value
            .fchDeliveryMin = GetFchDeliveryMin(exs)
            .Source = Xl_PdcSrc1.Source
            .Cur = GetCurrency()
            .Obs = TextBoxObs.Text
            .Hide = CheckBoxHide.Checked
            If Xl_ContactDeliverTo.Contact IsNot Nothing Then
                .Platform = New DTOCustomerPlatform(Xl_ContactDeliverTo.Contact.Guid)
                .Platform.FullNom = Xl_ContactDeliverTo.Contact.FullNom
            End If
            .Items = Xl_PurchaseOrderItems1.Items
            .UsrLog.UsrLastEdited = Current.Session.User
        End With

        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Private Function GetFchDeliveryMin(exs As List(Of Exception)) As Date
        Dim retval As Date
        If CheckBoxFchDelivery.Checked Then
            retval = DateTimePickerDelivery.Value
            If _PurchaseOrder.IsNew Then
                For Each item In Xl_PurchaseOrderItems1.Items
                    item.ETD = retval
                Next
            End If
        Else
            If _PurchaseOrder.IsNew Then
                exs.Add(New Exception("Cal especificar una data esperada de entrega"))
            End If
            retval = Nothing
        End If
        Return retval
    End Function

#Region "Divisas"
    Private Sub SetCurrency(oCur As DTOCur)
        Select Case oCur.Tag
            Case "EUR"
                EURToolStripMenuItem.Checked = True
            Case "USD"
                USDToolStripMenuItem.Checked = True
            Case "GBP"
                GBPToolStripMenuItem.Checked = True
            Case Else
                Dim oMenuItem As New ToolStripMenuItem(oCur.Tag)
                oMenuItem.Checked = True
                DivisaToolStripMenuItem.DropDownItems.Add(oMenuItem)
        End Select
    End Sub

    Private Async Sub CurrencyChanged(sender As Object, e As EventArgs) Handles _
            EURToolStripMenuItem.CheckedChanged,
            USDToolStripMenuItem.CheckedChanged,
            GBPToolStripMenuItem.CheckedChanged,
            ALTToolStripMenuItem.CheckedChanged

        If _AllowEvents Then
            _AllowEvents = False
            For Each item As ToolStripMenuItem In DivisaToolStripMenuItem.DropDownItems
                If Not item.Equals(sender) Then
                    item.Checked = False
                End If
            Next

            Dim exs As New List(Of Exception)
            If SetOrderFromForm(exs) Then

                For Each item In _PurchaseOrder.items
                    item.price = _PurchaseOrder.cur.AmtFromEuros(item.price.Eur)
                Next

                Await refresca()
                ButtonOk.Enabled = True
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Function GetCurrency() As DTOCur
        Dim retval As DTOCur = Nothing

        If EURToolStripMenuItem.Checked Then
            retval = DTOCur.Factory("EUR")
        ElseIf USDToolStripMenuItem.Checked Then
            retval = DTOCur.Factory("USD")
        ElseIf GBPToolStripMenuItem.Checked Then
            retval = DTOCur.Factory("GBP")
        ElseIf ALTToolStripMenuItem.Checked Then
            retval = DTOCur.Factory(ALTToolStripMenuItem.Text)
        End If
        Return retval
    End Function

#End Region

    Private Sub SetFchDelivery(DtFch As Date)
        If DtFch <> Nothing Then
            CheckBoxFchDelivery.Checked = True
            DateTimePickerDelivery.Value = DtFch
        End If
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        If SetOrderFromForm(exs) Then
            Dim oSheet = FEB2.PurchaseOrder.Excel(_PurchaseOrder)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs, "Error al redactar l'Excel:")
            End If
        Else
            UIHelper.WarnError(exs, "Error al validar la comanda:")
        End If
    End Sub



    Private Sub SetTotals()

        Dim allM3 As Decimal = 0
        Dim pendingM3 As Decimal = 0
        Dim oAmt = DTOAmt.Empty
        Dim oPendingAmt = DTOAmt.Empty
        For Each item In Xl_PurchaseOrderItems1.Items
            oAmt.Add(DTOAmt.FromQtyPriceDto(item.Qty, item.Price, item.Dto))
            oPendingAmt.Add(DTOAmt.FromQtyPriceDto(item.Pending, item.Price, item.Dto))
            allM3 += (item.Qty * DTOProductSku.VolumeM3OrInherited(item.Sku))
            pendingM3 += (item.Pending * DTOProductSku.VolumeM3OrInherited(item.Sku))
        Next

        Dim s As String = String.Format("Import: {0} / Volum: {1:0.000} m3", FormattedImport(oAmt), allM3)
        If (allM3 <> pendingM3 AndAlso pendingM3 > 0) Or (oAmt.unEquals(oPendingAmt) AndAlso oAmt.IsNotZero) Then
            s += String.Format(" (pendent: Import: {0} / Volum: {1:0.000} m3)", FormattedImport(oPendingAmt), pendingM3)
        End If
        TextBoxTotals.Text = s
    End Sub

    Private Function FormattedImport(oAmt As DTOAmt) As String
        Dim retval As String = DTOAmt.CurFormatted(oAmt)
        If oAmt.Cur.UnEquals(DTOCur.Eur) Then
            retval = String.Format("{0} ({1})", DTOAmt.CurFormatted(oAmt), DTOAmt.CurFormatted(DTOAmt.Factory(oAmt.Eur)))
        End If
        Return retval
    End Function


    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        Xl_ContactDeliverTo.AfterUpdate,
         TextBoxPdd.TextChanged,
          TextBoxObs.TextChanged,
           DateTimePickerFch.ValueChanged,
            DateTimePickerDelivery.ValueChanged,
             CheckBoxFchDelivery.CheckedChanged,
              CheckBoxHide.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxFchDelivery_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchDelivery.CheckedChanged
        If _AllowEvents Then
            DateTimePickerDelivery.Visible = CheckBoxFchDelivery.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_PurchaseOrderItems1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrderItems1.AfterUpdate
        SetTotals()
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Frm_PurchaseOrder_Proveidor_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim exs As New List(Of Exception)
        If Not FEB2.AlbBloqueig.BloqueigEnd(Current.Session.User, _PurchaseOrder.Proveidor, DTOAlbBloqueig.Codis.PDC, exs) Then
            e.Cancel = True
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class