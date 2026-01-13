Public Class DTOPdf
    Property PageCount As Integer
    Property Size As Size
    <JsonIgnore> Property Portrait As Image
    <JsonIgnore> Property Thumbnail As Image
End Class
