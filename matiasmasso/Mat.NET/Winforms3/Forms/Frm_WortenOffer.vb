Public Class Frm_WortenOffer

    Private _Offer As DTO.Integracions.Worten.Offer
    Private _Cache As Models.ClientCache
    Private _DirtyProduct As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTO.Integracions.Worten.Offer)
        MyBase.New()
        Me.InitializeComponent()
        _Offer = value
    End Sub

    Private Async Sub Frm_Offer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)

        If _Offer.product_sku <> Nothing Then
            _Cache = Await FEB.Cache.Fetch(exs, Current.Session.User)
            If exs.Count = 0 Then
                With _Offer
                    Dim oProducts As New List(Of DTOProduct)
                    Dim oSku = _Cache.FindSku(.product_sku)
                    If oSku IsNot Nothing Then oProducts.Add(oSku)
                    Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectSku)
                    TextBox_Ean.Text = .Ean()
                    Xl_Amount_total_price.Amt = DTOAmt.Factory(.total_price)
                    NumericUpDown_quantity.Value = .quantity
                End With
            Else
                UIHelper.WarnError(exs)
                Me.Close()
            End If
        End If

        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_Amount_total_price.AfterUpdate,
          NumericUpDown_quantity.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_LookupProduct1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        If _AllowEvents Then
            Dim oSku As DTOProductSku = Xl_LookupProduct1.Product
            TextBox_Ean.Text = DTOEan.eanValue(oSku.Ean13)
            _DirtyProduct = True
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Try
            With _Offer
                If _DirtyProduct Then
                    If Xl_LookupProduct1.Product Is Nothing Then Throw New System.Exception("Producte desconegut")
                    Dim oSku As DTOProductSku = Xl_LookupProduct1.Product
                    If oSku Is Nothing Then Throw New System.Exception("Producte desconegut")
                    .product_sku = oSku.Guid
                    .product_title = oSku.NomLlarg.Tradueix(DTOLang.POR)
                    .product_references = New List(Of Integracions.Worten.Product_Reference)
                    .AddProductReference("EAN", DTOEan.eanValue(oSku.Ean13))
                End If

                .total_price = Xl_Amount_total_price.Amt.Eur
                .quantity = NumericUpDown_quantity.Value
            End With

            UIHelper.ToggleProggressBar(PanelButtons, True)
            Dim oResult As DTO.Integracions.Worten.ImportResult = Await FEB.Worten.Update(exs, _Offer)
            If exs.Count = 0 Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Offer))
                MsgBox(String.Format("Producte actualitzat correctament" & vbCrLf & "id: {0}", oResult.import_id), MsgBoxStyle.Information)
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al desar")
            End If

        Catch ex As Exception
            UIHelper.WarnError(ex)
        End Try
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            'If Await FEB.Offer.Delete(exs, _Offer) Then
            ' RaiseEvent AfterUpdate(Me, New MatEventArgs(_Offer))
            ' Me.Close()
            ' Else
            '     UIHelper.ToggleProggressBar(PanelButtons, False)
            '     UIHelper.WarnError(exs, "error al eliminar")
            ' End If
        End If
    End Sub


End Class


