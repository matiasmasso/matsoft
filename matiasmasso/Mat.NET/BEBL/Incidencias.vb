Public Class Incidencia
    Shared Function Find(oGuid As Guid) As DTOIncidencia
        Dim retval As DTOIncidencia = IncidenciaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromNum(oEmp As DTOEmp, id As String) As DTOIncidencia
        Dim retval As DTOIncidencia = IncidenciaLoader.FromNum(oEmp, id)
        Return retval
    End Function

    Shared Function Trackings(oIncidencia As DTOIncidencia) As List(Of DTOTracking)
        Load(oIncidencia)
        Return oIncidencia.Trackings
    End Function

    Shared Function Sprite(oIncidencia As DTOIncidencia) As Byte()
        Dim oThumbnails = IncidenciaLoader.SpriteImages(oIncidencia)
        Dim retval = LegacyHelper.SpriteBuilder.Factory(oThumbnails)
        Return retval
    End Function

    Shared Function Load(ByRef oIncidencia As DTOIncidencia) As Boolean
        Dim retval As Boolean = IncidenciaLoader.Load(oIncidencia)
        Return retval
    End Function

    Shared Function Update(oIncidencia As DTOIncidencia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim duplicates As Integer = oIncidencia.DocFileImages.GroupBy(Function(x) x.hash).Where(Function(y) y.Count() > 1).Count
        If duplicates = 0 Then
            retval = IncidenciaLoader.Update(oIncidencia, exs)
        Else
            exs.Add(New DuplicateNameException("cal retirar " & duplicates & " imatges duplicades"))
        End If
        Return retval
    End Function

    Shared Function Delete(oIncidencia As DTOIncidencia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = IncidenciaLoader.Delete(oIncidencia, exs)
        Return retval
    End Function

    Shared Function Catalog(oProcedencia As DTOIncidencia.Procedencias, oCustomer As DTOCustomer) As DTOCatalog
        Dim retval As DTOCatalog = Nothing
        Select Case oProcedencia
            Case DTOIncidencia.Procedencias.MyShop, DTOIncidencia.Procedencias.Expo
                Dim oPreviousContacts = BEBL.Contact.PreviousContacts(oCustomer)
                retval = IncidenciaLoader.Catalog(oCustomer.lang, contacts:=oPreviousContacts)
            Case Else
                retval = IncidenciaLoader.Catalog(oCustomer.lang, emp:=oCustomer.emp)
        End Select

        retval.RemoveAll(Function(x) x.Guid.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Varios).Guid))

        Return retval
    End Function


End Class


Public Class Incidencias

    Shared Function Model(oRequest As Models.IncidenciesModel.Request) As Models.IncidenciesModel
        Return IncidenciasLoader.Model(oRequest)
    End Function

    Shared Function Compact(oTitular As DTOBaseGuid, oTitularCod As DTOEnums.TitularCods) As List(Of DTOIncidencia.Compact) ' iMat 3.0
        Return IncidenciasLoader.Compact(oTitular, oTitularCod)
    End Function

    Shared Function Headers(oUser As DTOUser, Optional oContact As DTOContact = Nothing) As List(Of DTOIncidencia)
        Return IncidenciasLoader.Headers(oUser, oContact)
    End Function


    Shared Sub LoadQuery(ByRef oQuery As DTOIncidenciaQuery)
        IncidenciasLoader.LoadQuery(oQuery)
    End Sub

    Shared Function CodisDeTancament() As List(Of DTOIncidenciaCod)
        Dim retval As List(Of DTOIncidenciaCod) = IncidenciasLoader.CodisDeTancament
        Return retval
    End Function

    Shared Function Reposicions(oEmp As DTOEmp, iYea As Integer) As List(Of DTOIncidencia)
        Return IncidenciasLoader.Reposicions(oEmp, iYea)
    End Function

    Shared Function Ratios(FchFrom As Date, FchTo As Date) As List(Of Tuple(Of DTOProductCategory, Integer, Integer))
        Dim retval = IncidenciasLoader.Ratios(FchFrom, FchTo)
        Return retval
    End Function

    Shared Function withVideos() As List(Of Guid) 'to deprecate
        Return IncidenciasLoader.withVideos
    End Function

End Class
