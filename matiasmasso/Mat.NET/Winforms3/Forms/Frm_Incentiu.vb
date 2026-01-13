Public Class Frm_Incentiu
    Private _Incentiu As DTOIncentiu
    Private _Orders As List(Of DTOPurchaseOrder)
    Private _Loaded As Boolean
    Private _LoadedProductesVenuts As Boolean

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)


    Private Enum Tabs
        General
        Config
        Esp
        Cat
        Eng
        Por
        Contribucio
        Comandes
        ProductesVenuts
    End Enum


    Public Sub New(value As DTOIncentiu)
        MyBase.New()
        Me.InitializeComponent()
        _Incentiu = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Incentiu.Load(exs, _Incentiu) Then
            With _Incentiu
                Me.Text = _Incentiu.Title.Tradueix(Current.Session.Lang)
                TextBoxTitleEsp.Text = .Title.Esp
                TextBoxTitleCat.Text = .Title.Cat
                TextBoxTitleEng.Text = .Title.Eng
                TextBoxTitlePor.Text = .Title.Por

                TextBoxExcerptEsp.Text = .Excerpt.Esp
                TextBoxExcerptCat.Text = .Excerpt.Cat
                TextBoxExcerptEng.Text = .Excerpt.Eng
                TextBoxExcerptPor.Text = .Excerpt.Por

                TextBoxBasesEsp.Text = .Bases.Esp
                TextBoxBasesCat.Text = .Bases.Cat
                TextBoxBasesEng.Text = .Bases.Eng
                TextBoxBasesPor.Text = .Bases.Por

                TextBoxContribution.Text = .ManufacturerContribution

                If _Incentiu.FchFrom > Date.MinValue Then
                    DateTimePickerFchFrom.Value = .FchFrom
                Else
                    DateTimePickerFchFrom.Value = DTO.GlobalVariables.Today()
                End If
                If _Incentiu.FchTo <> Nothing Then
                    DateTimePickerFchTo.Value = .FchTo
                    DateTimePickerFchTo.Visible = True
                    CheckBoxTo.Checked = True
                End If

                Xl_Image178x125.Bitmap = LegacyHelper.ImageHelper.Converter(.Thumbnail)

                Xl_LookupEvento1.EventoValue = .Evento

                If .Product IsNot Nothing Then
                    FEB.Product.Load(.Product, exs)
                    Dim oProducts As New List(Of DTOProduct)
                    If .Product IsNot Nothing Then oProducts.Add(.Product)
                    Xl_LookupVwProductNom.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
                End If

                CheckBoxOnlyInStock.Checked = .OnlyInStk
                CheckBoxCliVisible.Checked = .CliVisible
                CheckBoxRepVisible.Checked = .RepVisible
                RadioButtonDto.Checked = (.Cod = DTOIncentiu.Cods.Dto)
                RadioButtonFreeUnits.Checked = (.Cod = DTOIncentiu.Cods.FreeUnits)
                EnableLists()

                Xl_Products1.Load(_Incentiu.Products)
                Xl_QtyDtos1.Load(_Incentiu.QtyDtos, CurrentCod)

                Dim oAllChannels = Await FEB.DistributionChannels.Headers(GlobalVariables.Emp, Current.Session.Lang, exs)
                If exs.Count = 0 Then
                    Xl_DistributionChannels_Checklist1.Load(oAllChannels, _Incentiu.Channels)

                    ButtonOk.Enabled = .IsNew
                    ButtonDel.Enabled = Not .IsNew

                    Select Case Current.Session.Rol.Id
                        Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.Marketing, DTORol.Ids.Accounts
                        Case Else
                            TabPageContribucions.Enabled = False
                    End Select
                Else
                    UIHelper.WarnError(exs)
                End If
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub EnableLists()
        Dim BlEnabled As Boolean = (RadioButtonDto.Checked Or RadioButtonFreeUnits.Checked)
        GroupBoxProducts.Enabled = BlEnabled
        GroupBoxQtyDtos.Enabled = BlEnabled

        If BlEnabled Then
            Dim BlDtos As Boolean = RadioButtonDto.Checked
            GroupBoxQtyDtos.Text = IIf(BlDtos, "descomptes", "quantitats")
            LabelQty.Text = IIf(BlDtos, "quantitat", "amb carrec")
            LabelDto.Text = IIf(BlDtos, "descompte", "s/carrec")
            Xl_TextBoxNumDto.Mat_CustomFormat = IIf(BlDtos, Xl_TextBoxNum.Formats.percent, Xl_TextBoxNum.Formats.Numeric)

            'If DataGridViewDtos.Columns.Count > ColsDTO Then
            ' Dim sFormat As String = IIf(BlDtos, "#\%;-#\%;#", "#")
            'DataGridViewDtos.Columns(ColsDTO).DefaultCellStyle.Format = sFormat
            ' End If
        End If
    End Sub

    Private Function CurrentCod() As DTOIncentiu.Cods
        Dim oCod As DTOIncentiu.Cods = DTOIncentiu.Cods.NotSet
        If RadioButtonDto.Checked Then
            oCod = DTOIncentiu.Cods.Dto
        ElseIf RadioButtonFreeUnits.Checked Then
            oCod = DTOIncentiu.Cods.FreeUnits
        End If
        Return oCod
    End Function

    Private Sub SetButtons()
        ButtonOk.Enabled = True
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Incentiu
            .Title.esp = TextBoxTitleEsp.Text
            .Title.cat = TextBoxTitleCat.Text
            .Title.eng = TextBoxTitleEng.Text
            .Title.por = TextBoxTitlePor.Text

            .Excerpt.esp = TextBoxExcerptEsp.Text
            .Excerpt.cat = TextBoxExcerptCat.Text
            .Excerpt.eng = TextBoxExcerptEng.Text
            .Excerpt.por = TextBoxExcerptPor.Text

            .Bases.esp = TextBoxBasesEsp.Text
            .Bases.cat = TextBoxBasesCat.Text
            .Bases.eng = TextBoxBasesEng.Text
            .Bases.por = TextBoxBasesPor.Text

            .ManufacturerContribution = TextBoxContribution.Text

            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If

            .Thumbnail = LegacyHelper.ImageHelper.Converter(Xl_Image178x125.Bitmap)
            .Evento = Xl_LookupEvento1.EventoValue
            .OnlyInStk = CheckBoxOnlyInStock.Checked
            .Product = Xl_LookupVwProductNom.Product
            .Products = Xl_Products1.Values
            .QtyDtos = Xl_QtyDtos1.Values
            .Channels = Xl_DistributionChannels_Checklist1.SelectedValues
            .Cod = CurrentCod()
            .CliVisible = CheckBoxCliVisible.Checked
            .RepVisible = CheckBoxRepVisible.Checked

            Dim exs As New List(Of Exception)
            If Await FEB.Incentiu.Update(exs, _Incentiu) Then
                RaiseEvent AfterUpdate(sender, New MatEventArgs(_Incentiu))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError("fallo al grabar incentiu")
            End If
        End With
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem de la tabla?", MsgBoxStyle.OkCancel, "INCENTIUS")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Incentiu.Delete(exs, _Incentiu) Then
                MsgBox("eliminat", MsgBoxStyle.Information, "INCENTIUS")
                RaiseEvent AfterUpdate(sender, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "INCENTIUS")
        End If
    End Sub

    Private Sub TextBoxNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxTitleEsp.TextChanged,
         TextBoxTitleCat.TextChanged,
          TextBoxTitleEng.TextChanged,
           TextBoxTitlePor.TextChanged,
            TextBoxExcerptEsp.TextChanged,
             TextBoxExcerptCat.TextChanged,
              TextBoxExcerptEng.TextChanged,
               TextBoxExcerptPor.TextChanged,
                TextBoxBasesEsp.TextChanged,
                 TextBoxBasesCat.TextChanged,
                  TextBoxBasesEng.TextChanged,
                   TextBoxBasesEng.TextChanged,
                    DateTimePickerFchFrom.ValueChanged,
                     DateTimePickerFchTo.ValueChanged,
                      CheckBoxOnlyInStock.CheckedChanged,
                       CheckBoxTo.CheckedChanged,
                        CheckBoxRepVisible.CheckedChanged,
                        CheckBoxCliVisible.CheckedChanged,
                         Xl_LookupEvento1.AfterUpdate,
                          Xl_LookupProduct1.AfterUpdate,
                           Xl_LookupVwProductNom.AfterUpdate,
                            Xl_Image178x125.AfterUpdate,
                             ButtonAddProduct.Click,
                              ButtonAddQtyDto.Click,
                               Xl_DistributionChannels_Checklist1.AfterUpdate,
                                TextBoxContribution.TextChanged

        If _AllowEvents Then
            SetButtons()
        End If
    End Sub


    Private Sub Xl_LookupProduct1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
        ButtonAddProduct.Enabled = True
    End Sub

    Private Sub ButtonAddProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddProduct.Click
        Dim oProducts As List(Of DTOProduct) = Xl_Products1.Values
        oProducts.Add(Xl_LookupProduct1.Product)
        Xl_Products1.Load(oProducts)
        Xl_LookupProduct1.Clear()
        ButtonAddProduct.Enabled = False
    End Sub

    Private Sub ButtonAddQtyDto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddQtyDto.Click
        Dim oQtyDtos As List(Of DTOQtyDto) = Xl_QtyDtos1.Values
        Dim item As New DTOQtyDto
        With item
            .Qty = Xl_TextBoxNumQty.Value
            If CurrentCod() = DTOIncentiu.Cods.Dto Then
                .Dto = Xl_TextBoxNumDto.Value
            Else
                .FreeUnits = Xl_TextBoxNumDto.Value
            End If
        End With
        oQtyDtos.Add(item)
        Xl_QtyDtos1.Load(oQtyDtos, CurrentCod)
        Xl_TextBoxNumQty.Clear()
        Xl_TextBoxNumDto.Clear()
    End Sub

    Private Sub Xl_TextBoxNumQtyDto_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_TextBoxNumQty.AfterUpdate, Xl_TextBoxNumDto.AfterUpdate
        If _AllowEvents Then
            If Xl_TextBoxNumDto.Value <> 0 And Xl_TextBoxNumQty.Value <> 0 Then
                ButtonAddQtyDto.Enabled = True
            End If
        End If
    End Sub

    Private Sub CheckBoxTo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxTo.CheckedChanged
        DateTimePickerFchTo.Visible = CheckBoxTo.Checked
    End Sub


    Private Sub RadioButtonQtyDto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        RadioButtonDto.CheckedChanged, _
         RadioButtonFreeUnits.CheckedChanged

        If _AllowEvents Then
            EnableLists()

            Dim oQtyDtos As List(Of DTOQtyDto) = Xl_QtyDtos1.Values
            Xl_QtyDtos1.Load(oQtyDtos, CurrentCod)

        End If
    End Sub

    Private Sub Xl_LookupProduct1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        If _AllowEvents Then
            ButtonAddProduct.Enabled = True
        End If
    End Sub

    Private Sub Xl_PromosPerRep1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_PromosPerRep1.ValueChanged
        If _AllowEvents Then
            Dim oRep As DTORep = e.Argument
            Dim oOrders As List(Of DTOPurchaseOrder)
            If oRep.Guid.Equals(Guid.Empty) Then
                oOrders = _Orders
            Else
                oOrders = _Orders.Where(Function(x) x.Items.Any(Function(y) MatchesRep(y.RepCom, oRep))).ToList
            End If
            Xl_PurchaseOrders1.Load(oOrders)
        End If
    End Sub

    Private Function MatchesRep(oRepCom As DTORepCom, oRep As DTORep) As Boolean
        Dim retval As Boolean
        If oRepCom IsNot Nothing Then
            If oRepCom.Rep.Equals(oRep) Then
                retval = True
            End If
        End If
        Return retval
    End Function

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case DirectCast(TabControl1.SelectedIndex, Tabs)
            Case Tabs.General
            Case Tabs.Comandes
                If Not _Loaded Then
                    _AllowEvents = False
                    _Orders = Await FEB.Incentiu.PurchaseOrders(exs, _Incentiu, Current.Session.User)
                    If exs.Count = 0 Then
                        Await Xl_PromosPerRep1.Load(_Orders)
                        Xl_PurchaseOrders1.Load(_Orders)
                        _AllowEvents = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Tabs.ProductesVenuts
                If Not _LoadedProductesVenuts Then
                    _AllowEvents = False
                    Dim items = Await FEB.Incentiu.PncProducts(exs, _Incentiu)
                    If exs.Count = 0 Then
                        Xl_ProductSkuQtyEurs1.Load(items)
                        _AllowEvents = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub

    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        e.Cancel = Not e.TabPage.Enabled
    End Sub

    Private Async Sub ExcelResultatsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelResultatsToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet = Await FEB.Incentiu.ExcelResults(exs, _Incentiu)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ExcelAdrecesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelAdrecesToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        Dim oSheet = Await FEB.Incentiu.ExcelDeliveryAddresses(exs, _Incentiu, Current.Session.User)
        UIHelper.ToggleProggressBar(Panel1, False)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub
End Class