Public Class DTOCarrefourItem
    Property SupplierCode As String = "MATIA"
    Property Section As Integer = 61 'seccion de bebé
    Property Implantation As String = "610087"
    Property MadeIn As String
    Property SkuDsc As String
    Property SkuCustomRef As String
    Property SkuColor As String
    Property UnitsPerMasterBox As Integer
    Property UnitsPerInnerBox As Integer
    Property MasterBarCode As String
    Property Width As Integer
    Property Height As Integer
    Property Length As Integer
    Property Weight As Integer
    Property Albaran As String
    Property Linea As Integer

    Public Function Dimensions() As String
        Dim retval As String = String.Format("{0} x {1} x {2} cm", _Length / 10, _Width / 10, _Height / 10)
        Return retval
    End Function
End Class
