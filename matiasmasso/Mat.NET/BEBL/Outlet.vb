Public Class Outlet
    Shared Function All(oEmp As DTOEmp, oUser As DTOUser) As List(Of DTOProductSku)
        Dim exs As New List(Of Exception)
        Dim oMgz As DTOMgz = oEmp.Mgz
        Dim oBrands = BEBL.ProductBrands.All(oEmp, oUser)
        Dim retval As List(Of DTOProductSku) = OutletLoader.All(oUser, oBrands, oMgz)

        Select Case oUser.Rol.Id
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                Dim oTarifa = BEBL.CustomerTarifa.Load(oUser)
                For Each oSku As DTOProductSku In retval
                    Dim pSku = oTarifa.FindSku(oSku)
                    If pSku IsNot Nothing Then
                        oSku.Price = pSku.Price.ToAmt()
                    End If
                Next
        End Select
        Return retval
    End Function
End Class
