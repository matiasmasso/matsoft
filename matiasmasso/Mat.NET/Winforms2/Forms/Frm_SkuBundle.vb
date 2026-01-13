Public Class Frm_SkuBundle
    Private _ProductSku As DTOProductSku
    Private _DefaultUrl As String
    Private _CleanTab(20) As Boolean

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToRefreshLangTexts(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Accessories
        Spares
        'Downloads
        'MediaResources
        'Movies
        'Blogs
    End Enum

    Public Sub New(value As DTOProductSku)
        MyBase.New()
        Me.InitializeComponent()
        _ProductSku = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.ProductSku.Load(_ProductSku, exs) Then
            With _ProductSku
                If .IsNew Then
                    Me.Text = "(nou Bundle)"
                Else
                    Me.Text = String.Format("Bundle {0}: {1}", .id, .nomLlarg.Tradueix(Current.Session.Lang))
                End If
                _DefaultUrl = .defaultUrl
                Xl_LookupProductCategory1.Load(.Category)
                RefrescaLangTexts()
                If .Ean13 IsNot Nothing Then
                    TextBoxEan.Text = .Ean13.Value
                End If
                TextBoxRefProveidor.Text = .refProveidor
                TextBoxNomProveidor.Text = .nomProveidor
                TextBoxUrl.Text = .UrlOrDefault
                Xl_Percent1.Load(.bundleDto, 5)
                Xl_BundleSkus1.Load(.bundleSkus)
                CheckBoxObsoleto.Checked = .obsoleto
                If .FchObsoleto > DateTimePickerFchObsolet.MinDate Then
                    DateTimePickerFchObsolet.Value = .FchObsoleto
                    If .ObsoletoConfirmed > DateTimePickerFchObsolet.MinDate Then
                        CheckBoxObsoletoConfirmed.Checked = True
                        DateTimePickerFchObsolet.Value = .ObsoletoConfirmed
                    End If
                End If

                refrescaTotals()

                Dim oImageBytes = Await FEB.ProductSku.Image(exs, _ProductSku)
                If exs.Count = 0 Then
                    Xl_Image1.Load(oImageBytes, 700, 800, "imatge de producte")
                Else
                    UIHelper.WarnError(exs, "Error al descarregar la imatge")
                End If

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNomCurt.TextChanged,
         TextBoxNomLlarg.TextChanged,
          TextBoxEan.TextChanged,
           TextBoxNomProveidor.TextChanged,
            TextBoxUrl.TextChanged,
             Xl_Percent1.AfterUpdate,
              Xl_Image1.AfterUpdate,
               CheckBoxObsoleto.CheckedChanged,
                DateTimePickerFchObsolet.ValueChanged,
                 DateTimePickerObsoletoConfirmed.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _ProductSku
            .nomLlarg.Esp = TextBoxNomLlarg.Text
            .category = Xl_LookupProductCategory1.Category
            .Nom.Esp = TextBoxNomCurt.Text
            If TextBoxEan.Text = "" Then
                .ean13 = Nothing
            Else
                .ean13 = New DTOEan(TextBoxEan.Text)
            End If

            If TextBoxUrl.Text > "" And TextBoxUrl.Text <> .DefaultUrl Then
                .url = TextBoxUrl.Text
            End If

            .refProveidor = TextBoxRefProveidor.Text
            .nomProveidor = TextBoxNomProveidor.Text
            .bundleDto = Xl_Percent1.Value
            .BundleSkus = Xl_BundleSkus1.Values.Where(Function(x) x.Qty > 0).ToList
            Using ms As New IO.MemoryStream
                Xl_Image1.Bitmap.Save(ms, Imaging.ImageFormat.Jpeg)
                .Image = ms.ToArray()
            End Using
            .obsoleto = CheckBoxObsoleto.Checked
            If CheckBoxObsoleto.Checked Then
                .FchObsoleto = DateTimePickerFchObsolet.Value
                If CheckBoxObsoletoConfirmed.Checked Then
                    .ObsoletoConfirmed = DateTimePickerObsoletoConfirmed.Value
                Else
                    .ObsoletoConfirmed = Nothing
                End If
            Else
                .FchObsoleto = Nothing
                .Substitute = Nothing
                .ObsoletoConfirmed = Nothing
            End If
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.ProductSku.Update(_ProductSku, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductSku))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest bundle?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.ProductSku.Delete(_ProductSku, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductSku))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar el bundle")
            End If
        End If
    End Sub

    Private Sub refrescaTotals()
        Dim oBundleSkus = Xl_BundleSkus1.Values
        Dim oPvpBrut = DTOAmt.Factory(oBundleSkus.Where(Function(x) x.Sku.rrpp IsNot Nothing).Sum(Function(y) y.Qty * y.Sku.rrpp.Eur))
        Dim oPvpNet = oPvpBrut.DeductPercent(Xl_Percent1.Value)
        TextBoxPvp.Text = oPvpNet.Formatted
        ButtonOk.Enabled = True
    End Sub

    Private Sub RefrescaLangTexts()
        TextBoxNomCurt.Text = _ProductSku.Nom.Tradueix(Current.Session.Lang)
        TextBoxNomLlarg.Text = _ProductSku.NomLlarg.Tradueix(Current.Session.Lang)
        RaiseEvent RequestToRefreshLangTexts(Me, New MatEventArgs(_ProductSku))
    End Sub

    Private Sub RefrescaLangTexts(sender As Object, e As MatEventArgs)
        _ProductSku = e.Argument
        RefrescaLangTexts()
    End Sub

    Private Sub Xl_BundleSkus1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_BundleSkus1.AfterUpdate
        refrescaTotals()
    End Sub


    Private Sub Xl_Percent1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Percent1.AfterUpdate
        If _AllowEvents Then
            refrescaTotals()
        End If
    End Sub

    Private Sub ButtonNavigate_Click(sender As Object, e As EventArgs) Handles ButtonNavigate.Click
        Process.Start(TextBoxUrl.Text)
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Dim iTab As Tabs = TabControl1.SelectedIndex
        If Not _CleanTab(iTab) Then
            Select Case iTab
                Case Tabs.Accessories
                    Await LoadAccessories()
                Case Tabs.Spares
                    Await LoadSpares()
            End Select
            _CleanTab(iTab) = True
        End If
    End Sub

    Private Async Function LoadAccessories() As Task
        Dim exs As New List(Of Exception)
        Dim oAccessories = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Accessories, _ProductSku, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then
            Xl_ProductSkusExtendedAccessories.Load(oAccessories,,, DTOProduct.Relateds.Accessories)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Function LoadSpares() As Task
        Dim exs As New List(Of Exception)
        Dim oSpares = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Spares, _ProductSku, GlobalVariables.Emp.Mgz)
        If exs.Count = 0 Then
            Xl_ProductSkusExtendedSpares.Load(oSpares,,, DTOProduct.Relateds.Spares)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Sub Xl_ProductSkusExtendedAccessories_RequestToAddNewSku(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedAccessories.RequestToAddNewSku
        Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectSku, _ProductSku.category)
        AddHandler oFrm.OnItemSelected, AddressOf onAccessorySelected
        oFrm.Show()
    End Sub

    Private Async Sub onAccessorySelected(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oAccessories = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Accessories, _ProductSku, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oAccessory As DTOProductSku = e.Argument
            If oAccessories.Exists(Function(x) x.Equals(oAccessory)) Then
                MsgBox(oAccessory.nomLlarg.Tradueix(Current.Session.Lang) & " ja está registrat com a accessori de " & _ProductSku.nom.Tradueix(Current.Session.Lang))
            Else
                oAccessories.Add(e.Argument)
                If Await FEB.Product.UpdateRelateds(exs, DTOProduct.Relateds.Accessories, _ProductSku, oAccessories) Then
                    Await LoadAccessories()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductSkusExtendedSpares_RequestToAddNewSku(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedSpares.RequestToAddNewSku
        Dim exs As New List(Of Exception)
        Dim oSpares = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Spares, _ProductSku, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oSpare As DTOProductSku = e.Argument
            If oSpares.Exists(Function(x) x.Equals(oSpare)) Then
                MsgBox(oSpare.nomLlarg.Tradueix(Current.Session.Lang) & " ja está registrat com a recanvi de " & _ProductSku.nom.Tradueix(Current.Session.Lang))
            Else
                oSpares.Add(e.Argument)
                If Await FEB.Product.UpdateRelateds(exs, DTOProduct.Relateds.Spares, _ProductSku, oSpares) Then
                    Await LoadSpares()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductSkusExtendedAccessories_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedAccessories.RequestToRemove
        Dim exs As New List(Of Exception)
        Dim oAccessories = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Accessories, _ProductSku, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oAccessory As DTOProductSku = e.Argument
            If oAccessories.Exists(Function(x) x.Equals(oAccessory)) Then
                oAccessories.RemoveAll(Function(x) x.Equals(oAccessory))
                If Await FEB.Product.UpdateRelateds(exs, DTOProduct.Relateds.Accessories, _ProductSku, oAccessories) Then
                    Await LoadAccessories()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                MsgBox(oAccessory.nomLlarg.Tradueix(Current.Session.Lang) & " no estava registrat com a accessori de " & _ProductSku.nom.Tradueix(Current.Session.Lang))
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductSkusExtendedSpares_RequestToRemove(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtendedSpares.RequestToRemove
        Dim exs As New List(Of Exception)
        Dim oSpares = Await FEB.Product.Relateds(exs, DTOProduct.Relateds.Spares, _ProductSku, GlobalVariables.Emp.Mgz, True, False)
        If exs.Count = 0 Then
            Dim oSpare As DTOProductSku = e.Argument
            If oSpares.Exists(Function(x) x.Equals(oSpare)) Then
                oSpares.RemoveAll(Function(x) x.Equals(oSpare))
                If Await FEB.Product.UpdateRelateds(exs, DTOProduct.Relateds.Spares, _ProductSku, oSpares) Then
                    Await LoadSpares()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                MsgBox(oSpare.nomLlarg.Tradueix(Current.Session.Lang) & " no estava registrat com a recanvi de " & _ProductSku.nom.Tradueix(Current.Session.Lang))
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonShowLangTexts_Click(sender As Object, e As EventArgs) Handles ButtonNomCurt.Click, ButtonNomLlarg.Click
        Dim oFrm As New Frm_ProductDescription(_ProductSku)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaLangTexts
        oFrm.Show()
    End Sub


End Class


