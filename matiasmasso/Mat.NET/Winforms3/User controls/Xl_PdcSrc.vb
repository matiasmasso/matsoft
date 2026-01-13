Public Class Xl_PdcSrc

    Private _Src As DTOPurchaseOrder.Sources = DTOPurchaseOrder.Sources.no_Especificado

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(value As DTOPurchaseOrder.Sources)
        _Src = value
        PictureBox1.Image = IconHelper.PurchaseSrcIcon(_Src)
    End Sub

    Public ReadOnly Property Source() As DTOPurchaseOrder.Sources
        Get
            Return _Src
        End Get
    End Property

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        Dim oFrm As New Frm_PdcSrcs(_Src)
        AddHandler oFrm.ItemSelected, AddressOf ItemSelected
        oFrm.Show()
    End Sub

    Private Sub ItemSelected(sender As Object, e As MatEventArgs)
        Dim oSrc As DTOPurchaseOrder.Sources = e.Argument
        Load(oSrc)
        RaiseEvent AfterUpdate(oSrc, New MatEventArgs(oSrc))
    End Sub

End Class
