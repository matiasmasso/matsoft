Public Class QuizConsumerFairsLoader

#Region "CRUD"
    Shared Function FromContact(oContact As DTOContact) As List(Of DTOQuizConsumerFair)
        Dim retval As New List(Of DTOQuizConsumerFair)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * FROM QuizConsumerFair ")
        sb.AppendLine("WHERE Contact=@Guid ")
        sb.AppendLine("ORDER BY Brand, Franja ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oContact.Guid.ToString)
        Do While oDrd.Read
            Dim item As New DTOQuizConsumerFair
            With item
                .Contact = oContact
                .Evento = New DTOEvento(oDrd("Evento"))
                .Brand = New DTOProductBrand(oDrd("Brand"))
                .Franja = oDrd("Franja")
                .Tpv2pax = oDrd("Tpv2pax")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oQuizConsumerFairs As List(Of DTOQuizConsumerFair), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oQuizConsumerFairs, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oQuizConsumerFairs As List(Of DTOQuizConsumerFair), ByRef oTrans As SqlTransaction)
        If oQuizConsumerFairs.Count > 0 Then
            Delete(oQuizConsumerFairs, oTrans)

            Dim oContact As DTOContact = oQuizConsumerFairs(0).Contact
            Dim SQL As String = "SELECT * FROM QuizConsumerFair WHERE Contact=@Guid"
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oContact.Guid.ToString)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)

            For Each oQuiz As DTOQuizConsumerFair In oQuizConsumerFairs
                Dim oRow As DataRow = oTb.NewRow
                oRow("Evento") = oQuiz.Evento.Guid
                oRow("Contact") = oQuiz.Contact.Guid
                oRow("Brand") = oQuiz.Brand.Guid
                oRow("Tpv2pax") = oQuiz.Tpv2pax
                oRow("Franja") = oQuiz.Franja
                oTb.Rows.Add(oRow)
            Next

            oDA.Update(oDs)
        End If

    End Sub

    Shared Function Delete(oQuizConsumerFairs As List(Of DTOQuizConsumerFair), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oQuizConsumerFairs, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Delete(oQuizConsumerFairs As List(Of DTOQuizConsumerFair), ByRef oTrans As SqlTransaction)
        If oQuizConsumerFairs.Count > 0 Then
            Dim oContact As DTOContact = oQuizConsumerFairs(0).Contact
            Dim SQL As String = "DELETE QuizConsumerFair WHERE Contact=@Guid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oContact.Guid.ToString)
        End If
    End Sub

#End Region

End Class
