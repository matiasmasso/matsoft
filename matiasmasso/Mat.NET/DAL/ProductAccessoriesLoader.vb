Public Class ProductAccessoriesLoader
    Shared Function Exist(oProduct As DTOProduct, Optional IncludeObsoletos As Boolean = False) As Boolean
        Dim SQL As String = "SELECT productguid FROM ArtSpare WHERE targetguid=@Guid and cod= " & CInt(DTOProduct.Relateds.Accessories) & " "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oProduct.Guid.ToString())
        Dim RetVal As Boolean = oDrd.Read
        oDrd.Close()
        Return RetVal

    End Function
End Class
