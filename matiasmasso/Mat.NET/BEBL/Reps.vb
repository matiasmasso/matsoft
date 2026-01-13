Public Class Rep

#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTORep
        Dim retval As DTORep = RepLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oRep As DTORep) As Boolean
        Dim retval As Boolean = RepLoader.Load(oRep)
        Return retval
    End Function

    Shared Function Exists(oContact As DTOContact) As Boolean
        Dim retval As Boolean = RepLoader.Exists(oContact)
        Return retval
    End Function

    Shared Function Update(oRep As DTORep, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepLoader.Update(oRep, exs)
        Return retval
    End Function

    Shared Function Delete(oRep As DTORep, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepLoader.Delete(oRep, exs)
        Return retval
    End Function


#End Region

    Shared Function Countries(oRep As DTORep) As List(Of DTOCountry)
        'Countries amb les seves zones per iMat
        Dim oContacts As List(Of DTOContact) = RepCustomersLoader.All(oRep, Nothing, IncludeFuture:=True)
        Dim retval As List(Of DTOCountry) = oContacts.Select(Function(x) x.address.Zip.Location.Zona.Country).Distinct.ToList
        Return retval
    End Function

    Shared Function Archive(oRep As DTORep) As List(Of DTOPurchaseOrder)
        Return RepLoader.Archive(oRep)
    End Function

    Shared Function Baixa(oEmp As DTOEmp, oRep As DTORep, DtFch As Date, removePrivileges As Boolean, exs As List(Of Exception)) As Boolean
        If BEBL.Rep.Load(oRep) Then
            Dim oRepProducts = BEBL.RepProducts.All(oRep)
            For Each item In oRepProducts
                item.fchTo = DtFch
            Next
            If BEBL.RepProducts.Update(oRepProducts, exs) Then
                oRep.fchBaja = DtFch
                If BEBL.Rep.Update(oRep, exs) Then
                    If removePrivileges Then
                        Dim oUsers As List(Of DTOUser) = BEBL.Users.All(oRep, True)
                        For Each oUser In oUsers
                            oUser.Rol = New DTORol(DTORol.Ids.Guest)
                            BEBL.User.Update(oUser, exs)
                        Next
                    End If
                End If
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

End Class
Public Class Reps
    Shared Function All(oEmp As DTOEmp, Optional Active As Boolean = True) As List(Of DTORep)
        Dim retval As List(Of DTORep) = RepsLoader.All(oEmp, Active)
        Return retval
    End Function


    Shared Function All(oProduct As DTOProduct) As List(Of DTORep)
        Dim retval As List(Of DTORep) = RepsLoader.All(oProduct)
        Return retval
    End Function

    Shared Function Emails(oEmp As DTOEmp) As List(Of DTOEmail)
        Dim retval As List(Of DTOEmail) = RepsLoader.Emails(oEmp)
        Return retval
    End Function


    Shared Function Emails(oArea As DTOArea, oChannel As DTODistributionChannel) As List(Of DTOEmail)
        Dim retval As List(Of DTOEmail) = RepsLoader.Emails(oArea, oChannel)
        Return retval
    End Function

    Shared Function Emails(oProduct As DTOProduct) As List(Of DTOEmail)
        Dim retval As List(Of DTOEmail) = RepsLoader.Emails(oProduct)
        Return retval
    End Function


    Shared Function Ibans(oEmp As DTOEmp) As List(Of DTOIban)
        Dim retval As List(Of DTOIban) = RepsLoader.Ibans(oEmp)
        Return retval
    End Function

    Shared Function Saldos(oExercici As DTOExercici) As List(Of DTOPgcSaldo)
        Dim retval As List(Of DTOPgcSaldo) = RepsLoader.Saldos(DTOExercici.Current(oExercici.Emp))
        Return retval
    End Function


    Shared Function WithRetencions(oEmp As DTOEmp, FchFrom As Date, FchTo As Date) As List(Of DTORep)
        Return RepsLoader.WithRetencions(oEmp, FchFrom, FchTo)
    End Function

    Shared Function Sprite(oGuids As List(Of Guid), itemWidth As Integer, itemHeight As Integer) As Byte()
        Dim oImages = RepsLoader.Sprite(oGuids)
        Dim retval = LegacyHelper.SpriteHelper.Factory(oImages, itemWidth, itemHeight)
        Return retval
    End Function
End Class
