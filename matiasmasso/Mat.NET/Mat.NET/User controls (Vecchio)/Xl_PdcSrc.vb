Public Class Xl_PdcSrc

    Private _Src As DTOPurchaseOrder.Sources = DTO.DTOPurchaseOrder.Sources.no_Especificado

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(value As DTOPurchaseOrder.Sources)
        _Src = value
        PictureBox1.Image = BLL.BLLPurchaseOrder.SrcIcon(_Src)
    End Sub

    Public ReadOnly Property Source() As DTOPurchaseOrder.Sources
        Get
            Return _Src
        End Get
    End Property

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        Dim oFrm As New Frm_PdcSrc_Select
        With oFrm
            .Src = _Src
            .ShowDialog()
            Load(.Src)
            RaiseEvent AfterUpdate(.Src, MatEventArgs.Empty)
        End With
    End Sub

End Class
