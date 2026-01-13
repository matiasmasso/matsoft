Public Class XecsPresentacioLoader
    Shared Function Update(oXecsPresentacio As DTOXecsPresentacio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            CcaLoader.Update(oXecsPresentacio.Cca, oTrans)
            For Each oXec As DTOXec In oXecsPresentacio.Xecs
                XecLoader.Update(oXec, oTrans)
            Next

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


    Shared Function Delete(oXecsPresentacio As DTOXecsPresentacio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim oCcaPresentacio As DTOCca = Nothing
            For Each oXec As DTOXec In oXecsPresentacio.Xecs
                With oXec
                    oCcaPresentacio = .CcaPresentacio
                    .StatusCod = DTOXec.StatusCods.EnCartera
                    .CodPresentacio = DTOXec.ModalitatsPresentacio.NotSet
                    .CcaPresentacio = Nothing
                    .NBanc = Nothing
                End With
                XecLoader.Update(oXec, oTrans)
            Next

            CcaLoader.Delete(oCcaPresentacio, oTrans)
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
End Class
