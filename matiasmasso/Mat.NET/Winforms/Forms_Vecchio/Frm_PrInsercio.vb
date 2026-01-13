Public Class Frm_PrInsercio
    Private _Insercio As PrInsercio
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oInsercio As PrInsercio)
        MyBase.New()
        Me.InitializeComponent()

        PrInsercioLoader.Load(oInsercio)
        _Insercio = oInsercio

        With _Insercio
            Xl_PrNumero1.Numero = .Numero
            TextBoxPagina.Text = .Pagina
            ComboBoxSizeCod.SelectedIndex = .SizeCod

            Xl_PrAdDoc1.Revista = _Insercio.Numero.Revista
            If .Document IsNot Nothing Then
                PrAdDocLoader.load(.Document)
                Xl_PrAdDoc1.AdDoc = .Document
            End If

            If .Tarifa Is Nothing Then
                Xl_AmtTarifa.Amt = BLLApp.EmptyAmt
            Else
                Xl_AmtTarifa.Amt = .Tarifa
            End If

            If .Cost Is Nothing Then
                Xl_AmtCost.Amt = BLLApp.EmptyAmt
            Else
                Xl_AmtCost.Amt = .Cost
            End If

            If .OrdreDeCompra IsNot Nothing Then
                ButtonOrdreDeCompra.Enabled = .OrdreDeCompra.Exists
            End If

            If .Cca IsNot Nothing Then
                ButtonCca.Enabled = .Cca.Exists
            End If

            If .DocFile IsNot Nothing Then
                Xl_DocFile1.Load(.DocFile)
            End If

            ButtonDel.Enabled = Not .IsNew

        End With
        _AllowEvents = True
    End Sub

    Public Sub New(ByVal oOrdreDeCompra As PrOrdreDeCompra)
        MyBase.New()
        Me.InitializeComponent()
        Dim oNumero As New PrNumero(oOrdreDeCompra.Revista)
        With Xl_PrNumero1
            .Editorial = oOrdreDeCompra.Editorial
            .Numero = oNumero
        End With
        _AllowEvents = True
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If _Insercio Is Nothing Then
            _Insercio = New PrInsercio(Xl_PrNumero1.Numero)
        Else
            _Insercio.Numero = Xl_PrNumero1.Numero
        End If

        With _Insercio
            If IsNumeric(TextBoxPagina.Text) Then
                .Pagina = TextBoxPagina.Text
            End If
            .Document = Xl_PrAdDoc1.AdDoc
            .SizeCod = ComboBoxSizeCod.SelectedIndex
            If Xl_AmtTarifa.Amt IsNot Nothing Then
                .Tarifa = Xl_AmtTarifa.Amt
            End If
            .Cost = Xl_AmtCost.Amt
            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If

        End With


        Dim exs as New List(Of exception)
        If PrInsercioLoader.Update(_Insercio, exs) Then
            Dim oArgs As New MatEventArgs(_Insercio)
            RaiseEvent AfterUpdate(Me, oArgs)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al registrar la inserció")
        End If
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxPagina.TextChanged, _
    Xl_PrNumero1.AfterUpdate, _
     Xl_PrAdDoc1.AfterUpdate, _
    ComboBoxSizeCod.SelectedIndexChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub Xl_Amt_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     Xl_AmtCost.AfterUpdate, _
      Xl_AmtTarifa.AfterUpdate
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCca.Click
        Dim oFrm As New Frm_Cca(_Insercio.Cca)
        oFrm.Show()
    End Sub


    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        If PrInsercioLoader.Delete(_Insercio, exs) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al eliminar la inserció")
        End If
    End Sub
End Class