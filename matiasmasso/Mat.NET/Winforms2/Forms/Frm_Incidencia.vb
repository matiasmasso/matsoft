Public Class Frm_Incidencia
    Private _Incidencia As DTOIncidencia
    Private _trackingsLoaded
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        General
        Tracking
    End Enum

    Public Sub New(oIncidencia As DTOIncidencia)
        MyBase.New()
        Me.InitializeComponent()
        _Incidencia = oIncidencia
    End Sub

    Private Async Sub Frm_Incidencia_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        Me.Show()
        Application.DoEvents()

        If _Incidencia.IsNew Then
        Else
            _Incidencia = Await FEB.Incidencia.Find(exs, _Incidencia.Guid, includeThumbnails:=True)
        End If
        If exs.Count = 0 Then
            refresca()
            Await LoadTrackingCodes()

            UIHelper.ToggleProggressBar(Panel1, False)
            If Not _Incidencia.IsNew AndAlso Not _Incidencia.isAlreadyRead() Then
                Dim oTracking = _Incidencia.TrackRead(Current.Session.User)
                If Await FEB.Tracking.Update(exs, oTracking) Then
                    _Incidencia.Trackings.Add(oTracking)
                Else
                    UIHelper.WarnError(exs, "Error al registrar la lectura de la incidencia")
                End If
            End If
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If


    End Sub

    Private Async Function LoadTrackingCodes() As Task
        Dim exs As New List(Of Exception)
        Dim oCodRoot = DTOCod.Wellknown(DTOCod.Wellknowns.Incidencias)
        Dim oCods = Await FEB.Cods.All(exs, oCodRoot)
        oCods.Insert(0, New DTOCod(Guid.Empty))
        oCods.First.Nom = DTOLangText.Factory("(seleccionar nou estat a afegir)")
        ComboBoxTrackings.DataSource = oCods
        ComboBoxTrackings.DisplayMember = "DisplayMember"
    End Function


    Private Sub refresca()
        With _Incidencia
            SetCod(.src)
            If _Incidencia.IsNew() Then
                Me.Text = "Nova incidencia"
            Else
                Me.Text = "Incidencia"
                ButtonDel.Enabled = True
                ButtonSpv.Enabled = .Spv Is Nothing
            End If

            Xl_IncidenciaDocFilesImgs.Load(_Incidencia, {DTOIncidencia.AttachmentCods.imatge, DTOIncidencia.AttachmentCods.video})
            Xl_IncidenciaDocFilesTicket.Load(_Incidencia, {DTOIncidencia.AttachmentCods.ticket})

            If .src = DTOIncidencia.Srcs.Producte Then
                Me.Text = Me.Text & " de producte "
                Dim oProducts As New List(Of DTOProduct)
                If .Product IsNot Nothing Then oProducts.Add(.Product)
                Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny, True)
            Else
                Me.Text = Me.Text & " de transport "
                Xl_LookupProduct1.Visible = False
            End If

            Me.Text = Me.Text & .AsinAndNum()


            Xl_LookupIncidenciaCodApertura.Load(DTOIncidenciaCod.Cods.averia, .Codi, "(incidencia pendent de codificar)")
            Xl_LookupIncidenciaCodTancament.Load(DTOIncidenciaCod.Cods.tancament, .Tancament, "(tancament pendent de codificar)")

            If .Customer Is Nothing Then
                RadioButtonConsumidor.Checked = True
            Else
                RadioButtonProfesional.Checked = True
                Xl_Contact21.Contact = .Customer
            End If

            TextBoxSerialNumber.Text = .SerialNumber
            TextBoxManufactureDate.Text = .ManufactureDate
            TextBoxPerson.Text = .ContactPerson
            TextBoxTel.Text = .Tel
            TextBoxEmail.Text = .EmailAdr
            TextBoxRef.Text = .CustomerRef

            ComboBoxProcedencia.SelectedIndex = .Procedencia
            DateTimePickerFchCompra.Load(.FchCompra)

            SetRadioButtons()
            DateTimePicker1.Value = .Fch
            TextBoxObs.Text = .Description


            If .FchClose > DateTimePicker2.MinDate Then
                CheckBoxClosed.Checked = True
                DateTimePicker2.Value = .FchClose
                DateTimePicker2.Visible = True

                Xl_LookupIncidenciaCodTancament.Visible = True
            Else
            End If

            Xl_Trackings1.Load(.Trackings)

            Xl_UsrLog1.Load(.UsrLog)

            If (.ContactType = DTOIncidencia.ContactTypes.consumidor) Then
                TextBoxBoughtWhere.Enabled = True
                TextBoxBoughtWhere.Text = .BoughtFrom
                ComboBoxProcedencia.Enabled = False
            Else
                TextBoxBoughtWhere.Enabled = False
                ComboBoxProcedencia.Enabled = True
            End If

            _AllowEvents = True
        End With
    End Sub

    Private Sub SetCod(ByVal osrc As DTOIncidencia.Srcs)
        Select Case osrc
            Case DTOIncidencia.Srcs.Producte
        End Select
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub


    Private Sub RadioButton_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        SetRadioButtons()
    End Sub

    Private Sub SetRadioButtons()
        Xl_LookupProduct1.Visible = (_Incidencia.src = DTOIncidencia.Srcs.Producte)

        Dim BlPro As Boolean = RadioButtonProfesional.Checked
        If BlPro Then
            Xl_Contact21.Visible = True
            If Xl_Contact21.Contact.UnEquals(_Incidencia.Customer) Then
                Xl_Contact21.Contact = _Incidencia.Customer
            End If
        Else
            Xl_Contact21.Clear()
            Xl_Contact21.Visible = False
        End If
    End Sub




    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
 TextBoxObs.TextChanged,
  CheckBoxClosed.CheckedChanged,
  DateTimePicker1.ValueChanged,
 DateTimePicker2.ValueChanged,
  ComboBoxProcedencia.SelectedIndexChanged,
 Xl_LookupIncidenciaCodApertura.AfterUpdate,
  TextBoxEmail.TextChanged,
   TextBoxPerson.TextChanged,
    TextBoxRef.TextChanged,
     TextBoxSerialNumber.TextChanged,
      TextBoxManufactureDate.TextChanged,
      Xl_LookupIncidenciaCodTancament.AfterUpdate,
       TextBoxTel.TextChanged,
        Xl_Contact21.AfterUpdate,
         Xl_IncidenciaDocFilesTicket.AfterUpdate,
          Xl_IncidenciaDocFilesImgs.AfterUpdate,
           Xl_LookupProduct1.AfterUpdate,
            DateTimePickerFchCompra.ValueChanged,
             TextBoxBoughtWhere.TextChanged

        If _AllowEvents Then
            SetButtons()
        End If
    End Sub

    Private Sub RadioButtonConsumidor_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonConsumidor.CheckedChanged
        Xl_Contact21.Visible = Not RadioButtonConsumidor.Checked
        If _AllowEvents Then
            SetButtons()
        End If
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click

        If Xl_LookupProduct1.Product Is Nothing And _Incidencia.src = DTOIncidencia.Srcs.Producte Then
            UIHelper.WarnError("falta posar el producte")
        Else
            UIHelper.ToggleProggressBar(Panel1, True)

            ReadIncidenciaFromForm()

            Dim IsNew As Boolean = (_Incidencia.IsNew)
            Dim exs As New List(Of Exception)

            _Incidencia.UsrLog.usrLastEdited = Current.Session.User
            _Incidencia = Await FEB.Incidencia.Update(exs, _Incidencia)
            UIHelper.ToggleProggressBar(Panel1, False)
            If exs.Count = 0 Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Incidencia))
                If IsNew Then
                    MsgBox("Número de incidencia: " & _Incidencia.AsinAndNum(), MsgBoxStyle.Information, "MAT.NET")
                End If
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs, "error al desar la incidencia")
            End If

        End If
    End Sub


    Private Sub CheckBoxClosed_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxClosed.CheckedChanged
        If _AllowEvents Then
            Dim BlClosed As Boolean = CheckBoxClosed.Checked
            DateTimePicker2.Visible = BlClosed
            Xl_LookupIncidenciaCodTancament.Visible = BlClosed
            Xl_LookupIncidenciaCodTancament.Clear()
        End If

    End Sub

    Private Sub ButtonCodLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_IncidenciesCods(DTOIncidenciaCod.Cods.averia, DTO.Defaults.SelectionModes.selection)
        AddHandler oFrm.itemSelected, AddressOf OnCodSelection
        oFrm.Show()
    End Sub


    Private Sub OnCodSelection(ByVal sender As System.Object, ByVal e As MatEventArgs)
        SetButtons()
    End Sub

    Private Sub OnTancamentSelection(ByVal sender As System.Object, ByVal e As MatEventArgs)
        SetButtons()
    End Sub


    Private Sub SetButtons()
        Dim BlEnable As Boolean = True
        If (_Incidencia.IsNew And Xl_LookupIncidenciaCodApertura.IncidenciaCod Is Nothing) Then BlEnable = False
        ButtonOk.Enabled = BlEnable
    End Sub


    Private Sub ButtonTancamentLookUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_IncidenciesCods(DTOIncidenciaCod.Cods.tancament, DTO.Defaults.SelectionModes.selection)
        AddHandler oFrm.itemSelected, AddressOf OnTancamentSelection
        oFrm.Show()
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB.Incidencia.Delete(exs, _Incidencia) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            MsgBox("incidencia " & _Incidencia.AsinAndNum() & " eliminada", MsgBoxStyle.Information, "MAT.NET")
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al eliminar la incidencia")
        End If
    End Sub

    Private Sub ReadIncidenciaFromForm()

        With _Incidencia
            .Fch = DateTimePicker1.Value
            .Description = TextBoxObs.Text

            Dim oType As DTOIncidencia.ContactTypes = IIf(RadioButtonProfesional.Checked, DTOIncidencia.ContactTypes.professional, DTOIncidencia.ContactTypes.consumidor)
            .ContactType = oType
            If oType = DTOIncidencia.ContactTypes.professional Then
                If Xl_Contact21.Contact Is Nothing Then
                    .Customer = Nothing
                Else
                    .Customer = Xl_Contact21.Customer
                End If
            End If

            .Codi = Xl_LookupIncidenciaCodApertura.IncidenciaCod
            .Tancament = Xl_LookupIncidenciaCodTancament.IncidenciaCod

            .Product = Xl_LookupProduct1.Product

            .SerialNumber = TextBoxSerialNumber.Text
            .ManufactureDate = TextBoxManufactureDate.Text
            .ContactPerson = TextBoxPerson.Text
            .Tel = TextBoxTel.Text
            .EmailAdr = TextBoxEmail.Text
            .CustomerRef = TextBoxRef.Text

            .Procedencia = ComboBoxProcedencia.SelectedIndex
            .FchCompra = DateTimePickerFchCompra.Value
            .BoughtFrom = TextBoxBoughtWhere.Text
            .Description = TextBoxObs.Text
            If CheckBoxClosed.Checked Then
                .FchClose = DateTimePicker2.Value
            Else
                .FchClose = DateTime.MinValue
            End If

            .DocFileImages = Xl_IncidenciaDocFilesImgs.Values.Where(Function(x) Not x.IsVideo).ToList()
            .Videos = Xl_IncidenciaDocFilesImgs.Values.Where(Function(x) x.IsVideo).ToList()
            .PurchaseTickets = Xl_IncidenciaDocFilesTicket.Values

        End With
    End Sub


    Private Async Sub ButtonSpv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSpv.Click

        If Xl_LookupProduct1.Product Is Nothing And _Incidencia.src = DTOIncidencia.Srcs.Producte Then
            MsgBox("falta posar el producte", MsgBoxStyle.Exclamation, "MAT.NET")
        Else

            ReadIncidenciaFromForm()

            _Incidencia.FchClose = DTO.GlobalVariables.Today()
            _Incidencia.Tancament = DTOIncidenciaCod.Wellknown(DTOIncidenciaCod.Cods.averia)
            Dim IsNew As Boolean = _Incidencia.IsNew
            Dim exs As New List(Of Exception)

            _Incidencia.UsrLog.usrLastEdited = Current.Session.User

            UIHelper.ToggleProggressBar(Panel1, True)
            _Incidencia = Await FEB.Incidencia.Update(exs, _Incidencia)
            UIHelper.ToggleProggressBar(Panel1, False)
            If exs.Count = 0 Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Incidencia))
                If IsNew Then
                    MsgBox("Número de incidencia: " & _Incidencia.AsinAndNum(), MsgBoxStyle.Information, "MAT.NET")
                End If

                Dim oSpv As DTOSpv = DTOSpv.Factory(Current.Session.User)
                With oSpv
                    .incidencia = _Incidencia
                    .customer = _Incidencia.Customer
                    .nom = _Incidencia.Customer.nomComercialOrDefault
                    .address = _Incidencia.Customer.Address
                    .tel = _Incidencia.Tel
                    .contacto = _Incidencia.ContactPerson
                    .sRef = "incidencia num. " & _Incidencia.AsinOrNum()
                    .product = _Incidencia.Product
                    .serialNumber = _Incidencia.SerialNumber
                    .ManufactureDate = _Incidencia.ManufactureDate
                    .sRef = _Incidencia.CustomerRef
                    .obsClient = _Incidencia.Description
                End With

                Dim oFrm As New Frm_Spv(oSpv)
                oFrm.Show()
                Me.Close()
            Else
                MsgBox("error al desar la incidencia" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
            End If


        End If
    End Sub

    Private Async Sub ButtonNewPdc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNewPdc.Click
        If Xl_LookupIncidenciaCodTancament.IncidenciaCod Is Nothing Then
            MsgBox("falta posar el codi de tancament de la incidencia", MsgBoxStyle.Exclamation, "MAT.NET")
        ElseIf Xl_LookupProduct1.Product Is Nothing And _Incidencia.src = DTOIncidencia.Srcs.Producte Then
            MsgBox("falta posar el producte", MsgBoxStyle.Exclamation, "MAT.NET")
        Else

            ReadIncidenciaFromForm()

            If (Xl_LookupIncidenciaCodTancament.IncidenciaCod.reposicionParcial Or Xl_LookupIncidenciaCodTancament.IncidenciaCod.reposicionTotal) Then
                With _Incidencia
                    .FchClose = DTO.GlobalVariables.Today()
                End With

                Dim IsNew As Boolean = (_Incidencia.IsNew)
                Dim exs As New List(Of Exception)

                _Incidencia.UsrLog.usrLastEdited = Current.Session.User
                _Incidencia = Await FEB.Incidencia.Update(exs, _Incidencia)
                If exs.Count = 0 Then
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_Incidencia))
                    If IsNew Then
                        MsgBox("Número de incidencia: " & _Incidencia.AsinAndNum(), MsgBoxStyle.Information, "MAT.NET")
                    End If

                    Dim oCustomer = Await FEB.Customer.Find(exs, Xl_Contact21.Contact.Guid)
                    FEB.Contact.Load(oCustomer, exs)
                    Dim oPurchaseOrder = DTOPurchaseOrder.Factory(oCustomer, Current.Session.User, DTO.GlobalVariables.Today())
                    With oPurchaseOrder
                        .Concept = "incidencia " & _Incidencia.AsinOrNum()
                        .Source = DTOPurchaseOrder.Sources.no_Especificado

                        If Xl_LookupIncidenciaCodTancament.IncidenciaCod.reposicionTotal AndAlso Xl_LookupProduct1.Product.SourceCod = DTOProduct.SourceCods.Sku Then
                            .addItem(Xl_LookupProduct1.Product, 1)
                        End If
                    End With
                    If Await FEB.AlbBloqueig.BloqueigStart(Current.Session.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                        oFrm.Show()
                        Me.Close()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Else
                    MsgBox("error al desar la incidencia" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
                End If
            Else
                MsgBox("El codi de tancament de la incidencia no sembla l'adequat per resoldre-ho amb una comanda", MsgBoxStyle.Exclamation, "MAT.NET")
            End If

        End If
    End Sub


    Private Sub ButtonCancel_Click1(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub Xl_LookupProduct1_RequestToLookup(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.RequestToLookup
        Dim exs As New List(Of Exception)
        ReadIncidenciaFromForm()

        Dim oCustomCatalog As List(Of DTOProductBrand) = Nothing
        Select Case _Incidencia.Procedencia
            Case DTOIncidencia.Procedencias.myShop, DTOIncidencia.Procedencias.expo
                Dim oCatalog As DTOCatalog = Await FEB.Incidencia.Catalog(exs, _Incidencia.Procedencia, _Incidencia.Customer)
                oCustomCatalog = oCatalog.toProductBrands()
        End Select

        If exs.Count = 0 Then
            Dim oProduct As DTOProduct = Nothing
            If Xl_LookupProduct1.Product Is Nothing Then
                Dim sGuid As String = UIHelper.GetSetting(DTOSession.Settings.Last_Product_Selected)
                If GuidHelper.IsGuid(sGuid) Then
                    oProduct = Await FEB.Product.Find(exs, New Guid(sGuid))
                End If
            Else
                oProduct = Xl_LookupProduct1.Product
            End If

            Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectAny, oProduct, IncludeObsoletos:=(oCustomCatalog IsNot Nothing), oCustomCatalog:=oCustomCatalog)
            AddHandler oFrm.OnItemSelected, AddressOf onProductSelected
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub onProductSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim oProducts As New List(Of DTOProduct)
        If e.Argument IsNot Nothing Then oProducts.Add(e.Argument)
        Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
        UIHelper.SaveSetting(DTOSession.Settings.Last_Product_Selected, Xl_LookupProduct1.Product.Guid.ToString())
        SetButtons()
    End Sub

    Private Async Sub RecollidaPerFabricantToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecollidaPerFabricantToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSatRecall = Await FEB.SatRecall.fromIncidencia(exs, _Incidencia)
        If oSatRecall Is Nothing Then
            Dim oProduct As DTOProduct = _Incidencia.Product
            Select Case oProduct.sourceCod
                Case DTOProduct.SourceCods.Sku
                    oSatRecall = DTOSatRecall.Factory(_Incidencia)
                Case Else
                    exs.Add(New Exception("Cal especificar a la incidencia el producte concret a recullir"))
            End Select
        End If

        If exs.Count = 0 Then
            Dim oFrm As New Frm_SatRecall(oSatRecall)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonAddTracking_Click(sender As Object, e As EventArgs) Handles ButtonAddTracking.Click
        'Dim oCod = ComboBoxTrackings.SelectedItem
        Dim oTracking As New DTOTracking
        With oTracking
            .Cod = ComboBoxTrackings.SelectedItem
            .UsrLog = DTOUsrLog2.Factory(Current.Session.User)
            .Target = _Incidencia.ToGuidNom
        End With
        Dim exs As New List(Of Exception)
        If Await FEB.Tracking.Update(exs, oTracking) Then
            Dim oIncidencia = Await FEB.Incidencia.Find(exs, _Incidencia.Guid)
            If exs.Count = 0 Then
                Xl_Trackings1.Load(oIncidencia.Trackings)
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Xl_Trackings1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Trackings1.RequestToRefresh
        Dim exs As New List(Of Exception)
        Dim oTrackings = Await FEB.Incidencia.Trackings(exs, _Incidencia)
        If exs.Count = 0 Then
            Xl_Trackings1.Load(oTrackings)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub CopyAsinToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyAsinToolStripMenuItem.Click
        UIHelper.CopyToClipboard(_Incidencia.Asin)
    End Sub
End Class