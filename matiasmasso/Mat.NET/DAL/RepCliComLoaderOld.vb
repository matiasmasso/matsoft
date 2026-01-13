Public Class RepCliComLoaderOld
    Shared Function Find(oRep As DTORep, oCcx As DTOCustomer, DtFch As Date) As DTORepCliComOld
        Dim retval As DTORepCliComOld = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Com FROM Com ")
        sb.AppendLine("WHERE RepGuid=@RepGuid ")
        sb.AppendLine("AND CliGuid=@CliGuid ")
        sb.AppendLine("AND DESDE<=@Fch ")
        sb.AppendLine("AND (Hasta IS NULL OR Hasta>=@Fch)")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@RepGuid", oRep.Guid.ToString, "@CliGuid", oCcx.Guid.ToString, "@Fch", DtFch)
        If oDrd.Read Then
            retval = New DTORepCliComOld
            retval.RepCom = New DTORepCom
            retval.RepCom.Rep = oRep
            retval.RepCom.Com = oDrd("Com")
            retval.Customer = oCcx
        End If
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class RepCliComsLoaderOld
    Shared Function All(oEmp As DTOEmp, IncludeObsolets As Boolean) As List(Of DTORepCliComOld)
        Dim retval As New List(Of DTORepCliComOld)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT RepGuid, CliGuid, Desde, Hasta, Com ")
        sb.AppendLine("FROM Com ")
        sb.AppendLine("WHERE Com.Emp = " & CInt(oEmp.Id) & " ")

        If Not IncludeObsolets Then
            sb.AppendLine("AND DESDE<=GETDATE() ")
            sb.AppendLine("AND (HASTA IS NULL OR HASTA>=GETDATE()) ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRep As New DTORep(oDrd("RepGuid"))

            Dim oCustomer As New DTOCustomer(oDrd("CliGuid"))

            Dim oRepCom As New DTORepCom
            With oRepCom
                .Rep = oRep
                .Com = oDrd("Com")
            End With

            Dim item As New DTORepCliComOld
            With item
                .RepCom = oRepCom
                .Customer = oCustomer
                .FchFrom = oDrd("Desde")
                .FchTo = Defaults.FchOrNothing(oDrd("Hasta"))
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
