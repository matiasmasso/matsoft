Public Class SkuStocks

    Shared Function ForWeb(oUser As DTOUser, exs As List(Of Exception)) As DTOProductCatalog
        Dim retval As DTOProductCatalog = New DTOProductCatalog
        Dim oMgz As DTOMgz = oUser.Emp.Mgz
        Select Case oUser.Rol.id
            Case DTORol.Ids.comercial, DTORol.Ids.rep
                retval = SkuStocksLoader.ForRep(oUser, oMgz)
            Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                retval = SkuStocksLoader.ForWeb(oUser, oMgz)
            Case DTORol.Ids.manufacturer
                retval = SkuStocksLoader.ForManufacturer(oUser, oMgz)
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.operadora
                retval = SkuStocksLoader.ForStaff(oUser, oMgz)

        End Select
        Return retval
    End Function

    Shared Function ForApi(oEmp As DTOEmp, oBrand As DTOProductBrand, oUser As DTOUser) As List(Of DTOProductCategory)
        Dim oMgz As DTOMgz = oEmp.Mgz
        Dim retval As List(Of DTOProductCategory) = SkuStocksLoader.forApi(oBrand, oMgz, oUser)
        Return retval
    End Function

    Shared Function StockMovementsExcelSheet(oMgz As DTOMgz, oUser As DTOUser, year As Integer) As MatHelper.Excel.Sheet
        Return SkuStocksLoader.StockMovementsExcelSheet(oMgz, oUser, year)
    End Function

End Class
