Public Class RepCliCom

    Shared Function Find(oGuid As Guid) As DTORepCliCom
        Dim retval As DTORepCliCom = RepCliComLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oRepCliCom As DTORepCliCom) As Boolean
        Dim retval As Boolean = RepCliComLoader.Load(oRepCliCom)
        Return retval
    End Function

    Shared Function Update(oRepCliCom As DTORepCliCom, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepCliComLoader.Update(oRepCliCom, exs)
        Return retval
    End Function

    Shared Function Delete(oRepCliCom As DTORepCliCom, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepCliComLoader.Delete(oRepCliCom, exs)
        Return retval
    End Function

    Shared Function RepCom(oRepCliCom As DTORepCliCom, oRepProduct As DTORepProduct) As DTORepCom
        Dim retval As DTORepCom = Nothing
        Select Case oRepCliCom.ComCod
            Case DTORepCliCom.ComCods.Standard
                retval = New DTORepCom
                retval.Rep = oRepProduct.Rep
                retval.Com = oRepProduct.ComStd
            Case DTORepCliCom.ComCods.Reduced
                retval = New DTORepCom
                retval.Rep = oRepProduct.Rep
                retval.Com = oRepProduct.ComRed
        End Select
        Return retval
    End Function

    Shared Function Match(oEmp As DTOEmp, oRep As DTORep, oCustomer As DTOCustomer, DtFch As Date, Optional oRepCliComs As List(Of DTORepCliCom) = Nothing) As DTORepCliCom
        If oRepCliComs Is Nothing Then oRepCliComs = RepCliComsLoader.All(oEmp)
        Dim retval As DTORepCliCom = oRepCliComs.FindAll(Function(x) x.Rep.Equals(oRep) And x.Customer.Equals(oCustomer) And x.Fch <= DtFch).
            OrderBy(Function(x) x.Fch).
            LastOrDefault

        Return retval
    End Function
End Class

Public Class RepCliComs

    Shared Function All(oRep As DTORep) As List(Of DTORepCliCom)
        Dim retval As List(Of DTORepCliCom) = RepCliComsLoader.All(oRep)
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp) As List(Of DTORepCliCom)
        Dim retval As List(Of DTORepCliCom) = RepCliComsLoader.All(oEmp)
        Return retval
    End Function

    Shared Function Delete(oRepCliComs As List(Of DTORepCliCom), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepCliComsLoader.Delete(oRepCliComs, exs)
        Return retval
    End Function
End Class
